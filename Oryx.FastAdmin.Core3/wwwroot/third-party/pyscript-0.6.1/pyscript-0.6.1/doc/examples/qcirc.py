#!/usr/bin/env pyscript

# $Id: qcirc.py,v 1.3 2006/02/14 14:23:09 paultcochrane Exp $

"""
A quantum circuit example usingt the qi (quantum information) library.
"""

from pyscript import *
from pyscript.lib.qi import *

defaults.tex_head=r"""
\documentclass{article}
\pagestyle{empty}

\newcommand{\ket}[1]{\mbox{$|#1\rangle$}}
\newcommand{\bra}[1]{\mbox{$\langle #1|$}}
\newcommand{\braket}[2]{\mbox{$\langle #1|#2\rangle$}}
\newcommand{\op}[1]{\mbox{\boldmath $\hat{#1}$}}
\newcommand{\Ket}[2]{|#1\rangle^{ (#2)}}

% Paragraph formatting
\setlength{\parindent}{0pt}
\setlength{\parskip}{1ex}

\usepackage{amsmath}
\usepackage{amssymb}
\usepackage[dvips,nodvipsnames]{color}
\begin{document}
"""

def RX(theta,**dict):
    return Circled(TeX(r"$X_{%s}$"%theta,**dict),r=.5)
def RY(theta,**dict):
    return Circled(TeX(r"$Y_{%s}$"%theta,**dict),r=.5)
def RZ(theta,**dict):
    return Circled(TeX(r"$Z_{%s}$"%theta,**dict),r=.5)

render(
    Path(P(-1,0),P(5,0)),
    Path(P(-1,2),P(10,2)),

    classicalpath(Path(P(5,0),P(5.8,0),P(5.8,2)),Path(P(5.8,0),P(7,0),P(7,2))),

    Boxed(TeX(r'$\mathcal{E}(\rho)$',c=P(0,2))),

    RX(r'\frac{\pi}{2}',c=P(2,2)),
    RX(r'-\frac{\pi}{2}',c=P(8.5,2)),
    RY(r'\chi',c=P(4,0)),
    RY(r'\eta',c=P(7,2)),

    TeX(r'$\ket{\psi_s}$',e=P(-1-.1,2)),
    TeX(r'$\ket{\psi_m}$',e=P(-1-.1,0)),
    TeX(r'$\rho_s$',w=P(10+.1,2)),

    NOT(target=P(3,0),control=P(3,2)),

    Detector(c=P(5,0),height=.7),

    Z(c=P(5.8,2)),

    Rectangle(w=P(1,1),height=3.5,width=8.5,dash=Dash(2,1)),

    file="qcirc.eps",
)

# vim: expandtab shiftwidth=4:

