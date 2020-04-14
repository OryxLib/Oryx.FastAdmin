#!/usr/bin/env pyscript
# $Id: bifurcate.py,v 1.2 2006/02/14 14:23:08 paultcochrane Exp $

"""
bifurcate.py - a bifurcation diagram

As lambda increases to some critical value the function becomes
multivalued and bifurcates giving the fork structure after \lambda_c.
"""

# import the pyscript objects
from pyscript import *

# define the default units to use
defaults.units = UNITS['cm']

render(
    # define the axes
    Arrow( P(0,0), P(0,4) ),
    Arrow( P(0,0), P(5,0) ),

    # dashed vertical line at lambda_c
    Path( P(2,3.8), P(2,0), dash=Dash(2), fg=Color(.5) ),

    # solid line before bifurcation point
    Path( P(.5,2), P(2,2), linewidth=1 ),

    # multi-valued part of function after bifurcation
    Path( P(4,3.7),
         C(P(3,3.5), P(2,3)),
         P(2,2),
         C(P(2,1), P(3,.5)),
         P(4,.3), linewidth=1),

    # dashed horizontal line after lambda_c
    Path( P(2,2), P(4,2), dash=Dash(3), linewidth=1 ),

    # axes labels
    TeX(r'$\bar{\lambda}_c$', n=P(2,-.1)),

    TeX(r'$\bar{\lambda}$', ne=P(4.8,-.1)),
    
    # the output file
    file="bifurcate.eps",
    )

# vim: expandtab shiftwidth=4:
