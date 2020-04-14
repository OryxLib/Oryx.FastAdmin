#!/usr/bin/env pyscript

# $Id: div_con.py,v 1.3 2006/02/14 14:23:09 paultcochrane Exp $

"""
Example usage of the qi (quantum information) library.  Output is of a
divide-and-conquer scheme of quantum gates.
"""

# import relevant pyscript libraries
from pyscript import *
from pyscript.lib.qi import *

# assemble one set of quantum gates and wires together
g1 = Assemble(
        Gate(Boxed(TeX(r'$\mathcal{E}_1$'), height=1.5)), 1.5,
        Gate(Boxed(TeX(r'$\mathcal{E}_2$'), height=1.5)), 2.5,
        Gate(Boxed(TeX(r'$\mathcal{E}_3$'), height=1.5)), 1.5,
        wires=[QWire, QWire, QWire],
        )

# assemble another set of quantum gates
g21 = Assemble(Gate(Boxed(TeX(r'$\mathcal{E}_1$'), height=1.5)), 1.5,
        wires=[QWire, QWire, QWire],
        )
g22 = Assemble(Gate(Boxed(TeX(r'$\mathcal{E}_2$'), height=1.5)), 2.5,
        wires=[QWire, QWire, QWire],
        )
g23 = Assemble(Gate(Boxed(TeX(r'$\mathcal{E}_3$'), height=1.5)), 1.5,
        wires=[QWire, QWire, QWire],
        )

# and assemble a third set of quantum gates together
g31 = Assemble(Gate(Boxed(TeX(r'$\mathcal{E}_1$'), height=1.5)), 1.5,
        wires=[QWire, QWire],
        )
g32 = Assemble(Gate(Boxed(TeX(r'$\mathcal{E}_2$'), height=1.5)), 1.5,
        wires=[QWire, QWire],
        )
g33 = Assemble(Gate(Boxed(TeX(r'$\mathcal{E}_3$'), height=1.5)), 1.5,
        wires=[QWire, QWire],
        )

# define some TeX objects for later reuse
m1 = TeX(r'\Large $\le$')
m2 = TeX(r'\Large $+$')
m3 = TeX(r'\Large $+$')
m4 = TeX(r'\Large $=$')
m5 = TeX(r'\Large $+$')
m6 = TeX(r'\Large $+$')

# align all the gate and TeX objects
Align(g1, m1, m2, m3, m4, m5, m6, g31, g32, g33, 
        a1="e", a2="w", angle=90)

# distribute the objects equally along a line
divnconk = Distribute(
        g1,
        m1,
        g21, m2, g22,
        m3, g23,
        m4,
        g31, m5, g32, m6, g33,
        a1="e", a2="w", p1=P(0,0), p2=P(15,0),
        )

# define some labels
t1 = TeX(r'\Large (by chaining)', n=P(g22.s.x, divnconk.s.y-.2))
        
t2 = TeX(r'\Large (by stability)', n=P(g32.s.x, divnconk.s.y-.2))

# append the labels to the object for the diagram
divnconk.append(t1,t2).scale(.7)

# render the diagram
render(
        # object to render
        divnconk,

        # output file name
        file="div_con.eps"
        )

# vim: expandtab shiftwidth=4:
