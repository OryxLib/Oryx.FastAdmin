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

# $Id: arrowheads.py,v 1.6 2006/05/15 11:52:15 paultcochrane Exp $

"""
Arrow heads for Path and elsewhere 
"""

__revision__ = '$Revision: 1.6 $'

from pyscript.defaults import defaults
#from math import sqrt, pi, sin, cos
from pyscript.vectors import P, Bbox # ,Identity 
from pyscript.base import Color
from pyscript.objects import AffineObj
import cStringIO

# -------------------------------------------------------------------------
# Base class: ArrowHead
# -------------------------------------------------------------------------

class ArrowHead(AffineObj):
    '''
    Arrow head object

    @cvar tip: where to position the tip of the arrow head
    @cvar angle: the direction to point

    Convenience variables modifying the head size:

    @cvar scalew: scale the width by this amount
    @cvar scaleh: scale height by this amount

    The actual shape of the arrowhead is defined by the following, distances
    are given in points
    
    @cvar start: tuple giving starting point for path
    @cvar shape: list of tuples giving arguments to postscripts curveto operator
    @cvar closed: whether to close the path or not
    @cvar fg: line color or None for no line
    @cvar bg: fill color or None for no fill
    @cvar linewidth: linewidth
    @cvar linejoin: 0=miter, 1=round, 2=bevel
    @cvar mitrelimit:  length of mitre of corners
    ''' 

    fg = Color(0)
    bg = Color(0)

    # used by Path object to set position and direction
    reverse = 0    
    pos = 1

    tip = P(0, 0)
    angle = 0

    start = (0, 0)
    # triangular share in the Golden ratio
    # positions in pixels
    shape = [(0, 0, 1.5, -4.854, 1.5, -4.854),
           (1.5, -4.854, -1.5, -4.854, -1.5, -4.854),
           (-1.5, -4.854, 0, 0, 0, 0)]

    closed = 1

    scalew = 1
    scaleh = 1

    linewidth = 0.2
    linejoin = 2 #0=miter, 1=round, 2=bevel

    # miterlimit:
    # 1.414 cuts off miters at angles less than 90 degrees.
    # 2.0 cuts off miters at angles less than 60 degrees.
    # 10.0 cuts off miters at angles less than 11 degrees.
    # 1.0 cuts off miters at all angles, so that bevels are always produced
    miterlimit = 2  

    def __init__(self, *param, **options):

        # remember this angle for when instance is copied...
        # this assumes all rotations that have been applied
        # are represented by angle and reverse
        angle0 = self.angle + self.reverse*180

        AffineObj.__init__(self, **options)

        if len(param) == 1:
            self.pos = param[0]

        sx = self.scalew
        sy = self.scaleh

        self.start = (self.start[0]*sx, self.start[1]*sy)

        shape = []
        for b in self.shape:
            shape.append((b[0]*sx, b[1]*sy, b[2]*sx, b[3]*sy, b[4]*sx, b[5]*sy))
        self.shape = shape
     
        self.rotate(self.angle+self.reverse*180-angle0)

        self.move(self.tip)
        
    def body(self):
        """
        Return the postscript body of the Path
        """

        out = cStringIO.StringIO()

        if self.linewidth is not None:
            out.write("%g setlinewidth " % self.linewidth)

        if self.linejoin is not None:
            out.write("%d setlinejoin " % self.linejoin)

        if self.miterlimit is not None:
            out.write("%f setmiterlimit " % self.miterlimit)

        out.write('newpath %g %g moveto\n' % self.start)
        for bez in self.shape:
            out.write('%g %g %g %g %g %g curveto\n' % bez)

        if self.closed:
            out.write('closepath\n')

        if self.bg is not None:
            out.write("gsave %s fill grestore\n" % self.bg)
        
        if self.fg is not None:
            out.write("%s stroke\n" % self.fg)

        return out.getvalue()

    def bbox(self):
        """
        Return the bounding box of the Path
        """
        
        # the (0,0) point:
        p0 = self.itoe(P(0, 0))
        xmax = xmin = p0.x
        ymax = ymin = p0.y

        for bez in self.shape:
       
            c1x, c1y, c2x, c2y, p2x, p2y = bez
            p1 = self.itoe(P(c1x, c1y)/float(defaults.units))
            p2 = self.itoe(P(c2x, c2y)/float(defaults.units))
            p3 = self.itoe(P(p2x, p2y)/float(defaults.units))

            xmax = max(xmax, p1.x, p2.x, p3.x)
            xmin = min(xmin, p1.x, p2.x, p3.x)
            ymax = max(ymax, p1.y, p2.y, p3.y)
            ymin = min(ymin, p1.y, p2.y, p3.y)

        return Bbox(sw=P(xmin, ymin), width=xmax-xmin, height=ymax-ymin)

# -------------------------------------------------------------------------
# Modifications
# -------------------------------------------------------------------------
# for symmetry....
class ArrowHead1(ArrowHead):
    '''
    Default Arrow head: triangular in Golden ratio
    '''
    pass

class ArrowHead2(ArrowHead):
    '''
    Similar to default but with concave base
    '''

    shape = [(0, 0, 1.5, -4.854, 1.5, -4.854),
           (0, -2, 0, -2, -1.5, -4.854),
           (-1.5, -4.854, 0, 0, 0, 0)]

class ArrowHead3(ArrowHead):
    '''
    Like default but rounded base
    '''

    shape = [(0, 0, 1.5, -4.854, 1.5, -4.854),
           (1.5*1.5, 1.5*(-4.854), 1.5*(-1.5), 1.5*(-4.854), -1.5, -4.854),
           (-1.5, -4.854, 0, 0, 0, 0)]

class ArrowHead4(ArrowHead):
    '''
    traditional line arrow
    '''
    bg = None
    start = (2, -5)
    closed = 0
    linewidth = .5
    shape = [(2, -5, 0, 0, 0, 0),
           (0, 0, -2, -5, -2, -5)]

# vim: expandtab shiftwidth=4:
