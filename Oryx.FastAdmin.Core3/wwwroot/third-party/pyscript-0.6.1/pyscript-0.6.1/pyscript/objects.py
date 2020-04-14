# Copyright (C) 2002-2006  Alexei Gilchrist and Paul Cochrane
# 
# This program is free software; you can redistribute it and/or
# modify it under the terms of the GNU General Public License
# as published by the Free Software Foundation; either version 2
# of the License, or (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

# $Id: objects.py,v 1.50 2006/05/16 05:38:31 aalexei Exp $

"""
Some of the key drawing objects
"""

__revision__ = '$Revision: 1.50 $'

import os, re, sys
import cStringIO

from math import cos, sin, pi

from pyscript.defaults import defaults
from pyscript.vectors import P, Bbox, Matrix
from pyscript.base import PsObj, Color, UNITS
from pyscript.afm import AFM

import warnings

# -------------------------------------------------------------------------
class AffineObj(PsObj):
    '''
    A base class for object that should implement affine
    transformations, this should apply to any object that draws
    on the page.
    '''

    o = P(0, 0)
    T = Matrix(1, 0, 0, 1)

    def concat(self, t, p = None):
        '''
        concat matrix t to tranformation matrix
        @param t: a 2x2 Matrix dectribing Affine transformation
        @param p: the origin for the transformation
        @return: reference to self
        @rtype: self
        '''

        # update transformation matrix
        self.T = t*self.T
        
        #if p is not None:
        #    o=self.o # o is in external co-ords
        #    self.move(p-o)
        #    self.move(t*(o-p))
        

        o = self.o # o is in external co-ords

        # set origin at (0,0)
        self.move(-o)

        # move to transformed p id defined 
        if p is not None:
            self.move(p)
            self.move(t*(-p))

        # move to transformed origin
        self.move(t*o)
       
        return self

    def move(self, *args):
        '''
        translate object by a certain amount
        @param args: amount to move by, can be given as
         - dx,dy
         - P
        @return: reference to self
        @rtype: self
        '''
        if len(args) == 1:
            # assume we have a point
            self.o += args[0]
        else:
            # assume we have dx,dy
            self.o += P(args[0], args[1])

            return self
            
    def rotate(self, angle, p = None):
        """
        rotate object, 
        the rotation is around p when supplied otherwise
        it's the objects origin

        @param angle: angle in degrees, clockwise
        @param p: point to rotate around (external co-ords)
        @return: reference to self
        @rtype: self
        """ 
        angle = angle/180.0*pi # convert angle to radians
        t = Matrix(cos(angle), sin(angle), -sin(angle), cos(angle))
        self.concat(t, p)

        return self

    def scale(self, sx, sy = None, p = None):
        '''
        scale object size (towards objects origin or p)
        @param sx: x scale factor, or total scale factor if sy=None
        @param sy: y scale factor
        @param p: point around which to scale
        @return: reference to self
        @rtype: self
        '''
        if sy is None: 
            sy = sx
        
        t = Matrix(sx, 0, 0, sy)
        self.concat(t, p)
        
        return self

    def reflect(self, angle, p = None):
        '''
        reflect object in mirror
        @param angle: angle of mirror (deg clockwise from top)
        @param p: origin of reflection
        @return: reference to self
        @rtype: self
        '''
        # convert angle to radians, clockwise from top
        angle = angle/180.0*pi-pi/2 

        t = Matrix(
            cos(angle)**2-sin(angle)**2,
            -2*sin(angle)*cos(angle),
            -2*sin(angle)*cos(angle),
            sin(angle)**2-cos(angle)**2
            )
        
        self.concat(t, p)
        
        return self


    def shear(self, s, angle, p = None):
        '''
        shear object
        @param s: amount of shear
        @param angle: direction of shear (deg clockwise from top)
        @param p: origin of shear
        @return: reference to self
        @rtype: self
        '''

        self.rotate(angle, p)
        
        t = Matrix(1, 0, -s, 1)
        self.concat(t, p)
        
        self.rotate(-angle, p)
        
        return self
    
        
    def itoe(self, p_i):
        '''
        convert internal to external co-ords
        @param p_i: internal co-ordinate
        @return: external co-ordinate
        @rtype: P
        '''
        assert isinstance(p_i, P), "object not a P()"

        return self.T*p_i+self.o
        
    def etoi(self, p_e):
        '''
        convert external to internal co-ords
        @param p_e: external co-ordinate
        @return: internal co-ordinate
        @rtype: P
        '''
        assert isinstance(p_e, P), "object not a P()"

        return self.T.inverse()*(p_e-self.o)


    def prebody(self):
        '''
        set up transformation of coordinate system
        @rtype: string
        '''
        T = self.T
        o = self.o
        S = "gsave "
        if T == Matrix(1, 0, 0, 1):
            S = S+"%s translate\n" % o
        else:
            # NB postscript matrix is the transpose of what you'd expect!
            S = S+"[%g %g %g %g %s] concat\n" % (T[0], T[2], T[1], T[3], o())
        return S

    def postbody(self):
        '''
        undo coordinate system transformation
        @rtype: string
        '''
        return "grestore\n"


# -------------------------------------------------------------------------

class Area(AffineObj):
    """
    A Rectangular area defined by sw corner and width and height.
    
    defines the following compass points that can be set and retrived::

          nw--n--ne
          |       |
          w   c   e
          |       |
          sw--s--se

    The origin is the sw corner and the others are calculated from the
    width and height attributes.

    If a subclass should have the origin somewhere other than sw then
    overide the sw attribute to make it a function
    
    @cvar width: the width
    @cvar height: the height
    @cvar c: centre point (simillarly for n,ne etc)
    """

    #XXX allow the changing of sw corner away from origin eg Text

    isw = P(0, 0)
    width = 0
    height = 0


    # Dynamic locations
    def _get_n(self):
        """
        Get the "north" point
        """
        return self.itoe(P(self.width/2., self.height)+self.isw)

    def _set_n(self, pe):
        """
        Set the "north" point
        """
        self.move(pe-self.n)
    n = property(_get_n, _set_n)

    def _get_ne(self):
        """
        Get the "north-east" point
        """
        return self.itoe(P(self.width, self.height)+self.isw)

    def _set_ne(self, pe):
        """
        Set the "north-east" point
        """
        self.move(pe-self.ne)
    ne = property(_get_ne, _set_ne)

    def _get_e(self):
        """
        Get the "east" point
        """
        return self.itoe(P(self.width, self.height/2.)+self.isw)

    def _set_e(self, pe):
        """
        Set the "east" point
        """
        self.move(pe-self.e)
    e = property(_get_e, _set_e)

    def _get_se(self):
        """
        Get the "south-east" point
        """
        return self.itoe(P(self.width, 0)+self.isw)

    def _set_se(self, pe):
        """
        Set the "south-east" point
        """
        self.move(pe-self.se)
    se = property(_get_se, _set_se)

    def _get_s(self):
        """
        Get the "south" point
        """
        return self.itoe(P(self.width/2., 0)+self.isw)

    def _set_s(self, pe):
        """
        Set the "south" point
        """
        self.move(pe-self.s)
    s = property(_get_s, _set_s)

    def _get_sw(self):
        """
        Get the "south-west" point
        """
        return self.itoe(self.isw)

    def _set_sw(self, pe):
        """
        Set the "south-west" point
        """
        self.move(pe-self.sw)
    sw = property(_get_sw, _set_sw)

    def _get_w(self):
        """
        Get the "west" point
        """
        return self.itoe(P(0, self.height/2.)+self.isw)

    def _set_w(self, pe):
        """
        Set the "west" point
        """
        self.move(pe-self.w)
    w = property(_get_w, _set_w)

    def _get_nw(self):
        """
        Get the "north-west" point
        """
        return self.itoe(P(0, self.height)+self.isw)

    def _set_nw(self, pe):
        """
        Set the "north-west" point
        """
        self.move(pe-self.nw)
    nw = property(_get_nw, _set_nw)

    def _get_c(self):
        """
        Get the "centre" point
        """
        return self.itoe(P(self.width/2., self.height/2.)+self.isw)
    def _set_c(self, pe):
        """
        Set the "centre" point
        """
        self.move(pe-self.c)
    c = property(_get_c, _set_c)

    def bbox(self):
        """
        Return the bounding box of the object
        """

        x1, y1 = self.sw
        x2, y2 = self.ne

        for p in [self.sw, self.nw, self.ne, self.se]:
            x1 = min(x1, p[0])
            y1 = min(y1, p[1])
            x2 = max(x2, p[0])
            y2 = max(y2, p[1])

        return Bbox(sw = P(x1, y1), width = x2-x1, height = y2-y1)

# -------------------------------------------------------------------------
class TeX(Area):
    '''
    A TeX expression
    (requires working latex and dvips systems)
    
    @cvar fg: TeX color
    @cvar iscale: initial scale for tex
    '''

    text = ""
    iscale = 1
    fg = Color(0)
    bodyps = ""

    def __init__(self, text = "", **options):

        self.text = text

        print "Obtaining TeX object's boundingbox ..."
        
        # this should be a tempfile ?
        tempName = "temp1"
        fp = open("%s.tex"%tempName, "w")
        fp.write(defaults.tex_head)
        fp.write(text)
        fp.write(defaults.tex_tail)
        fp.close()

        foe = os.popen(defaults.tex_command % tempName)
        sys.stderr.write(foe.read(-1))
        sys.stderr.write('\n')
        # Help the user out by throwing the latex log to stderr
        if os.path.exists("%s.log" % tempName):
            fp = open("%s.log" % tempName, 'r')
            sys.stderr.write(fp.read(-1))
            fp.close()
        if foe.close() is not None:
            raise RuntimeError, "Latex Error"


        fi, foe = os.popen4("dvips -q -E -o %s.eps %s.dvi" % \
                (tempName, tempName))
        err = foe.read(-1)
        sys.stderr.write(err)
        sys.stderr.write('\n')
        fi.close()
        if len(err)>0:
            raise RuntimeError, "dvips Error"
    
        fp = open("%s.eps" % tempName, "r")
        eps = fp.read(-1)
        fp.close()
    
        # grab boundingbox ... only thing we want at this stage
        bbox_so = re.search("\%\%boundingbox:\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)",
                          eps, re.I)
        bbox = []
        for ii in bbox_so.groups():
            bbox.append(int(ii))

        self.width = (bbox[2]-bbox[0])/float(defaults.units)
        self.height = (bbox[3]-bbox[1])/float(defaults.units)

        # now we have a width and height we can initialise Area
        Area.__init__(self, **options)

        self.offset = -P(bbox[0], bbox[1])/float(defaults.units)

        self.scale(self.iscale)

    def body(self):
        """
        Returns the object's postscript body
        """
        out = cStringIO.StringIO()

        
        out.write("%s translate "%self.offset)
        out.write("%s\n"%self.fg)
        out.write("%s\n"%self.bodyps)
        return out.getvalue()


# -------------------------------------------------------------------------

class Text(Area):
    '''
    A single line text object
    @cvar font: postscript font name eg "Times-Roman"
    @cvar size: pointsize of the font
    @cvar kerning: use kerning?
    @cvar text: the string to typeset
    @cvar fg: color of the text
    '''
    
    # these all affect the size so should be dynamic
    _text = ''
    _font = "Times-Roman"
    _size = 12
    _kerning = 1
    
    fg = Color(0)

    def __init__(self, text = "", **options):

        # get the bbox
        # first need font and scale BEFORE positioning
        # for efficiency don't use dynamic attributes
        self._text = text
        self._font = options.get('font', self._font)
        self._size = options.get('size', self._size)
        self._kerning = options.get('kerning', self._kerning)
        
        # Now calc sizes from AFM
        self._typeset()

        Area.__init__(self, **options)
        
    def _get_font(self):
        """
        Get the font
        """
        return self._font
    def _set_font(self, fontname):
        """
        Set the font
        """
        self._font = fontname
        self._typeset()
    font = property(_get_font, _set_font)
        
    def _get_size(self):
        """
        Get the font size
        """
        return self._size
    def _set_size(self, size):
        """
        Set the font size
        """
        self._size = size
        self._typeset()
    size = property(_get_size, _set_size)

    def _get_kerning(self):
        """
        Get the kerning information
        """
        return self._kerning
    def _set_kerning(self, kerning):
        """
        Set the kerning information
        """
        self._kerning = kerning
        self._typeset()
    kerning = property(_get_kerning, _set_kerning)

    def _get_text(self):
        """
        Get the text of the Text object
        """
        return self._text
    def _set_text(self, text):
        """
        Set the text of the Text object
        """
        self._text = text
        self._typeset()
    text = property(_get_text, _set_text)
    
    def _typeset(self):
        """
        Typeset the Text object (including kerning info)
        """
        
        string = self.text
        afm = AFM(self._font)
        
        # set the correct postscript font name
        self._font = afm.FontName
        
        size = self.size
        sc = size/1000.

        chars = map(ord, list(string))

        # order: width l b r t

        # use 'reduce' and 'map' as they're written in C

        # add up all the widths
        width = reduce(lambda x, y: x+afm[y][0], chars, 0)

        # subtract the kerning
        if self.kerning == 1:
            if len(chars)>1:
                kerns = map(lambda x, y:afm[(x, y)] , chars[:-1], chars[1:])
                
                charlist = list(string)

                out = "("
                for ii in kerns:
                    if ii != 0:
                        out += charlist.pop(0)+") %s ("%str(ii*sc)
                    else:
                        out += charlist.pop(0)
                out += charlist.pop(0)+")"
                
                settext = out

                kern = reduce(lambda x, y:x+y, kerns)
                            
                width += kern
            else:
                # this is to catch the case when there are no characters
                # in the string, but self.kerning==1
                settext = "("+string+")"

        else:
            settext = "("+string+")"

        # get rid of the end bits
        start = afm[chars[0]][1]
        f = afm[chars[-1]]
        width = width-start-(f[0]-f[3])

        # accumulate maximum height
        top = reduce(lambda x, y: max(x, afm[y][4]), chars, 0)

        # accumulate lowest point
        bottom = reduce(lambda x, y: min(x, afm[y][2]), chars, afm[chars[0]][2])

        x1 = start*sc
        y1 = bottom*sc
        x2 = x1+width*sc
        y2 = top*sc

        self.settext = settext
        self.offset = -P(x1, y1)/float(defaults.units)
        self.width = (x2-x1)/float(defaults.units)
        self.height = (y2-y1)/float(defaults.units)

    def body(self):
        """
        Returns the object's postscript body
        """
        out = cStringIO.StringIO()

        ATTR = {'font':self.font,
              'fg':self.fg,
              'size':self.size,
              'settext':self.settext,
              'offset':self.offset}
        
        out.write("%(offset)s moveto\n" % ATTR)
        out.write("/%(font)s %(size)s selectfont %(fg)s \n" % ATTR)
        out.write("mark %(settext)s kernshow\n" % ATTR)

        return out.getvalue()


# -------------------------------------------------------------------------
# Rectangle
# -------------------------------------------------------------------------
class Rectangle(Area):
    """
    Draw a rectangle 

    @cvar linewidth: the line thickness in points
    @type linewidth: float

    @cvar dash: a Dash() object giving the dash pattern to use 
    @type dash: L{Dash} object

    @cvar fg: line color
    @type fg: L{Color} object

    @cvar bg: fill color or None for empty
    @type bg: L{Color} object

    @cvar r: radius of corners (saturates at min(width/2,height/2))
    @type r: float

    @cvar width: width of rectangle
    @type width: float

    @cvar height: height of rectangle
    @type height: float
    """

    bg = None
    fg = Color(0)
    r = 0.0
    linewidth = None
    dash = None
    width = 1.0
    height = 1.0

    def __init__(self, obj=None, **options):
        '''
        @param obj:
            for Area() or Bbox(), the size and position will
            be taken from obj
        '''

        if isinstance(obj, Area) or isinstance(obj, Bbox):
            options['sw'] = obj.sw
            options['width'] = obj.width
            options['height'] = obj.height
            
        Area.__init__(self, **options)
    
    def body(self):
        """
        Returns the object's postscript body
        """
        
        out = cStringIO.StringIO()
        
        if self.linewidth:
            out.write("%g setlinewidth "%self.linewidth)

        if self.dash is not None:
            out.write(str(self.dash))
        
        # make sure we have a sensible radius
        r = min(self.width/2., self.height/2., self.r)
        w = self.width
        h = self.height
        
        ATTR = {'bg':self.bg,
              'fg':self.fg,
              'width':w,
              'height':h,
              'r':r,
              'ne':P(w, h),
              'n':P(w/2., h),
              'nw':P(0, h),
              'w':P(0, h/2.),
              'sw':P(0, 0),
              's':P(w/2., 0),
              'se':P(w, 0),
              'e':P(w, h/2.),
              }
                
        if self.bg is not None:
            if self.r == 0:
                out.write("%(bg)s 0 0 %(width)g uu %(height)g uu rectfill\n"\
                        % ATTR)
            else:
                out.write("%(bg)s newpath %(w)s moveto\n" % ATTR)
                out.write("%(nw)s %(n)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("%(ne)s %(e)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("%(se)s %(s)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("%(sw)s %(w)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("closepath fill\n")
                
        if self.fg is not None:
            if self.r == 0:
                out.write("%(fg)s 0 0 %(width)g uu %(height)g uu rectstroke\n"\
                                % ATTR)
            else:
                out.write("%(fg)s newpath %(w)s moveto\n" % ATTR)
                out.write("%(nw)s %(n)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("%(ne)s %(e)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("%(se)s %(s)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("%(sw)s %(w)s %(r)s uu arcto 4 {pop} repeat\n" % ATTR)
                out.write("closepath stroke\n")
                
        return out.getvalue()


# -------------------------------------------------------------------------
class Circle(AffineObj):
    """
    Draw a circle, or part of. Generate ellipses by scaling. 
    The origin is the center
    
    @cvar r: radius
    @type r: float

    @cvar start: starting angle for arc
    @type start: float

    @cvar end: end angle for arc
    @type end: float

    @cvar c: (also n, ne,...) as for L{Area}
    @type c: L{P} object

    @cvar linewidth: width of the lines
    @type linewidth: float

    @cvar dash: Dash() object giving dash pattern to use
    @type dash: L{Dash} object
    """

    bg = None
    fg = Color(0)
    r = 1.0
    start = 0.0
    end = 360.0
    linewidth = None
    dash = None
    
    def locus(self, angle, target=None):
        '''
        Set or get a point on the locus
        
        @param angle: locus point in degrees
            (Degrees clockwise from north)
        @type angle: float

        @param target: target point
        @return: 
            - target is None: point on circumference at that angle
            - else: set point to the target, and return reference
                    to object
        @rtype: self or P
        '''
        r = self.r
        x = r*sin(angle/180.0*pi)
        y = r*cos(angle/180.0*pi)
        l = P(x, y)

        if target is None:
            return self.itoe(l)
        else:
            self.move(target-self.locus(angle))
            return self
    
    # some named locations
    def _get_c(self):
        """
        Get the "centre" point
        """
        return self.o

    def _set_c(self, pe):
        """
        Set the "centre" point
        """
        self.move(pe-self.o)
    c = property(_get_c, _set_c)

    def _get_n(self):
        """
        Get the "north" point
        """
        return self.locus(0)
    
    def _set_n(self, pe):
        """
        Set the "north" point
        """
        self.locus(0, pe)
    n = property(_get_n, _set_n)

    def _get_e(self):
        """
        Get the "east" point
        """
        return self.locus(90)

    def _set_e(self, pe):
        """
        Set the "east" point
        """
        self.locus(90, pe)
    e = property(_get_e, _set_e)

    def _get_s(self):
        """
        Get the "south" point
        """
        return self.locus(180)

    def _set_s(self, pe):
        """
        Set the "south" point
        """
        self.locus(180, pe)
    s = property(_get_s, _set_s)

    def _get_w(self):
        """
        Get the "west" point
        """
        return self.locus(270)

    def _set_w(self, pe):
        """
        Set the "west" point
        """
        self.locus(270, pe)
    w = property(_get_w, _set_w)

    # these are of the square that holds the circle
    def _get_ne(self):
        """
        Get the "nort-east" point
        """
        return self.itoe(P(self.r, self.r))

    def _set_ne(self, pe):
        """
        Set the "north-east" point
        """
        self.move(pe-self.ne)
    ne = property(_get_ne, _set_ne)

    def _get_nw(self):
        """
        Get the "nort-west" point
        """
        return self.itoe(P(-self.r, self.r))

    def _set_nw(self, pe):
        """
        Set the "nort-west" point
        """
        self.locus(315, pe)
        self.move(pe-self.nw)
    nw = property(_get_nw, _set_nw)

    def _get_se(self):
        """
        Get the "south-east" point
        """
        return self.itoe(P(self.r, -self.r))

    def _set_se(self, pe):
        """
        Set the "south-east" point
        """
        self.locus(135, pe)
        self.move(pe-self.se)
    se = property(_get_se, _set_se)

    def _get_sw(self):
        """
        Get the "south-west" point
        """
        return self.itoe(P(-self.r, -self.r))

    def _set_sw(self, pe):
        """
        Set the "south-west" point
        """
        self.locus(235, pe)
        self.move(pe-self.sw)
    sw = property(_get_sw, _set_sw)

    def body(self):
        """
        Returns the object's postscript body
        """
        out = cStringIO.StringIO()

        if self.linewidth:
            out.write("%g setlinewidth " % self.linewidth)

        if self.dash is not None:
            out.write(str(self.dash))

        # By default postscript goes anti-clockwise
        # and starts from 'e' ... fix it so it goes
        # clockwise and starts from 'n'

        ATTR = {'bg':self.bg,
              'fg':self.fg,
              'r':self.r,
              'start':self.start,
              'end':self.end}

        if self.bg is not None:
            out.write("%(bg)s 0 0 %(r)g uu 360 %(start)g -1 mul add 90 " 
                    "add 360 %(end)g -1 mul add 90 add arcn fill\n" % ATTR)

        if self.fg is not None:
            out.write("%(fg)s 0 0 %(r)g uu 360 %(start)g -1 mul add 90 "
                    "add 360 %(end)g -1 mul add 90 add arcn stroke\n" % ATTR)

        return out.getvalue()

    def bbox(self):
        """
        Return the bounding box object of the Circle
        """

        #grab a tight boundingbox by zipping around circumference

        SW = self.locus(0)
        NE = self.locus(0)
        for ii in xrange(self.start, self.end+10, 10):
            p = self.locus(ii)

            SW[0] = min(SW[0], p[0])
            SW[1] = min(SW[1], p[1])
            NE[0] = max(NE[0], p[0])
            NE[1] = max(NE[1], p[1])


        return Bbox(sw = SW, width = NE[0]-SW[0], height = NE[1]-SW[1])

# -------------------------------------------------------------------------
class Dot(Circle):
    '''
    draw a dot at the given location
    @cvar r: dot radius
    @cvar bg: dot color
    @cvar fg: dot border color
    '''
    r = .05
    bg = Color(0)
    fg = None

    def __init__(self, p1 = P(0, 0), p2 = 0, **options):
        if isinstance(p1, P):
            c = p1
        else:
            c = P(p1, p2)
        Circle.__init__(self, **options)
        self.c = c

    def bbox(self):
        """
        Return the bounding box of the Dot
        """
        
        return Bbox(sw = self.sw, width = 2*self.r, height = 2*self.r)




# -------------------------------------------------------------------------

class Paper(Area):
    '''
    returns an area object the size of one of the standard paper sizes
    
    B{Class deprecated - use Page instead}
    '''

    size = None
    orientation = "portrait"

    # PAPERSIZES taken from gs man page (x cm,y cm)
    PAPERSIZES = {
        "a0":         (83.9611, 118.816),
        "a1":         (59.4078, 83.9611),
        "a2":         (41.9806, 59.4078),
        "a3":         (29.7039, 41.9806),
        "a4":         (20.9903, 29.7039),
        "a4r":        (29.7039, 20.9903),  # rotated version of a4
        "a5":         (14.8519, 20.9903),
        "a6":         (10.4775, 14.8519),
        "a7":         (7.40833, 10.4775),
        "a8":         (5.22111, 7.40833),
        "a9":         (3.70417, 5.22111),
        "a10":        (2.61056, 3.70417),
        "b0":         (100.048, 141.393),
        "b1":         (70.6967, 100.048),
        "b2":         (50.0239, 70.6967),
        "b3":         (35.3483, 50.0239),
        "b4":         (25.0119, 35.3483),
        "b5":         (17.6742, 25.0119),
        "archA":      (22.86  , 30.48),
        "archB":      (30.48  , 45.72),
        "archC":      (45.72  , 60.96),
        "archD":      (60.96  , 91.44),
        "archE":      (91.44  , 121.92),
        "flsa":       (21.59  , 33.02),
        "flse":       (21.59  , 33.02),
        "halfletter": (13.97  , 21.59),
        "note":       (19.05  , 25.4 ),
        "letter":     (21.59  , 27.94),
        "legal":      (21.59  , 35.56),
        "11x17":      (27.94  , 43.18),
        "ledger":     (43.18  , 27.94),
        }

    def __init__(self, size, **options):
        '''
        @param size: eg "a4","letter" etc. See L{PAPERSIZES} for sizes
        @return: An area object the size of the selected paper
                 with the sw corner on P(0,0)
        '''
        warnings.warn("Paper() class deprecated .. use Page()")
        
        self.size = size
        orientation = options.get("orientation", self.orientation)
        
        if orientation == "portrait":
            w, h = self.PAPERSIZES[size]
        else:
            h, w = self.PAPERSIZES[size]
        
        self.width = w*UNITS['cm']/float(defaults.units)
        self.height = h*UNITS['cm']/float(defaults.units)

        Area.__init__(self, **options)

# -------------------------------------------------------------------------

class Epsf(Area):
    '''
    Load an Eps from file

    @cvar width: on init - set width to this
    @type width: float

    @cvar height: on init - set height to this
    @type height: float
    '''

    def __init__(self, file, **options):
        '''
        @param file: path to epsf file
        @type file: string

        @return: The eps figure as an area object
        '''

        self.file = file

        print "Loading %s" % file
        
        fp = open(file, 'r')
        self.all = fp.read(-1)
        fp.close()

        bbox_so = re.compile(
                "\%\%boundingbox:\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)", 
                re.I|re.S)
        
        so = bbox_so.search(self.all)
        x1s, y1s, x2s, y2s = so.groups()

        d = float(defaults.units)
        x1 = float(x1s)/d
        y1 = float(y1s)/d
        x2 = float(x2s)/d
        y2 = float(y2s)/d

        self.offset = -P(x1, y1)

        self.width = x2-x1
        self.height = y2-y1

        # width and height have special meaning here
        if options.has_key('width') and options.has_key('height'):
            sx = options['width']/float(self.width)
            sy = options['height']/float(self.height)
            del options['width']
            del options['height']
        elif options.has_key('width'):
            sx = sy = options['width']/float(self.width)
            del options['width']
        elif options.has_key('height'):
            sx = sy = options['height']/float(self.height)
            del options['height']
        else:
            sx = sy = 1
            
        self.scale(sx, sy)

        Area.__init__(self, **options)

    def body(self):
        """
        Return the body of the object's postcript
        """
        
        out = cStringIO.StringIO()

        out.write("BeginEPSF\n")
        out.write("%s translate \n" % self.offset)

        out.write("%%%%BeginDocument: %s\n" % self.file)
        out.write(self.all)
        out.write("%%EndDocument\n")
        out.write("EndEPSF\n")
        
        return out.getvalue()


# vim: expandtab shiftwidth=4:
