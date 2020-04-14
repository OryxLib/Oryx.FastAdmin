#!/usr/bin/env pyscript

# $Id: tex.py,v 1.4 2006/02/14 14:23:09 paultcochrane Exp $

"""
Example showing off the TeX object.  This shows a series of TeX objects of a
mathematical description of a wavefunction (but you don't need to know that)
placed at various angles in a circle, with a blue background.
"""

# import the pyscript libraries
from pyscript import *

# define the default units for the diagram
defaults.units=UNITS['cm']

# define a TeX object
tex = TeX(r'$|\psi_t\rangle=e^{-iHt/\hbar}|\psi_0\rangle$',
        w=P(.5,0), fg=Color(1))

# define the group of objects to render
g = Group()
for ii in range(0, 360, 60):
    g.append(tex.copy().rotate(ii, P(0,0)))

# render the diagram
render(
        # one circle
        Circle(r=.6+tex.width, bg=Color('midnightblue')), 

        # the TeX object
        g,

        # another circle
        Circle(r=.4, bg=Color(1)),

        # the output file name
        file="tex.eps")

# vim: expandtab shiftwidth=4:

