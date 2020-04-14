#!/usr/bin/env pyscript

# $Id: teleport.py,v 1.3 2006/02/14 14:23:09 paultcochrane Exp $

"""
Create a quantum circuit of a quantum teleporter using the qi library
"""

# import the pyscript libraries
from pyscript import *
from pyscript.lib.qi import *

# Assemble the quantum circuit
g = Assemble(
        SWAP, 1, 2,
        NOT, 2, 1,
        H, 1,
        X, 3, 2,
        Z, 3, 1,
        hang=.5,
        wires=[QWire, QWire, QWire],
        )

# define the object for the wires
w = g.wires

# render the diagram
render(
        # the main circuit
        g,

        # some labels
        TeX(r'$|\psi\rangle$', e=w[0].w),
        TeX(r'$|\psi\rangle$', w=w[2].e),
        TeX(r'$|\beta_{00}\rangle\left\{\rule{0cm}{7mm}\right.$',
            e=(w[2].w+w[1].w)/2.),

        # the meters
        Meter(w=w[0].e),
        Meter(w=w[1].e),

        # the output file
        file="teleport.eps"
        )

# vim: expandtab shiftwidth=4:
