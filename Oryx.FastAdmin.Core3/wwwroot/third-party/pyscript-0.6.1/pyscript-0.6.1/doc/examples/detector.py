#!/usr/bin/env pyscript

# $Id: detector.py,v 1.4 2006/02/14 14:23:09 paultcochrane Exp $

"""
A quantum circuit diagram of a detection setup in optical quantum computing.
"""

# import the pyscript libraries
from pyscript import *

# set up the default units for the diagram
defaults.units=UNITS['cm']

# define some helpful LaTeX macros
defaults.tex_head=r"""
\documentclass{article}
\pagestyle{empty}

\newcommand{\ket}[1]{\mbox{$|#1\rangle$}}
\newcommand{\bra}[1]{\mbox{$\langle #1|$}}
\newcommand{\braket}[2]{\mbox{$\langle #1|#2\rangle$}}
\newcommand{\ketbra}[2]{\mbox{|#1$\rangle\langle #2|$}}
\newcommand{\op}[1]{\mbox{\boldmath $\hat{#1}$}}
\begin{document}
"""

# import helpful objects from the optics and quantumcircuits libraries
from pyscript.lib.optics import BS
from pyscript.lib.quantumcircuits import detector, classicalpath

# distance between the two "rails" of the quantum circuit
h = 1.7

# define the phase plate
pp2 = P(2,0)
phase = Rectangle(width=.1, height=.4, c=pp2, bg=Color(.8) )

# define the beam splitter
pp = P(1.5,0)
bs = Rectangle(width=.1, height=1, c=pp, bg=Color(1) ).rotate(-45, pp)

# define detectors
d1 = detector()(c=P(1.5+h/2.,0))
d2 = detector()(c=P(1.5,-h/2.)).rotate(90, P(1.5,-h/2.))

# render the diagram
render(
    # the rails of the quantum circuit
    Path(P(1.5,h/2.), P(1.5,-h/2.)),
    Path(P(3.5,h), P(0,h), P(-.5,h/2.), P(0,0), P(1.5+h/2.,0)),

    # the state at one output port
    TeX(r'$\ket{p}$', s=P(1.5,h/2.)),

    # beam splitter and label
    bs,
    TeX(r'$\omega$', se=bs.n),

    # labels of paths through system
    TeX('$a$', sw=P(.1,h+.1)),
    TeX('$b$', sw=P(.1,0.1)),
    TeX('$c$', w=P(1.6,h/2.4)),

    # phase plate and label
    phase,
    TeX(r'$\lambda$', s=phase.n+P(0,.1)),

    # detectors and label
    d1,
    d2,
    TeX('$y$', w=d1.e),
    TeX('$x$', n=d2.e),
    
    # box highlighting part of the circuit
    Rectangle(width=h+.6, height=h+1, dash=Dash(2), e=d1.e+P(.4,0)),
    
    # the input state
    Dot(P(-.5,h/2.), r=.05),
    TeX(r'$\ket{\psi}$', e=P(-.5,h/2.)),

    # the output file name
    file="detector.eps",
    )

# vim: expandtab shiftwidth=4:
