#!/usr/bin/env pyscript

# $Id: align2.py,v 1.3 2006/02/14 14:23:08 paultcochrane Exp $

"""
Aligning boxes, with numbers inside, around a circle.
"""

from pyscript import *

# define the object to hold all the objects
all = Align(angle=90, space=2)

# create an equal sided rectangle, put a number in it and add it to the
# group of all objects
for ii in range(10):
    rt = Rectangle(width=1, height=1)
    all.append(
        Group(rt, Text(str(ii), c=rt.c)), angle=all.angle+ii*360/10.)

# render the diagram
render(
    all,
    file='align2.eps',
    )

# vim: expandtab shiftwidth=4:
