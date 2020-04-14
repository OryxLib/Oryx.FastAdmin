#!/usr/bin/env pyscript

# $Id: sphere.py,v 1.5 2006/02/14 14:23:09 paultcochrane Exp $

"""
A Poincare sphere.  The example shows a sphere with axes labeled and a
vector pointing to a point on the surface of the sphere.
"""

# import the pyscript libraries
from pyscript import *

# define the default units for the diagram
defaults.units=UNITS['cm']

# define some handy LaTeX macros
defaults.tex_head=r"""
\documentclass{article}
\pagestyle{empty}

\newcommand{\ket}[2]{\mbox{$|#1\rangle_{#2}$}}
\newcommand{\bra}[1]{\mbox{$\langle #1|$}}
\newcommand{\braket}[2]{\mbox{$\langle #1|#2\rangle$}}
\newcommand{\op}[1]{\mbox{\boldmath $\hat{#1}$}}
\begin{document}
"""

# define an arrow head function
def ArrowHead(tip=P(0,0), width=0.2, height=0.2, 
        fg=Color("black"), bg=Color("black"), angle=0, dent=0.2):
    """
    ArrowHead object
    """
    tmptip = P(0,0)

    ah = Path(tmptip, tmptip + P(width/2.0,-height),
                tmptip + P(0,-height*(1-dent)),
                tmptip + P(-width/2.0,-height), tmptip,
                fg=fg, bg=bg)

    ah.rotate(angle)
    ah.move(tip-tmptip)
    
    return ah

# define the circle in the x-z plane
circ1 = Circle(c=P(0,0))

# define the circle in the x-y plane
circ2front = Circle(c=P(0,0), start=90, end=270)
circ2back = Circle(c=P(0,0), start=270, end=90, dash=Dash(3))
circ2 = Group(circ2front, circ2back)
circ2.scale(1, 0.3)

# define the circle in the y-z plane
circ3front = Circle(c=P(0,0), start=90, end=270)
circ3back = Circle(c=P(0,0), start=270, end=90, dash=Dash(3))
circ3 = Group(circ3front, circ3back)
x2 = 0.78
circ3.scale(x2, 0.3*x2)
circ3.c = P(0,-0.6)

# define the axes
zaxis = Path(P(0,-1.2), P(0,1.2))
xaxis = Path(P(-1.2,0), P(1.2,0))
yaxis = Path(P(-0.7,0), P(0.7,0))
yaxis.rotate(p=yaxis.P(.5), angle=-30)

# define the arrows for each axis
xArrow = ArrowHead(tip=xaxis.end, width=0.07, height=0.07, angle=90)
yArrow = ArrowHead(tip=yaxis.end, angle=90-30, width=0.07, height=0.07)
zArrow = ArrowHead(tip=zaxis.end, width=0.07, height=0.07)

# define the labels
xLabel = TeX("$\mathbf{J}_x$").scale(.5)(sw=xaxis.end)
yLabel = TeX("$\mathbf{J}_y$").scale(.5)(se=yaxis.end+P(0,.05))
zLabel = TeX("$\mathbf{J}_z$").scale(.5)(sw=zaxis.end)

# define the vector R
rad = Path(P(0,-0.59), P(0.5,-0.59))
rad.rotate(p=rad.bbox().w, angle=20)
radArrow = ArrowHead(tip=rad.end, width=0.06, height=0.06, angle=90+20)
radThing = Group(rad, radArrow)
radLabel = TeX("$\mathcal{R}$").scale(0.5,0.5)(sw=rad.P(.5))

# define the figure describing the diagram
fig = Group(circ1, circ2, circ3, 
        zaxis, xaxis, yaxis,
        xArrow, yArrow, zArrow,
        xLabel, yLabel, zLabel,
        radThing, radLabel)

# render the diagram
render(fig, file="sphere.eps")

# vim: expandtab shiftwidth=4:
