# Copyright (C) 2002-2006 Alexei Gilchrist and Paul Cochrane
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
# Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307,
# USA.

# $Id: qi.py,v 1.12 2006/03/03 10:58:40 paultcochrane Exp $

### = we have the equivalent
#   = not yet implemented

#  zero    - replaces qubit with |0> state
#  discard - discard qubit (put "|" vertical bar on qubit wire)
#  slash   - put slash on qubit wire
#  Utwo    - two-qubit operation U
#  SS      - two-qubit gate, symmetric; open squares

# -----------------------------------------------------------------------------
'''
Package for drawing quantum circuit diagrams
'''

__revision__ = '$Revision: 1.12 $'

from pyscript import Rectangle, Color, Circle, Dot, P, Path, TeX, \
        Distribute, C, U
from pyscript.groups import Group

from types import IntType, FloatType, ListType, TupleType, StringType

# -------------------------------------------------------------------------
class Boxed(Group, Rectangle):
    '''
    Draws a box around an object,
    the box can be placed acording to standard Area tags

    @cvar pad: padding around object
    @type pad: float

    @cvar width: overide the width of the box
    @type width: float

    @cvar height: override the height of the box
    @type height: float
    '''

    fg = Color(0)
    bg = Color(1)
    pad = 0.2

    def __init__(self, obj, **options):
        
        Rectangle.__init__(self, **options)
        Group.__init__(self, **options)

        bbox = obj.bbox()

        w = bbox.width+2*self.pad
        h = bbox.height+2*self.pad

        self.width = options.get('width', w)
        self.height = options.get('height', h)

        self.append(
            Rectangle(width=self.width, height=self.height,
                      bg=self.bg, fg=self.fg,
                      c=obj.c,
                      r=self.r, linewidth=self.linewidth, dash=self.dash),
            obj,
            )

# -------------------------------------------------------------------------
class Circled(Group, Circle):
    '''
    Draws a circle around an object,

    @cvar pad: padding around object
    @cvar r: overide the radius of the circle
    
    '''

    fg = Color(0)
    bg = Color(1)
    pad = 0.1

    def __init__(self, obj, **options):
        
        Circle.__init__(self, **options)
        Group.__init__(self, **options)

        bbox = obj.bbox()

        w = bbox.width+2*self.pad
        h = bbox.height+2*self.pad

        self.r = options.get('r', max(w, h)/2.)

        self.append(
            Circle(r=self.r,
                   bg=self.bg, fg=self.fg,
                   c=obj.c,
                   linewidth=self.linewidth, dash=self.dash),

            obj,
            )


# -------------------------------------------------------------------------
class Gate(Group):
    """
    Gate class
    """

    control = None
    target = None
    
    dot_r = .1
   
    # target object get set in __init__
    targetobj = None
    controlobj = None


    def __init__(self, tobj, **options):
        
        Group.__init__(self, **options)

        # XXX should we take a copy???
        self.targetobj = tobj.copy()

        if self.controlobj is None:
            self.controlobj = Dot(r=self.dot_r)

        # fix up target and control points    
        if type(self.target) in (type(()), type([])):
            pass
        elif isinstance(self.target, P):
            self.target = [self.target]
        elif self.target is None:
            self.target = [P(0, 0)]
        else:
            raise ValueError, "don't understand target structure for Gate"
            
        if type(self.control) in (type(()), type([])):
            pass
        elif isinstance(self.control, P):
            self.control = [self.control]
        elif self.control is None:
            self.control = []
        else:
            raise ValueError, "don't understand control structure for Gate"

        self._make()

    def settarget(self, *p):
        """
        Sets the target qubit
        """
        
        self.target = p
        self._make()
        
        
    def setcontrol(self, *p):
        """
        Sets the control qubit
        """

        self.control = p
        self._make()
        
    def _make(self):
        """
        Makes the gate
        """

        self.clear()
        
        # calc average target point
        tp = self.target[0]
        if len(self.target)>1:
            for tt in self.target[1:]:
                tp = tp+tt
            tp = tp/float(len(self.target))
        
        self.targetobj.c = tp

        #XXX should target adjust height here

        # add controls 
        for cc in self.control:
            self.append(Path(tp, cc))
            self.controlobj.c = cc
            self.append(self.controlobj.copy())

        self.append(self.targetobj)
            

# -------------------------------------------------------------------------
class GateBoxedTeX(Gate):
    """
    Gate with TeX object enclosed in a Box
    """
    def __init__(self, tex, **options):
        Gate.__init__(self, Boxed(TeX(tex)) , **options)

GBT = GateBoxedTeX
# -------------------------------------------------------------------------
class GateCircledTeX(Gate):
    """
    Gate with TeX object enclosed in a Circle
    """
    def __init__(self, tex, **options):
        Gate.__init__(self, Circled(TeX(tex)) , **options)

GCT = GateCircledTeX
# -------------------------------------------------------------------------
def H(**options): 
    """
    Hadamard gate
    """
    return GBT('$H$', **options)

def X(**options): 
    """
    X gate
    """
    return GBT('$X$', **options)

def Y(**options): 
    """
    Y gate
    """
    return GBT('$Y$', **options)

def Z(**options): 
    """
    Z gate
    """
    return GBT('$Z$', **options)

def S(**options): 
    """
    @todo: ask Alexei what this gate is
    """
    return GBT('$S$', **options)

def T(**options): 
    """
    @todo: ask Alexei what this gate is
    """
    return GBT('$T$', **options)

def RX(arg, **options): 
    """
    @todo: ask Alexei what this gate is
    """
    return GCT('$R_x(%s)$'%arg, **options)

def RY(arg, **options): 
    """
    @todo: ask Alexei what this gate is
    """
    return GCT('$R_y(%s)$'%arg, **options)

def RZ(arg, **options): 
    """
    @todo: ask Alexei what this gate is
    """
    return GCT('$R_z(%s)$'%arg, **options)

# -------------------------------------------------------------------------
def NOT(**options):
    """
    NOT gate
    """
    r = .2
    return Gate(
        Group(Circle(r=r), Path(P(0, r), P(0, -r)), Path(P(-r, 0), P(r, 0))),
        **options)
# -------------------------------------------------------------------------
def CSIGN(**options):
    """
    Controlled sign gate
    """
    return Gate(Dot(r=Gate.dot_r), **options)

ZZ = CSIGN
# -------------------------------------------------------------------------
def SWAP(**options):
    """
    Swap gate
    """
    x = Group(Path(P(-.1, .1), P(.1, -.1)), Path(P(-.1, -.1), P(.1, .1)))
    options['controlobj'] = options.get('controlobj', x)
    return Gate(x, **options)
    #return Gate(x, **options)
# -------------------------------------------------------------------------

# XXX make this a class!
class ClassicalPath:
    """
    A classical path
    """
    pass

def classicalpath(*paths):
    '''
    @return: classical path
    @param paths: 1 or more Path() objects
    '''
    g = Group()

    for path in paths:
        g.append(path.copy(linewidth=2, fg=Color(0)))

    # reuse these paths
    for path in paths:
        g.append(path(linewidth=1, fg=Color(1)))

    return g


# -------------------------------------------------------------------------


class NoWire(Group):
    """
    Class representing no wire in diagram
    """
    def __init__(self, **options):
        Group.__init__(self, **options)

    def set(self, y, e, w):
        """
        Set the east, west and y postions of the NoWire
        """
        return self
    
class QWire(NoWire):
    """
    Class representing a quantum wire
    """
    
    fg = Color(0)
    linewidth = None
    dash = None

    def set(self, y, e, w):
        """
        Set the east, west and y postions of the QWire
        """
        path = Path(P(w, y), P(e, y),
                fg=self.fg, linewidth=self.linewidth, dash=self.dash)
        self.append(path)
        return self

class CWire(QWire):
    """
    Class representing a classical wire
    """
    def set(self, y, e, w):
        """
        Set the east, west and y postions of the CWire
        """
        path = Path(P(w, y), P(e, y),
                fg=self.fg, linewidth=self.linewidth, dash=self.dash)
        
        self.append(classicalpath(path))
        return self
    

class Assemble(Group):
    """
    Class representing the assembled objects in diagram/circuit
    """

    wirespacing = 1
    gatespacing = .1
   
    wires = []
    hang = .2
    starthang = hang
    endhang = hang

   
    def __init__(self, *gates, **options):
        self.starthang = options.get('hang', self.hang)
        self.endhang = options.get('hang', self.hang)
        Group.__init__(self, **options)
        
        sequence = list(gates)
        
        # parse the list ...
        wires = []
        named = {}
        basetime = 0
        while len(sequence) > 0:
            # the gate ...
            gate = sequence.pop(0)

            # the target ...
            t = sequence.pop(0)
            wires.append(t)

            # optional controls ...
            if len(sequence) > 0 and \
                    isinstance(sequence[0], (IntType, FloatType)):
                c = sequence.pop(0)
                wires.append(c)
            elif len(sequence) > 0 and \
                    isinstance(sequence[0], (TupleType, ListType)):
                c = sequence.pop(0)
                wires.extend(c)
            else:
                c = None

            g = self.setgate(gate, t, c)

            # optional time label ...
            if len(sequence)>0 and isinstance(sequence[0], StringType):
                l = sequence.pop(0)
                if named.has_key(l):
                    # group already exists
                    named[l].append(g)
                else:
                    # create new named group
                    G = named[l] = Group(g)
                    self.append(G)
            else:
                self.append(g)
       
        L = 0
        for ii in self:
            L += ii.width+self.gatespacing
        L -= self.gatespacing

        # XXX add distribute's options
        Distribute(self, p1=P(0, 0), p2=P(L, 0))            
        self.recalc_size()

        # XXX should check wires are ints

        # add wires ...
        x0 = self.w.x-self.starthang
        x1 = self.e.x+self.endhang
        if len(self.wires) == 0:
            for w in range(-min(wires), -max(wires)-1, -1):
                wire = QWire().set(w*self.wirespacing, x0, x1)
                self.insert(0, wire)
                self.wires.append(wire)
            print self.wires
        else:
            #w=-int(min(wires))
            w = -1
            wirestmp = []
            for wire in self.wires:
                # if it already an instance this will have no effect
                # otherwise create an instance
                wire = apply(wire, ())
                wire.set(w*self.wirespacing, x0, x1)
                self.insert(0, wire)
                wirestmp.append(wire)
                w -= 1
            self.wires = wirestmp

        
    def setgate(self, gate, target, control=None):
        """
        Set the gate in the assembly
        """

        # if it already an instance this will have no effect
        # otherwise create an instance
        gate = apply(gate)
        
        # XXX multi target qubits
        gate.settarget(P(0, -target))
       
        if isinstance(control, (IntType, FloatType)):
            gate.setcontrol(P(0, -control))
        elif isinstance(control, (TupleType, ListType)):
            tmp = []
            for cc in control:
                tmp.append(P(0, -cc))
            apply(gate.setcontrol, tmp)

        return gate

# -------------------------------------------------------------------------
# misc other items
# -------------------------------------------------------------------------

class Meter(Group):
    """
    A meter object as in Mike'n'Ike
     
    """
    height = .7
    width = 1.8*height

    angle = 45
    bg = Color(1)
    mcolor = Color(.8)
    
    def __init__(self, **args):
        Group.__init__(self, **args)

        h = self.height
        w = self.width
		
        
        self.append(Rectangle(width=1.8*h, height=h, bg=self.bg))
        
        p = Path(
                P(.1, .1), C(0, 0), P(w-.1, .1),
                P(w-.2, .1), C(0, 0), P(.2, .1),
                closed=1, bg=self.mcolor, fg=None)
        
        self.append(p,
            Path(P(w/2., .1), U(self.angle, h*.9)),
            )

# -------------------------------------------------------------------------
class Detector(Group):
    '''
    A D shaped detector, can be given an object to surround
    '''

    height = .8
    width = height/2.
    bg = Color(1)
    fg = Color(0)
    pad = .1
	
    def __init__(self, object=None, **options):
        if object is not None:
            # use the object's boundingbox when width and height not supplied
            bb = object.bbox()
            w = bb.width+2*self.pad
            h = bb.height+2*self.pad

            self.width = options.get("width", max(w, self.width))
            self.height = options.get("height", max(h, self.height))
        Group.__init__(self, **options)

        if self.width>self.height:
            p = Path(
                P(0, 0), P(0, self.height),
                P(self.width-self.height/2., self.height), C(90, 0),
                P(self.width, self.height/2.), C(180, 90),
                P(self.width-self.height/2., 0),
                closed=1)
        else:
			
            p = Path(
                P(0, 0), P(0, self.height),  C(90, 0),
                P(self.width, self.height/2.), C(180, 90),
                closed=1)
		
        p(bg=options.get("bg", self.bg), fg=options.get("fg", self.fg))

        self.append(p)
        if object is not None:
            # object looks better if it's slightly off centre
            # since one side is curved. pad/3 is about right
            object.c = P(self.width/2.-self.pad/3., self.height/2.)
            self.append(object)

# vim: expandtab shiftwidth=4:
