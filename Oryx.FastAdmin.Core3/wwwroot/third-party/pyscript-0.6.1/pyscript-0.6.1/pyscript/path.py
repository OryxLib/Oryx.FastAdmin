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

# $Id: path.py,v 1.25 2006/05/16 05:38:31 aalexei Exp $

"""
The Path module
"""

__revision__ = '$Revision: 1.25 $'

#from pyscript.defaults import defaults
from math import sqrt, pi, sin, cos
from pyscript.vectors import P, Bbox, U, Identity, R
from pyscript.base import Color
from pyscript.objects import AffineObj
from pyscript.arrowheads import ArrowHead
import cStringIO

# -------------------------------------------------------------------------
# Pathlettes ... components of path, not used by themselves
# -------------------------------------------------------------------------

class _line(object):
    '''
    A line pathlette
    '''
    s = None
    e = None

    def __init__(self, s, e):
        object.__init__(self)
        self.s = s
        self.e = e

    def _get_start(self):
        """
        return start point
        """
        return self.s
    start = property(_get_start)

    def _get_end(self):
        """
        return end point
        """
        return self.e
    end = property(_get_end)

    def _get_length(self):
        """
        Get the length of the pathlette
        """
        return (self.e-self.s).length
    length = property(_get_length)

    def P(self, f):
        '''
        return point at fraction f of length
        '''
        return (self.s+(self.e-self.s)*f)

    def tangent(self, f):
        '''
        return angle of tangent of curve at fraction f of length
        '''
        return (self.e-self.s).arg


    def body(self):
        """
        Return the postscript body
        """
        return '%s lineto\n' % self.e

    def bbox(self, itoe = Identity):
        """
        Return the bounding box
        """

        p0 = itoe(self.s)
        p1 = itoe(self.e)

        x0 = min(p0[0], p1[0])
        x1 = max(p0[0], p1[0])
        y0 = min(p0[1], p1[1])
        y1 = max(p0[1], p1[1])

        return Bbox(sw = P(x0, y0), width = x1-x0,
                    height = y1-y0)

# -------------------------------------------------------------------------

class _bezier(object):
    '''
    A Bezier pathlette
    '''
    s = None
    e = None
    cs = None
    ce = None
    length = None
    
    TOL = None #tolerance for linearising

    def __init__(self, s, cs, ce, e, TOL = 2e-3, temporary = False):
        object.__init__(self)
        self.s = s # start
        self.e = e # end
        self.cs = cs # start control
        self.ce = ce # end control
        self.TOL = TOL
        
        # for efficiency don't do this unless we intend to
        # keep this pathlette
        if not temporary:
            self._points = self.straighten()
            self.set_length()

    def _is_straight(self):
        '''
        is this curve straight?
        '''

        L1 = (self.cs-self.s).length+\
          (self.ce-self.cs).length+\
          (self.e-self.ce).length
        L2 = (self.e-self.s).length
        
        if abs(L1-L2)/float(L1) <= self.TOL:
            return True
        else:
            return False

    def straighten(self):
        """
        Straighten the bezier curve
        """
        
        if self._is_straight():
            return (self.s, self.e)
        else: 
            c1, c2 = self._bisect(temporary = True)
            
            return (c1.straighten()+c2.straighten())
        
    def _bisect(self, t = .5, temporary = False):
        '''
        Divide this bezier into two
        '''
        p01   = self.s * (1-t) + self.cs * t
        p12   = self.cs * (1-t) + self.ce * t
        p23   = self.ce * (1-t) + self.e * t
        p012  = p01  * (1-t) + p12  * t
        p123  = p12  * (1-t) + p23  * t
        p0123 = p012 * (1-t) + p123 * t

        return (_bezier(self.s.copy(), 
            p01, p012, p0123, temporary = temporary), 
            _bezier(p0123.copy(), 
                p123, p23, self.e.copy(), temporary = temporary)) 

    
    def set_length(self):
        """
        Set the length of the bezier curve
        """
        L = 0
        p0 = self.s
        for p in self._points:
            L += (p-p0).length
            p0 = p
        self.length = L

    def body(self):
        """
        Return the postscript body of the object
        """
        return '%s %s %s curveto\n' % (self.cs, self.ce, self.e)


    def _t(self, t):
        '''
        Return point on curve parametrised by t [0-1]
        This is exact
        '''
        a1 = 3*(self.cs-self.s)
        a2 = 3*(self.s-2*self.cs+self.ce)
        a3 = -self.s+3*self.cs-3*self.ce+self.e
        
        return a3*t**3+a2*t**2+a1*t+self.s

    def _get_start(self):
        """
        return start point
        """
        return self.s
    start = property(_get_start)

    def _get_end(self):
        """
        return end point
        """
        return self.e
    end = property(_get_end)

    def P(self, f):
        '''
        return point on curve at fraction f of length
        '''
        assert 0 <= f <= 1

        #if self.length is None:
        #    self._cache()
        
        if f == 0:
            return self.s
        elif f == 1:
            return self.e
        
        Lf = self.length*f

        L = 0
        p0 = self.s
        for p in self._points:
            l = (p-p0).length
            if L+l >= Lf: 
                break
            L += l
            p0 = p

        # XXX Add a correction here so it's actually on the curve!
        # Newton Rapson?

        return (p-p0).U*(Lf-L) +p0

    def tangent(self, f):
        '''
        return angle of tangent of curve at fraction f of length
        '''
        assert 0 <= f <= 1

        if f == 0:
            return (self.cs-self.s).arg
        elif f == 1:
            return (self.e-self.ce).arg
        
        Lf = self.length*f

        L = 0
        p0 = self.s
        for p in self._points:
            l = (p-p0).length
            if L+l >= Lf: 
                break
            L += l
            p0 = p

        # XXX Add a correction here so it's actually on the curve!
        # Newton Rapson?

        return (p-p0).arg

    def bbox(self, itoe = Identity):
        """
        Return the bounding box of the object
        """
        # run through the list of points to get the bounding box
        
        #if self.length is None:
        #    self._cache()

        p0 = itoe(self.s)
        x0, y0 = p0
        x1, y1 = p0

        for p in self._points:
        
            p1 = itoe(p)

            x0 = min(x0, p1[0])
            x1 = max(x1, p1[0])
            y0 = min(y0, p1[1])
            y1 = max(y1, p1[1])

        return Bbox(sw = P(x0, y0), width = x1-x0,
                    height = y1-y0)

# -------------------------------------------------------------------------
# Curve specifier
# -------------------------------------------------------------------------

class C(object):
    """
    Specifier and generator for curves
    """

    # these params control the natural bezier
    # (they are set to the MetaPost defaults)
    _a = sqrt(2)
    _b = 1/16.
    _c = (3-sqrt(5))/2.
    
    # user parameters for curve:
    c0 = None
    c1 = None
    t0 = 1
    t1 = 1
    #curl = 1

    # this for specifing an arc
    arc = None
    
    def __init__(self, *args, **options):
        '''
        store curve parameters
        '''

        if len(args) == 1:
            raise ValueError, "C takes two arguments"
            #self.c0 = args[0]
            #self.c1 = args[0]
        
        elif len(args) == 2: 
            self.c0 = args[0]
            self.c1 = args[1]
        
        # anything supplied in keywords will override
        # the above points eg C(P(0, 0), c1=45)
        object.__init__(self)
        self(**options)

    def __call__(self, **options):
        '''
        Set a whole lot of attributes in one go
        
        eg::
          obj.set(bg=Color(.3), linewidth=2)

        @return: self 
        @rtype: self
        '''

        # first do non-property ones
        # this will raise an exception if class doesn't have attribute
        # I think this is good.
        prop = []
        for key, value in options.items():
            if isinstance(eval('self.__class__.%s'%key), property):
                prop.append((key, value))
            else:
                self.__class__.__setattr__(self, key, value)

        # now the property ones
        # (which are functions of the non-property ones)
        for key, value in prop:
            self.__class__.__setattr__(self, key, value)
                

        # for convenience return a reference to us
        return self

    def _get_fullyspecified(self):
        '''
        Is this curve fully specified (all control points)
        '''
        if self.arc is not None:
            # an arc is already fully specified
            return 1
        elif isinstance(self.c0, P) and isinstance(self.c1, P):
            # both points set
            return 1
        else:
            return 0

    fullyspecified = property(_get_fullyspecified, None)
        
    def curve(self, p0, p1 = None):
        '''
        return pathlette object corresponding to curve
        '''

        if self.arc is not None:
            # an arc
            return self.create_arc(p0)
        else:
            # a bezier
            if not self.fullyspecified:
                # fit natural curve...
                self.fit_curve(p0, p1)
            return self.create_bezier(p0, p1)
        
    def fit_curve(self, p0, p1):
        '''
        fit a natural looking spline to end slopes
        '''

        # first get the angles ...
        if type(self.c0) in [type(10), type(10.0)]:
            # turn this into a unit vector in that direction
            w0 = U(self.c0)
        elif isinstance(self.c0, R):
            # already have unit vectior
            w0 = self.c0
        elif isinstance(self.c0, P):
            # non-unit vector giving direction
            w0 = (self.c0-p0)
        else:
            raise ValueError, "Unknown control type c0"

        if type(self.c1) in [type(10), type(10.0)]:
            # turn this into a unit vector in that direction
            w1 = U(self.c1)
        elif isinstance(self.c1, R):
            # already have unit vectior
            w1 = self.c1
        elif isinstance(self.c1, P):
            # non-unit vector giving direction
            w1 = (self.c1-p1)
        else:
            raise ValueError, "Unknown control type c1"

        t = ((p1-p0).arg-w0.arg)*pi/180.
        p = -((-p1+p0).arg-w1.arg)*pi/180.

        a = self._a
        b = self._b
        c = self._c

        alpha = a*(sin(t)-b*sin(p))*(sin(p)-b*sin(t))*(cos(t)-cos(p))

        rho   = (2+alpha)/(1+(1-c)*cos(t)+c*cos(p))
        sigma = (2-alpha)/(1+(1-c)*cos(p)+c*cos(t))

        c0 = P( p0.x + rho*( (p1.x-p0.x)*cos(t)-(p1.y-p0.y)*sin(t))/(3*self.t0) ,
            p0.y + rho*( (p1.y-p0.y)*cos(t)+(p1.x-p0.x)*sin(t))/(3*self.t0) )

        c1 = P( 
                p0.x + (p1.x-p0.x)*(1-sigma*cos(p)/(3*self.t1)) -
                (p1.y-p0.y)*sigma*sin(p)/(3*self.t1) , 
                p0.y + (p1.y-p0.y)*(1-sigma*cos(p)/(3*self.t1)) + 
                (p1.x-p0.x)*sigma*sin(p)/(3*self.t1) 
                )

        # only change if we were given an angle
        if type(self.c0) in [type(10), type(10.0)]:
            self.c0 = c0
        if type(self.c1) in [type(10), type(10.0)]:
            self.c1 = c1

    def create_arc(self, centre):
        """
        Create an arc
        """
        return None

    def create_bezier(self, p0, p1):
        """
        Create a bezier curve
        """
        c0 = self.c0
        c1 = self.c1
    
        # fix up relative points:
        if isinstance(c0, R):
            c0 = p0+c0
        if isinstance(c1, R):
            c1 = p1+c1
    
        return _bezier(p0, c0, c1, p1)

# -------------------------------------------------------------------------
# Path object
# -------------------------------------------------------------------------

class Path(AffineObj):
    """
    A Path
    """
    
    fg = Color(0)
    bg = None
    linewidth = None
    linecap = None
    linejoin = None
    miterlimit = None
    dash = None
    closed = 0

    #ArrowHead instances:
    heads = []

    #_pathlettes=[]

    def __init__(self, *path, **options):

        self._pathlettes = []

        AffineObj.__init__(self, **options)

        path = list(path) # so we can use pop
        
        # first point must be, well a point
        assert isinstance(path[0], P)

        # if the last point of a closed path has been
        # skipped, add it now
        if not isinstance(path[-1], P) and self.closed:
            path.append(path[0])

        cp = path.pop(0) # current point

        while 1:
            if len(path) == 0: 
                break

            p = path.pop(0)
            if isinstance(p, R):
                p = cp+p
                self._pathlettes.append(_line(cp, p))
                cp = p
            elif isinstance(p, P):
                self._pathlettes.append(_line(cp, p))
                cp = p
            elif isinstance(p, C):
                c = p
                # Get the next point
                p = path.pop(0)
                if isinstance(p, R):
                    p = cp+p
                self._pathlettes.append(c.curve(cp, p))
                cp = p
                
            else:
                raise ValueError, "Unknown path control"

        # now add arrowheads
        heads = []
        for head in self.heads:
        
            # make a copy so this class has it's own instance 
                
            h=head.copy()

            # line colors overide arrow they blend
            # (how would a user overide this?)
            if options.has_key('fg'):
                h(fg=options['fg'])
                h(bg=options['fg'])
            
            # position it appropriately:
            h.__init__(tip=self.P(head.pos), angle=self.tangent(head.pos).arg)
    
            heads.append(h)
        self.heads = heads 
           

    def bbox(self):
        """
        Return the bounding box of the Path
        """
        b = Bbox()
        for pl in self._pathlettes:
            b.union(pl.bbox(self.itoe))

        # take into account extent of arrowheads
        for ar in self.heads:
            b.union(ar.bbox())

        return b

    def _get_start(self):
        """
        return start point
        """
        return self.itoe(self._pathlettes[0].start)
    start = property(_get_start)

    def _get_end(self):
        """
        return end point
        """
        return self.itoe(self._pathlettes[-1].end)
    end = property(_get_end)

    def _get_length(self):
        """
        Get the length of the path
        """

        l = 0
        for pl in self._pathlettes:
            l += pl.length
        return l
    length = property(_get_length)
        
    def P(self, f):
        '''
        Return the point at fraction f along the path
        '''

        assert 0 <= f <= 1

        Lf = self.length*f

        L = 0
        for pl in self._pathlettes:
            l = pl.length
            if L+l >= Lf: 
                break
            L += l
        return self.itoe(pl.P((Lf-L)/float(l)))

    def tangent(self, f):
        '''
        return tangent (unit vector) of curve at fraction f of length
        '''

        assert 0 <= f <= 1

        Lf = self.length*f

        L = 0
        for pl in self._pathlettes:
            l = pl.length
            if L+l >= Lf: 
                break
            L += l
        return U(self.itoe(U(pl.tangent((Lf-L)/float(l)))).arg)

    def body(self):
        """
        Return the postscript body of the Path
        """

        out = cStringIO.StringIO()

        if self.linewidth is not None:
            out.write("%g setlinewidth "%self.linewidth)

        if self.linecap is not None:
            out.write("%d setlinecap "%self.linecap)
            
        if self.linejoin is not None:
            out.write("%d setlinejoin "%self.linejoin)

        if self.miterlimit is not None:
            out.write("%f setmiterlimit "%self.miterlimit)

        if self.dash is not None:
            out.write(str(self.dash))

        out.write('newpath %s moveto\n'%self._pathlettes[0].start)

        for pl in self._pathlettes:
            out.write(pl.body())

        if self.closed:
            out.write(' closepath ')
        
        if self.bg is not None:
            out.write("gsave %s fill grestore\n"%self.bg)
        
        if self.fg is not None:
            out.write("%s stroke\n"%self.fg)

        for head in self.heads:
            out.write(str(head))

        return out.getvalue()

# -------------------------------------------------------------------------
# Arrow objects
# -------------------------------------------------------------------------

class Arrow(Path):
    '''
    Path object with arrow at end ... just for convenience
    '''
    #heads = [defaults.arrowhead(pos=1)]
    heads = [ArrowHead(1)]

class DoubleArrow(Path):
    """
    Path object with arrow at both ends ... just for convenience
    """
    #heads = [defaults.arrowhead(pos=1),defaults.arrowhead(pos=1,reverse=1)]
    heads = [ArrowHead(1), ArrowHead(0, reverse=1)]

# vim: expandtab shiftwidth=4:
