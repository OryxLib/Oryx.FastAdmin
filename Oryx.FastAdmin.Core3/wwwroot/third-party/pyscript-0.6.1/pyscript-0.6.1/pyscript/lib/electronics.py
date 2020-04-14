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

# $Id: electronics.py,v 1.14 2006/04/24 14:24:03 paultcochrane Exp $

"""
PyScript electronics objects library

Thanks to Adrian Jonstone's lcircuit macros from CTAN for the ideas and names
"""

__revision__ = '$Revision: 1.14 $'

from pyscript import P, Group, Path, Circle, C, Rectangle, Color

class Gate(Group):
    """
    Generic gate class
    """
    def __init__(self, **options):
        # initialise the base class
        Group.__init__(self, **options)

        self.height = 2.0
        self.width = 3.0
        self.angle = 0.0
        self.pinLength = 0.5
        self.fg = Color(0)
        self.bg = Color(1)

# AND gate
class AndGate(Gate):
    """
    Generates an AND gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """

    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        buff = 0.0
        pinEdgeDist = 0.1*self.height
        bodyHeight = self.height
        pl = self.pinLength
        bodyWidth = self.width - 2.0*pl
        gateBody = Group(
                Path(
                    P(pl, buff+0), 
                    P(pl, buff+bodyHeight), 
                    P(pl+bodyWidth/2.0, buff+bodyHeight)), 
                Circle(c=P(pl+bodyWidth/2.0, buff+bodyHeight/2.0), 
                    r=bodyHeight/2.0, start=0, end=180), 
                Path(
                    P(pl+bodyWidth/2.0, buff+0), 
                    P(pl, buff+0))) 
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Path(
                P(bodyWidth+pl, bodyHeight/2.0), 
                P(bodyWidth+2.0*pl, bodyHeight/2.0))

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.bbox().c)

        # now set the object to myself
        self.append(obj)

# NAND gate
class NandGate(Gate):
    """
    Generates a NAND gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """

    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        buff = 0.0
        pinEdgeDist = 0.1*self.height
        pl = self.pinLength
        bodyHeight = self.height
        bodyWidth = self.width - 2.0*pl
        rad = 0.1

        gateBody = Group(
                Path(
                    P(pl, buff+0), 
                    P(pl, buff+bodyHeight), 
                    P(pl+bodyWidth/2., buff+bodyHeight)), 
                Circle(c=P(pl+bodyWidth/2., buff+bodyHeight/2.), 
                    r=bodyHeight/2., start=0, end=180), 
                Path(
                    P(pl+bodyWidth/2., buff+0), 
                    P(pl, buff+0)))
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Group( 
                Circle(c=P(bodyWidth+pl+rad, bodyHeight/2.), r=rad), 
                Path(
                    P(bodyWidth+pl+2.*rad, bodyHeight/2.), 
                    P(bodyWidth+2.*rad+2.*pl, bodyHeight/2.)))

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # now set the object to myself
        self.append(obj)

# OR gate
class OrGate(Gate):
    """
    Generates an OR gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        pinEdgeDist = 0.1*self.height
        pl = self.pinLength
        pinBackDist = -0.08*self.width
        bodyHeight = self.height
        bodyWidth = self.width - 2.0*pl

        gateBody = Group(
                Path( 
                    P(-pinBackDist, -pinEdgeDist), 
                    C(90, 225),
                    P(1.25*bodyWidth, bodyHeight/2.0), 
                    C(-45, 90),
                    P(-pinBackDist, bodyHeight+pinEdgeDist), 
                    C(140, 40),
                    closed=1,
                    )
                )
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Path(
                gateBody.e, 
                gateBody.e+P(pl, 0))

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # now set the object to myself
        self.append(obj)

# NOR gate
class NorGate(Gate):
    """
    Generates a NOR gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        pl = self.pinLength
        pinEdgeDist = 0.1*self.height
        pinBackDist = -0.08*self.width
        bodyHeight = self.height
        bodyWidth = self.width - 2.0*pl
        rad = 0.1

        gateBody = Group(
                Path( 
                    P(-pinBackDist, -pinEdgeDist), 
                    C(90, 225),
                    P(1.25*bodyWidth, bodyHeight/2.0), 
                    C(-45, 90),
                    P(-pinBackDist, bodyHeight+pinEdgeDist), 
                    C(140, 40),
                    closed=1,
                    )
                )
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Group( 
                Circle(w=gateBody.e, r=rad), 
                Path(
                    gateBody.e+P(0.2, 0), 
                    gateBody.e+P(pl+0.2, 0)),
                )

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # nwo set the object to myself
        self.append(obj)

# XOR gate
class XorGate(Gate):
    """
    Generates an XOR gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        pinEdgeDist = 0.1*self.height
        pinBackDist = -0.08*self.width
        xBit = 0.2
        pl = self.pinLength
        bodyHeight = self.height
        bodyWidth = self.width - 2.0*pl

        gateBody = Group( 
                Path(
                    P(-pinBackDist+xBit, -pinEdgeDist), 
                    C(90, 225),
                    P(1.4*bodyWidth, bodyHeight/2.), 
                    C(-45, 90),
                    P(-pinBackDist+xBit, bodyHeight+pinEdgeDist), 
                    C(140, 40),
                    P(-pinBackDist+xBit, -pinEdgeDist),
                    ), 
                Path(
                    P(-pinBackDist, bodyHeight+pinEdgeDist), 
                    C(140, 40),
                    P(-pinBackDist, -pinEdgeDist)
                    ),
                )
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Path(
                gateBody.e, 
                gateBody.e+P(pl, 0))

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # now set the object to myself
        self.append(obj)

# NXOR gate
class NxorGate(Gate):
    """ 
    Generates a NXOR gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        pinEdgeDist = 0.1*self.height
        pinBackDist = -0.08*self.width
        xBit = 0.2
        rad = 0.1
        pl = self.pinLength
        bodyHeight = self.height
        bodyWidth = self.width - 2.0*pl

        gateBody = Group( 
                Path(
                    P(-pinBackDist+xBit, -pinEdgeDist), 
                    C(90, 225),
                    P(1.4*bodyWidth, bodyHeight/2.), 
                    C(-45, 90),
                    P(-pinBackDist+xBit, bodyHeight+pinEdgeDist), 
                    C(140, 40),
                    P(-pinBackDist+xBit, -pinEdgeDist),
                    ), 
                Path(
                    P(-pinBackDist, bodyHeight+pinEdgeDist), 
                    C(140, 40),
                    P(-pinBackDist, -pinEdgeDist)
                    ),
                )
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Group( 
                Circle(w=gateBody.e, r=rad), 
                Path(
                    gateBody.e+P(0.2, 0), 
                    gateBody.e+P(pl+0.2, 0)),
                )

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # now set the object to myself
        self.append(obj)

# NOT gate
class NotGate(Gate):
    """
    Generates a NOT gate

    @ivar height: gate height
    @type height: float

    @ivar width: gate width
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of gate
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # initialise the base class
        Gate.__init__(self, **options)

        # process the options if any
        self.height = options.get("height", self.height)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)
 
        # now draw the gate
        buff = 0
        pl = self.pinLength
        pinEdgeDist = 0.1*self.height
        bodyHeight = self.height
        bodyWidth = self.width - 2.0*pl
        rad = 0.1

        gateBody = Group(
                Path( 
                    P(pl, buff+0), 
                    P(pl, buff+bodyHeight), 
                    P(pl+0.707106781*bodyWidth, buff+bodyHeight/2.), 
                    P(pl, buff+0)
                    )
                )
        gatePinIn1 = Path(
                P(0, bodyHeight-pinEdgeDist), 
                P(pl, bodyHeight-pinEdgeDist))
        gatePinIn2 = Path(
                P(0, pinEdgeDist), 
                P(pl, pinEdgeDist))
        gatePinOut = Group(
                Circle(w=gateBody.e, r=rad), 
                Path(
                    gateBody.e+P(2.*rad, 0), 
                    gateBody.e+P(2.*rad+pl, 0))
                )

        # collect the objects together
        obj = Group(gateBody, gatePinIn1, gatePinIn2, gatePinOut)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # now set the object to myself
        self.append(obj)

# resistor
class Resistor(Group):
    """
    Generates a box resistor

    @ivar length: length of resistor
    @type length: float

    @ivar width: width of resistor
    @type width: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of resistor
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # intitialise base class
        Group.__init__(self, **options)

        self.length = 3.0
        self.width = 1.0
        self.angle = 0.0
        self.pinLength = 0.5
        self.fg = Color(0)
        self.bg = Color(1)

        # process the options if any
        self.length = options.get("length", self.length)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)

        pinIn = Group(
                Path( 
                    P(0, 0), 
                    P(self.pinLength, 0)
                    )
                )
        resistor = Rectangle(w=pinIn.e, width=self.length, height=self.width)
        pinOut = Path(
                resistor.e, 
                resistor.e+P(self.pinLength, 0))

        # collect the objects together
        obj = Group(pinIn, pinOut, resistor)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # return object to myself
        self.append(obj)

# capacitor
class Capacitor(Group):
    """
    Generates a capacitor

    @ivar width: width of capacitor
    @type width: float

    @ivar sep: separation of the plates of the capacitor
    @type sep: float

    @ivar angle: gate angle
    @type angle: float

    @ivar pinLength: length of pins into and out of capacitor
    @type pinLength: float

    @ivar fg: foreground colour
    @type fg: L{Color} object

    @ivar bg: background colour
    @type bg: L{Color} object
    """
    def __init__(self, **options):
        # intitialise base class
        Group.__init__(self, **options)

        self.sep = 0.25
        self.width = 1.0
        self.angle = 0.0
        self.pinLength = 0.5
        self.fg = Color(0)
        self.bg = Color(1)

        # process the options if any
        self.sep = options.get("sep", self.sep)
        self.width = options.get("width", self.width)
        self.angle = options.get("angle", self.angle)
        self.pinLength = options.get("pinLength", self.pinLength)
        self.fg = options.get("fg", self.fg)
        self.bg = options.get("bg", self.bg)

        pinIn = Group(
                Path( 
                    P(0, 0), 
                    P(self.pinLength, 0),
                    )
                )
        cap = Group( 
                Path(pinIn.e+P(0, -self.width/2.0), 
                    pinIn.e+P(0, self.width/2.0)), 
                Path(pinIn.e+P(self.sep, -self.width/2.0), 
                    pinIn.e+P(self.sep, self.width/2.0)),
                )
        pinOut = Path(
                cap.e, 
                cap.e+P(self.pinLength, 0))

        # group the objects together
        obj = Group(pinIn, pinOut, cap)

        # apply the colours
        obj.apply(fg=self.fg, bg=self.bg)

        # rotate if necessary
        if self.angle != 0.0:
            obj.rotate(self.angle, p=obj.c)

        # set the object to myself
        self.append(obj)

# vim: expandtab shiftwidth=4:
