#!/usr/bin/env pyscript

# $Id: distribute.py,v 1.4 2006/02/14 14:23:09 paultcochrane Exp $

"""
An example showing usage of the Distribute class and functionality.
"""

# load pyscript libraries
from pyscript import *

# define the default units for the diagram
defaults.units = UNITS['cm']

# end points of line about which to distribute objects
p1 = P(-3,0)
p2 = P(3,0)

# objects to distribute
o1 = Rectangle(width=.5, height=1)
o2 = Circle(r=.5)
o3 = Rectangle(width=.5, height=.5)
o4 = Rectangle(width=2, height=.5)

# render the diagram
render(
    # distribute the objects
    Distribute(o1, o2, o3, o4, 
        p1=p1, p2=p2,
        a1='c', a2='c', as='w', ae='e'),

    # highlight the line about which everything is distributed with a line
    Path(p1, p2, fg=Color('red')),

    # dots showing centres of all objects distributed
    Dot(o1.c), Dot(o2.c), Dot(o3.c), Dot(o4.c),
    
    # output file name
    file="distribute.eps"
    )

# vim: expandtab shiftwidth=4:
