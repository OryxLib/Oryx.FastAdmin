#!/usr/bin/env pyscript
# $Id: align.py,v 1.4 2005/03/02 01:40:03 paultcochrane Exp $

"""
align.py - example of using the Align class.

Define some Rectangles and Circles and show how the Align class aligns them
in a horizontal line.
"""

# import the pyscript objects
from pyscript import *

# set the default units to use
defaults.units=UNITS['cm']

# define some objects to align
r1 = Rectangle(width=2, height=1, c=P(0,0))
r2 = Rectangle(width=1, height=2, c=P(2,1))
c1 = Circle(c=P(3,2))
c2 = Circle(r=2, c=P(6,3))

# group the objects together to save their original positions
orig = Group(r1, r2, c1, c2)

# define an Align object and add the rectangles and circles to it
a = Align(a1="e", a2="w", space=None, angle=90)
for o in orig:
	a.append(o.copy())

# record where they started in red
orig.apply(fg=Color('Red'))

# make the linewidth of the aligned objects thicker
a.apply(linewidth=2)

# render the objects and save to file
render(
    orig,
    a,
    file="align.eps",
    )

# vim: expandtab shiftwidth=4:
