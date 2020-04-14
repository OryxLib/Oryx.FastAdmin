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

# $Id: groups.py,v 1.35 2006/02/28 18:16:58 paultcochrane Exp $

"""
Groupies

Here are collected all the classes and functions to do with groups.
Since much of the rendering is done by Eps, Page and Pages this
module also contains some helper functions such as collecttex
and TeXStuff.
"""

__revision__ = '$Revision: 1.35 $'

import cStringIO, time, os, sys

from types import TupleType, ListType
from pyscript.base import PsObj
from pyscript.vectors import P, Bbox, U
from pyscript.objects import Area, TeX
from pyscript.defaults import defaults
from pyscript.version import version

# -------------------------------------------------------------------------

class Group(Area):
    """
    Groups together a list of objects
    """
    
    def __init__(self, *objects, **options):
        """
        Initialisation of Group object

        @param objects: list of objects to group together
        @type objects: list

        @param options: dictionary of options
        @type options: dict
        """

        self.objects = []
        self.objbox = Bbox()
        
        if len(objects) == 1 and type(objects[0]) in (TupleType, ListType):
            apply(self.append, objects[0])
            #self.append(objects[0])
        else:
            apply(self.append, objects)
            #self.append(objects)

        Area.__init__(self, **options)

    def __getitem__(self, i):
        """
        Get an item from the list of objects

        @param i: the index of the item to get
        @type i: int
        """
        return self.objects[i]
        
    # these will break alignment
    #def __setitem__(self,i,other):
    #    self.objects[i]=other

    def __getslice__(self, i, j):
        """
        Get a slice of items from the list of objects

        @param i: the first index of the slice of items to get
        @type i: int

        @param j: the last index of the slice of items to get
        @type j: int
        """
        return self.objects[i:j]

    #def __setslice__(self,i,j,wert):
    #    self.objects[i:j]=wert

    def __len__(self):
        """
        Returns the length of the object list
        """
        return len(self.objects)

    def validate(self, obj):
        '''
        make sure this object can be inserted into group

        @param obj: object to test for insertability
        @type obj: object
        '''
        if isinstance(obj, Page) or isinstance(obj, Pages):
            raise TypeError, "Can't add a Page to %s" % str(self.__class__)

    def reverse(self):
        """
        Reverse the order of objects in the list of objects in the group
        """
        self.objects.reverse()
        
        # for convenience return reference to group
        return self

    def insert(self, idx, obj):
        '''
        insert object

        @param idx: index at which to insert object
        @type idx: int

        @param obj: the object to insert
        @type obj: object
        '''

        self.validate(obj)
        self.objbox.union(obj.bbox())
        self.objects.insert(idx, obj)

        # for convenience return reference to group
        return self

    def append(self, *objs, **options):
        '''
        append object(s) to group

        @param objs: list of objects to append
        @type objs: list

        @param options: dictionary of options
        @type options: dict
        '''
        for obj in objs:
            self.validate(obj)
            self.objbox.union(obj.bbox())
            self.objects.append(obj)

        # update size
        if self.objbox.is_set():
            self.isw = self.objbox.sw
            self.width = self.objbox.width
            self.height = self.objbox.height

        # for convenience return reference to group
        return self

    def apply(self, **options):
        '''
        apply attributes to all objects

        @param options: dictionary of attributes
        @type options: dict

        @return: reference to self
        @rtype: self
        '''
        # do this by attributes since they 
        # might not all get accepted

        for key, value in options.items():
            dict1 = {key:value}
            for obj in self.objects:
                if isinstance(obj, Group):
                    # recurse
                    apply(obj.apply, (), options)
                try:
                    apply(obj, (), dict1)            
                except AttributeError:
                    # skip objects that don't have the attribute
                    pass
        # we don't know if the sizes where changes so recalculate them
        self.recalc_size()

        # for convenience return reference to group
        return self

    def recalc_size(self):
        '''
        recalculate internal container size based on objects within
        '''
        self.objbox = Bbox()
        for obj in self.objects:
            self.objbox.union(obj.bbox())

        if self.objbox.is_set():
            self.isw = self.objbox.sw
            self.width = self.objbox.width
            self.height = self.objbox.height

    def clear(self):
        '''
        Clear all the elements and reset group to 
        an empty group
        '''

        self.isw = Area.isw
        self.width = Area.width
        self.height = Area.height
        self.objbox = Bbox()
        self.objects = []
        
        
    def body(self):
        """
        Returns the body postscript of the object
        """
        out = cStringIO.StringIO()
        for obj in self.objects:
            if obj.bbox().sw is not None:
                out.write(str(obj))
        return out.getvalue()


    def bbox(self):
        """
        Gather together common bounding box for group
        Don't use Area's bbox as transformations may
        mean a tighter bbox (eg a circle)
        @return: a Bbox()
        @rtype: Bbox
        """

        # We need to do the calculation in the 
        # external co-ordinates (that's where the
        # bounding box will be used)

        # first a null Bbox
        bbox = Bbox()
        
        for obj in self.objects:
            bbox.union(obj.bbox(), self.itoe)

        return bbox

# -------------------------------------------------------------------------
class Align(Group):
    '''
    Function to align a group of objects.

     - a1: The first anchor point to align to eg "e", "c"
     - a2: The second anchor point for aligning
     - space: the amount of space to enforce between the anchor points, 
              if None, then only move perpendicular to angle
     - angle: the angle of the line between anchor points
    '''

    a1 = 'c'
    a2 = 'c'
    space = None
    angle = 0

    def __init__(self, *objects, **options):
        """
        Initialisation of the Align object

        @param objects: list of objects to align
        @type objects: list

        @param options: dictionary of attributes
        @type options: dict
        """

        Group.__init__(self, **options)

        self.aligned = []

        self.append(*objects)
        

    def append(self, *objects, **options):
        """
        Append objects to list of objects for aligning

        @param objects: list of objects to append
        @type objects: list

        @param options: dictionary of attributes to temporarily override
        @type options: dict
        """

        # this allows temporary overiding of parameters:

        a1 = options.get('a1', self.a1)
        a2 = options.get('a2', self.a2)
        space = options.get('space', self.space)
        angle = options.get('angle', self.angle)

        assert a1 in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]
        assert a2 in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]

        for obj in objects:
            if isinstance(obj, PsObj):

                if len(self.aligned)==0:
                    # first object sets the position ...
                    Group.append(self, obj)
                    self.aligned.append(obj)
                else: 

                    # align the object
                    p1 = getattr(self.aligned[-1].bbox(), a1)
                    p2 = getattr(obj.bbox(), a2)

                    if space is not None:
                        obj.move(U(angle, space)-(p2-p1))

                    else:
                        # Don't touch the spacing in the angle direction
                        # adjust in othogonal direction instead

                        obj.move((U(angle+90)*(p2-p1))*U(angle-90))
            
                    Group.append(self, obj)
                    self.aligned.append(obj)
            else:
                # append but don't align
                Group.append(self, obj)

        # for convenience
        return self


# ----------------------------------------------------------------------
# some convenience functions

class VAlign(Align):
    """
    Vertical align class
    """
    a1 = 's'
    a2 = 'n'
    space = 1
    angle = 180
    
class HAlign(Align):
    """
    Horizontal align class
    """
    a1 = 'e'
    a2 = 'w'
    space = 1
    angle = 90

# -------------------------------------------------------------------------

def Distribute(*items, **options):
    '''
    Function to distribute a group of objects.

    @param items: list of items to distribute
    @type items: list

    @param options: dictionary of attributes
    @type options: dict

     - p1: first point of the line along which to distribute
     - p2: second point of the line along which to distribute
     - a1: The first anchor point to use for spacing to eg "e", "c"
     - a2: The second anchor point for spacing
     - as: anchor point for first item (overides a2 if present)
     - ae: anchor point for last item (overides a1 if present)
    @return: a reference to a group containing the objects
    @rtype: Group
    '''

    a1 = options.get('a1', 'c')
    a2 = options.get('a2', 'c')

    assert a1 in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]
    assert a2 in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]

    # note the swap:
    as = options.get('as', a2)
    ae = options.get('ae', a1)

    assert as in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]
    assert ae in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]

    # these two have to be present
    p1 = options['p1']
    p2 = options['p2']

    pv = p2-p1

    if len(items) == 1:
        if isinstance(items[0], Group):
            items = items[0]

    # A vector giving the direction to distribute things
    pv = p2-p1


    if len(items) == 1:
        # place item in the centre
        
        ov = ( getattr(items[0].bbox(), a1)+getattr(items[0].bbox(), a2) )/2. -p1

        # how much we need to move by
        mv = (pv.length/2.-pv.U*ov)*pv.U

        items[0].move(mv)

    else:

        # work out the amount of space we have to play with
        space = pv.length

        # place items at the edges
        # ---first object----
        ov = getattr(items[0].bbox(), as)-p1

        # how much we need to move by
        mv = -pv.U*ov*pv.U
        items[0].move(mv)

        space -= abs(( getattr(items[0].bbox(), a1)
                         - getattr(items[0].bbox(), as) )*pv.U)

        # ---second object---
        ov = getattr(items[-1].bbox(), ae)-p2

        # how much we need to move by
        mv = -pv.U*ov*pv.U
        items[-1].move(mv)

        space -= abs(( getattr(items[-1].bbox(), ae)
                         - getattr(items[-1].bbox(), a2) )*pv.U)

        if len(items)>2:

            # take out the length of each item in this dir
            for item in items[1:-1]:
                # abs? XX
                space -= abs(( getattr(item.bbox(), a2)
                                - getattr(item.bbox(), a1) )*pv.U)

            ds = space/float((len(items)-1))

            for ii in range(1, len(items)-1):
                p1 = getattr(items[ii-1].bbox(), a1)
                p2 = getattr(items[ii].bbox(), a2)

                mv = (ds-(p2-p1)*pv.U)*pv.U
                items[ii].move(mv)
            
    if isinstance(items, Group):
        items.recalc_size()

        # for convenience ..
        return items
    else:
        # create a group (though it may not be used)
        # for convenience
        return apply(Group, items) 


# -------------------------------------------------------------------------

PSMacros = """%%BeginResource: procset pyscript
/PyScriptDict 10 dict def PyScriptDict begin
%show text with kerning if supplied
/kernshow { 0 2 2 counttomark 2 sub { -2 roll } for
counttomark 2 idiv { exch show 0 rmoveto} repeat pop
} bind def
/BeginEPSF { 
/b4_Inc_state save def 
/dict_count countdictstack def 
/op_count count 1 sub def      
userdict begin                 
/showpage { } def              
0 setgray 0 setlinecap         
1 setlinewidth 0 setlinejoin
10 setmiterlimit [ ] 0 setdash newpath
/languagelevel where           
{pop languagelevel             
1 ne                           
{false setstrokeadjust false setoverprint
} if
} if
} bind def
/EndEPSF { 
count op_count sub {pop} repeat 
countdictstack dict_count sub {end} repeat
b4_Inc_state restore
} bind def
/PyScriptStart {} def
/PyScriptEnd {} def
/showpage {} def
end
%%EndResource
"""


def collecttex(objects, tex = []):
    """
    Collect the TeX objects in the order they're rendered

    @param objects: the objects to check for being TeX objects
    @type objects: object

    @param tex: list of TeX objects
    @type tex: list
    
    Used by render()
    @return: list of TeX objects
    @rtype: list
    """
    for obj in objects:
        if isinstance(obj, TeX):
            tex.append(obj)
        elif isinstance(obj, Group) and not isinstance(obj, Eps):
            tex = collecttex(obj.objects, tex)
    return tex


def TeXstuff(objects):
    '''
    Get the actual postscript code and insert it into
    the tex objects. Also grab prolog

    used by render()
    @return: TeX objects prolog from dvips
    @rtype: string
    '''

    objects = collecttex(objects)
    if len(objects) == 0:
        return ""
    
    print "Collecting postscript for TeX objects ..."
    
    fname = "temp.tex"
    fp = open(fname, "w")
    fp.write(defaults.tex_head)
    for tex in objects:
        fp.write('\\special{ps:PyScriptStart}\n')
        fp.write("{%s}\n"%tex.text)
        fp.write('\\special{ps:PyScriptEnd}\n')
        fp.write('\\newpage\n')
    fp.write(defaults.tex_tail)
    fp.close()

    ##os.system(defaults.tex_command%file+'> pyscript.log 2>&1')
    #(fi,foe) = os.popen4(defaults.tex_command%file)
    #fi.close()
    #sys.stderr.writelines(str(foe.readlines()))
    #sys.stderr.write('\n')
    #foe.close()

    # TeX it twice ... only pay attention to the 2nd one
    os.popen(defaults.tex_command % fname)
    foe = os.popen(defaults.tex_command % fname)
    sys.stderr.write(foe.read(-1))
    sys.stderr.write('\n')
    # Help the user out by throwing the latex log to stderr
    if os.path.exists("%s.log" % fname):
        fp = open("%s.log" % fname, 'r')
        sys.stderr.write(fp.read(-1))
        fp.close()
    if foe.close() is not None:
        raise RuntimeError, "Latex Error"
    
    (fi, foe) = os.popen4("dvips -q -tunknown %s -o temp.ps temp.dvi"%\
            defaults.dvips_options)
    fi.close()
    err = foe.read(-1)
    sys.stderr.write(err)
    sys.stderr.write('\n')
    foe.close()
    if len(err)>0:
        raise RuntimeError, "dvips Error"

    fp = open("temp.ps", "r")
    ps = fp.read(-1)
    fp.close()

    # Now rip it appart .. use string rather than re which
    # gets caught on recursion limits
    
    # grab prolog dvips dosn't use %%BeginProlog!
    start = ps.index("%%EndComments")+14
    end = ps.index("%%EndProlog")
    prolog = ps[start:end]

    tt = []
    pos1 = end
    while 1:
        pos1 = ps.find("PyScriptStart", pos1)
        if pos1 < 0:
            break
        pos2 = ps.find("PyScriptEnd", pos1)
        
        tt.append("TeXDict begin 1 0 bop\n%s\neop end"%ps[pos1+14:pos2])
        pos1 = pos2
    
    assert len(tt) == len(objects)

    for ii in range(len(objects)):
        objects[ii].bodyps = tt[ii]


    # remove showpage
    # no we don't ... this kills some things
    #defs=re.sub("(?m)showpage","",defs)

    # Cant's seem to set a paper size of 0x0 without tinkering with
    # dvips config files. We need this so it matches with -E offsets.
    # the closest is the 'unknown' paper format which unfortunately
    # introduces some postript code that uses 'setpageparams' and 
    # 'setpage' for size. Can't seem to overide
    # those def easily so hunt out that code and kill it:
    # defs=re.sub("(?s)statusdict /setpageparams known.*?if } if","",defs)

    return prolog


class Eps(Group):
    '''
    Create the EPS
    @cvar pad: extra padding around EPS bbox to absorb effect
      of line thicknesses etc (in pt)
    '''
    
    pad = 2
    
    def __init__(self, *objects, **options):
        '''
        Override to allow fixed dimentions to be set

        @param objects: list of Eps objects
        @type objects: list

        @param options: dictionary of attributes
        @type options: dict
        '''
        args = (self, )+objects
        Group.__init__(self, **options)

        b = self.bbox()

        # width and height have special meaning here
        if options.has_key('width') and options.has_key('height'):
            sx = options['width']/float(b.width)
            sy = options['height']/float(b.height)
            del options['width']
            del options['height']
        elif options.has_key('width'):
            sx = sy = options['width']/float(b.width)
            del options['width']
        elif options.has_key('height'):
            sx = sy = options['height']/float(b.height)
            del options['height']
        else:
            sx = sy = 1
            
        self.scale(sx, sy)

        # initialise again since scaling must be applied BEFORE
        # any positioning args (width/height will be correct now)
        Group.__init__(*args, **options)
        
    
    def write(self, fp, title = "PyScriptEPS"):
        '''
        write a self-contained EPS file

        @param fp: the filehandle to write to
        @type fp: file object

        @param title: the title of the postscript to write
        @type title: string
        '''
        # --- Header Comments ---
        
        # We conform DSC 3.0...
        fp.write("%!PS-Adobe-3.0 EPSF-3.0\n")
        fp.write("%%%%BoundingBox: %d %d %d %d\n"%self.bbox_pp())
        fp.write("%%%%Creator: PyScript %s\n"%version)
        fp.write("%%%%CreationDate: %s\n"%time.ctime(time.time()))
        # Color() can use CMYK ... don't need this with level 2 spec below
        # fp.write("%%Extensions: CMYK\n")
        # we've used some level 2 ops:
        fp.write("%%LanguageLevel: 2\n")
        fp.write("%%%%Title: %s\n"%title)
        # Say it's a single page:
        fp.write("%%Pages: 1\n") 
        fp.write("%%EndComments\n")

        # --- Prolog ---
        fp.write("%%BeginProlog\n")
        fp.write(PSMacros)
        # insert TeX prolog & fonts here
        fp.write(TeXstuff(self))
        fp.write("%%EndProlog\n")

        # --- Setup ---
        fp.write("%%BeginSetup\n")
        fp.write("PyScriptDict begin\n")
        fp.write('/uu {%f mul} def '%defaults.units)
        #fp.write('%s\n'%defaults.fg)
        fp.write('%g setlinewidth \n'%defaults.linewidth)
        fp.write('%d setlinecap %d setlinejoin %g setmiterlimit\n'%\
                 (defaults.linecap,
                  defaults.linejoin,
                  defaults.miterlimit
                  ))
        if defaults.dash is not None:
            fp.write(defaults.dash+"\n")
        fp.write("end\n")
        fp.write("%%EndSetup\n")

        # --- Code ---
        fp.write("%%Page: 1 1\n")
        fp.write("PyScriptDict begin\n")
        fp.write(self.prebody())
        fp.write(Group.body(self))
        fp.write(self.postbody())
        fp.write("end %PyScriptDict\n") # does this go after Trailer?
        fp.write("showpage\n") # where should this go?

        # --- Trailer ---
        fp.write("%%Trailer\n") 
        fp.write("%%EOF\n") 
        
    def __str__(self):
        '''
        Eps file with correct pre- and post- code for embeding
        @rtype: string
        '''
        out = cStringIO.StringIO()

        # NB this is slightly different to Epsf 
        # transformations are done in write() here.
        # in Epsf, when the file is written the origin 
        # is adjusted so it can be printed easily and 
        # tranformations have to be done in __str__ too.
        # hence Epsf overrides body here we're override __str__
        out.write("BeginEPSF\n")
        out.write("%%BeginDocument: PyScriptEPS\n")
        self.write(out)
        out.write("%%EndDocument\n")
        out.write("EndEPSF\n")
        
        return out.getvalue()

    def bbox_pp(self):
        """
        Get the bounding box of the Eps object (with some padding)
        """
        # Grab the groups bounding box
        b = self.bbox()
        
        p = self.pad

        x1 = round(b.sw[0]*defaults.units)-p
        y1 = round(b.sw[1]*defaults.units)-p

        x2 = round(b.ne[0]*defaults.units)+p
        y2 = round(b.ne[1]*defaults.units)+p
         
        return x1, y1, x2, y2
        
# -------------------------------------------------------------------------


class Page(Group):
    '''
    A postscript page
    @cvar size: The paper size, eg "a4"
    @cvar orientation: The paper orientation, "Portrait"/"Landscape"
    @cvar label: page number label
    '''

    size = 'a4'
    orientation = "Portrait"
    label = None
    
    # From gs_statd.ps which defines the paper sizes for gs:
    # Define various paper formats.  The Adobe documentation defines only these:
    # 11x17, a3, a4, a4small, b5, ledger, legal, letter, lettersmall, note.
    # These procedures are also accessed as data structures during 
    # initialization, so the page dimensions must be the first two elements 
    # of the procedure.

    PAPERSIZES = {
        # Page sizes defined by Adobe documentation
        "11x17":(792, 1224),
        # a3 see below
        # a4 see below
        # a4small should be a4 with an ImagingBBox of [25 25 570 817].
        # b5 see below
        "ledger":(1224, 792), # 11x17 landscape
        "legal":(612, 1008),
        "letter":(612, 792),
        # lettersmall should be letter with an ImagingBBox of [25 25 587 767].
        # note should be letter (or some other size) with the ImagingBBox
        # shrunk by 25 units on all 4 sides.

        # ISO standard paper sizes
        "a0":(2380, 3368),
        "a1":(1684, 2380),
        "a2":(1190, 1684),
        "a3":(842, 1190),
        "a4":(595, 842),
        "a5":(421, 595),
        "a6":(297, 421),
        "a7":(210, 297),
        "a8":(148, 210),
        "a9":(105, 148),
        "a10":(74, 105),
        # ISO and JIS B sizes are different....
        # first ISO
        "b0":(2836, 4008),
        "b1":(2004, 2836),
        "b2":(1418, 2004),
        "b3":(1002, 1418),
        "b4":(709, 1002),
        "b5":(501, 709),
        "b6":(354, 501),
        "jisb0":(2916, 4128),
        "jisb1":(2064, 2916),
        "jisb2":(1458, 2064),
        "jisb3":(1032, 1458),
        "jisb4":(729, 1032),
        "jisb5":(516, 729),
        "jisb6":(363, 516),
        "c0":(2600, 3677),
        "c1":(1837, 2600),
        "c2":(1298, 1837),
        "c3":(918, 1298),
        "c4":(649, 918),
        "c5":(459, 649),
        "c6":(323, 459),
        # U.S. CAD standard paper sizes
        "arche":(2592, 3456),
        "archd":(1728, 2592),
        "archc":(1296, 1728),
        "archb":(864, 1296),
        "archa":(648, 864),
        # Other paper sizes
        "flsa":(612, 936), # U.S. foolscap
        "flse":(612, 936), # European foolscap
        "halfletter":(396, 612),
        # Screen size (NB this is 2mm too wide for A4):
        "screen":(600, 800),
        }
    
    def area(self):
        '''
        return an area object same size as page in default units
        @rtype: Area
        '''
        d1, d2, w, h = self.bbox_pp()
        
        w, h = w/float(defaults.units), h/float(defaults.units)
        
        return Area(sw = P(0, 0), width = w, height = h)
        
    def recalc_size(self):
        """
        Recalculate the size of the Page object
        """
        # disable this as we're always the same size
        pass
    
    def bbox(self):
        """
        Return the bounding box of the Page object as a Bbox object
        """
        
        area = self.area()
        return Bbox(sw = area.sw, width = area.width, height = area.height)

    def bbox_pp_raw(self):
        """
        Return the raw string of the bounding box of the Page object
        """

        w, h = self.PAPERSIZES[self.size.lower()]
        return 0, 0, w, h

    def bbox_pp(self):
        """
        Return the bounding box of the Page object
        """
        
        w, h = self.PAPERSIZES[self.size.lower()]
        if self.orientation.lower() == "landscape": 
            h, w = w, h
        
        return 0, 0, w, h
    
    def write(self, fp, number):
        '''
        write a self-contained PS Page

        @param fp: the filehandle to write the postscript to
        @type fp: filehandle object

        @param number: the number of the page to write
        @type number: int
        '''
        
        label = self.label
        if label is None: 
            label = str(number)
        fp.write("%%%%Page: %s %d\n"%(label, number))

        orientation = self.orientation.capitalize()
        if orientation not in ("Portrait", "Landscape"):
            raise ValueError, "Don't understand page orientation"
        
        fp.write("%%%%PageOrientation: %s\n" % orientation)

        w, h = self.PAPERSIZES[self.size.lower()]
        fp.write("%%%%PageBoundingBox: %d %d %d %d\n"%\
                 (0, 0, w, h))

        # --- Setup ---
        fp.write("%%BeginPageSetup\n")
        fp.write("%%%%BeginFeature: *PageSize %s\n"%self.size)
        # The orientation of w & h should make no diff here acording to specs
        fp.write("<</PageSize [%d %d] /ImagingBBox null>> setpagedevice\n"%\
                (w, h))
        fp.write("%%EndFeature\n")
        # remember the page graphics state
        fp.write("/pgsave save def\n")
        # rotate if we're landscape 
        if orientation == "Landscape":
            fp.write("90 rotate 0 -%d translate\n"%w)
        fp.write("%%EndPageSetup\n")
    
        fp.write("PyScriptDict begin\n")
        fp.write(self.prebody())
        fp.write(Group.body(self))
        fp.write(self.postbody())
        fp.write("end %PyScriptDict\n") # does this go after Trailer?

        # restore the graphics state and show the page
        fp.write("pgsave restore showpage\n")

        
class Pages(Group):
    '''
    Class to hold pages and write out a multi-page postsript document
    '''
    
    def write(self, fp, title = "PyScriptPS"):
        '''
        write the Pages

        @param fp: filehandle to write the postscript to
        @type fp: filehandle object

        @param title: the title to use in the postscript
        @type title: string
        '''
        fp.write("%!PS-Adobe-3.0\n")
        fp.write("%%%%Creator: PyScript %s\n"%version)
        fp.write("%%%%CreationDate: %s\n"%time.ctime(time.time()))
        # Color() can use CMYK ... don't need this with level 2 spec below
        # fp.write("%%Extensions: CMYK\n")
        # we've used some level 2 ops:
        fp.write("%%LanguageLevel: 2\n")
        fp.write("%%%%Title: %s\n"%title)

        # If all the pages are orientated the same, give a global orientation
        # Combine boundingboxes to a highwater-mark global boundingbox
        orientation = self[0].orientation
        orient = True
        x1, y1, x2, y2 = self[0].bbox_pp_raw()
        for page in self:
            orient = orient and (page.orientation == orientation)
            x1t, y1t, x2t, y2t = page.bbox_pp_raw()
            x1 = min(x1, x1t)
            y1 = min(y1, y1t)
            x2 = max(x2, x2t)
            y2 = max(y2, y2t)
        
        fp.write("%%%%BoundingBox: %d %d %d %d\n"%(x1, y1, x2, y2))            
        if orient:
            fp.write("%%%%Orientation: %s\n"%orientation)
        # Say it's a single page:
        fp.write("%%%%Pages: %d\n"%len(self)) 
        fp.write("%%EndComments\n")

        # --- Prolog ---
        fp.write("%%BeginProlog\n")
        fp.write(PSMacros)
        # insert TeX prolog & fonts here
        fp.write(TeXstuff(self))
        fp.write("%%EndProlog\n")

        # --- Setup ---
        fp.write("%%BeginSetup\n")
        fp.write("PyScriptDict begin\n")
        fp.write('/uu {%f mul} def '%defaults.units)
        fp.write('%g setlinewidth \n'%defaults.linewidth)
        fp.write('%d setlinecap %d setlinejoin %g setmiterlimit\n'%\
                 (defaults.linecap,
                  defaults.linejoin,
                  defaults.miterlimit,
                  ))
        if defaults.dash is not None:
            fp.write(defaults.dash+"\n")
        fp.write("end\n")
        fp.write("%%EndSetup\n")

        for pp in range(len(self)):
            self[pp].write(fp, pp+1)
        
        # --- Trailer ---
        fp.write("%%Trailer\n") 
        fp.write("%%EOF\n") 

    def validate(self, obj):
        """
        Check that the objects are able to be added to the Page object

        @param obj: object to check validity of
        @type obj: object
        """
        
        if not isinstance(obj, Page):
            raise TypeError, "Can only add Page to %s" % str(self.__class__)
        
# vim: expandtab shiftwidth=4:

