#!/usr/bin/env pyscript

# $Id#

"""
Poster for the Conference on Computational Physics 2004
"""

from pyscript.lib.presentation import Poster, Column, ColumnBox, CodeBox, TeXBox

defaults.tex_head+=r"\newcommand{\xmds}{\textsc{xmds}\xspace}"

# firstly, write the code I would like to write...
poster = Poster(size="a4", style="ccp2004-poster")

poster.set_title(r"\xmds: the eXtensible Multi-Dimensional Simulator")

poster.set_authors(r"""\underline{Paul~T.~Cochrane}, G.~Collecutt, 
P.~D.~Drummond, and J.~J.~Hope""")

poster.set_address(r"""Australian Centre for Quantum-Atom Optics, 
Physics Department, The University of Queensland, 
Brisbane, Australia""")

poster.set_abstract(r"""{\em 
Writing codes for the simulation of complex phenomena is an art and
science unto itself.  What with finding and using good algorithms,
actually writing the code, debugging the code and testing the code,
not much time is left to actually investigate what it was you were
initially out to look at.  This is where \xmds comes in.  \xmds allows
you to write a high-level description of the problem you are
trying to solve (usually a differential equation of some form) it goes
away and writes low-level simulation code for you (trying hard to
keep the code as efficient as possible), compiles and presents it,
ready to be run.}
""")

poster.add_logo("ARC_COE_crop.eps", height=1.2)
poster.add_logo("uq_logo_new.eps", height=1.2)
# could also do this as:
#poster.add_logos("ARC_COE_crop.eps", "uq_logo_new.eps", height=1.2)

col1 = Column(poster)

what_xmds = ColumnBox(poster)
what_xmds.set_title(r"What is \xmds?")
what_xmds.add_TeXBox(r"""
\begin{itemize}
\setlength{\itemsep}{-1mm}
\item \xmds = e\underline{X}tensible \underline{M}ulti-\underline{D}imensional 
\underline{S}imulator

\item \xmds is open source software; released under the GNU General Public 
License

\item Has applications in physics, mathematics, weather, chemistry, 
economics \ldots

\item One writes a high-level description of a problem in XML

\item \xmds converts XML to C language code, which is then compiled to 
produce an executable which solves the problem about as quickly as code 
written by an expert

\item \xmds gives people doing simulations structure, organisation and 
standardisation

\item Provides a convenient framework for describing simulations of a 
system be it in a scientific or industrial setting

\item Keeps the ideas behind a simulation well laid out and, importantly, 
documented for others to see and use

\item \xmds gives a common ground from which scientists can compare their 
numerical work; something lacking in an area at the interface between 
theory and experiment, which already have a well-ingrained culture of 
comparison and verification~\cite{Ceperley:1999:1}
\end{itemize}
""")
col1.add_box(what_xmds)

overview = ColumnBox(poster)
overview.set_title(r"Overview")
overview.add_TeXBox(r"""
\begin{itemize}
\setlength{\itemsep}{-2mm}
\item \xmds is designed to integrate the following general PDE:
\vspace*{-3mm}
\begin{align}
\frac{\partial}{\partial x^0}\vect{a}(\vect{x}) & =
\vect{\mathcal{N}}\left(\vect{x}, \vect{a}(\vect{x}), \vect{p}(\vect{x}),
\vect{b}(\vect{x}),\;\vect{\xi}(\vect{x})\right),\\ 
p^i(\vect{x}) & = \mathcal{F}^{-1}\left[\Sigma_j
\mathcal{L}^{ij}\left(x^0,\vect{k_\bot}\right)
\mathcal{F}\left[a^j(\vect{x})\right]\right],\\
\frac{\partial}{\partial x^{c}}\vect{b}(\vect{x}) & =
\vect{\mathcal{H}}\left(\vect{x}, \vect{a}(\vect{x}), 
\vect{b}(\vect{x})\right)
\label{eq:xmdsPdeEx}
\end{align}
\vspace*{-4mm}

\item $\vect{a}(\vect{x})$ : main field, $\vect{b}(\vect{x})$ : 
cross-propagating field, $\vect{p}(\vect{x})$ : field in Fourier space,\\ 
$\xi(\vect{x})$ : noise terms 

\item \xmds integrates ODEs, PDEs, and stochastic ODEs and PDEs
\end{itemize}
""")

tAlign = Align(a1="ne", a2="nw", angle=90, space=-0.2)

t1 = TeXBox(r"""
\begin{itemize}
\item \xmds solves DEs with two methods:
  \begin{itemize}
  \setlength{\itemsep}{-1.5mm}
  \vspace*{-3mm}
  \item fourth-order Runge-Kutta,
  \item split-step semi-implicit method~\cite{Drummond:1983:1}
  \end{itemize}
\end{itemize}
""")
t1.set_fixed_width(5.1)
t1.make()

t2 = TeXBox(r"""
\begin{itemize}
\item \xmds can handle any number of:
\vspace*{-3mm}
  \begin{itemize}
  \setlength{\itemsep}{-1.5mm}
  \item components
  \item dimensions
  \item random variables
  \end{itemize}
\end{itemize}
""")
t2.set_fixed_width(4.5)
t2.make()

tAlign.append(t1, t2)
overview.add_object(tAlign)

overview.add_TeXBox(r"""
\vspace*{-3mm}
\begin{itemize}
\setlength{\itemsep}{-2mm}
\item Performs automatic numerical error checking
\item Handles cross-propagating fields
\item Calculates trajectory means and variances of stochastic simulations
\item Automatically parallelises stochastic and deterministic problems using MPI
\end{itemize}
""")

col1.add_box(overview)

why_xmds = ColumnBox(poster)
why_xmds.set_title(r"Why use \xmds?")
why_xmds.add_TeXBox(r"""
\begin{itemize}
\setlength{\itemsep}{-1.5mm}
\item \xmds reduces development time and user-introduced bugs

\item Execution time closely approximates that of hand-written code

\item Input file size dramatically smaller than hand-written code

\item Open source and documentation 
(\texttt{http://www.xmds.org})~\cite{xmdsweb}

\item Uses XSIL output format for easy and portable data interchange

\item FFTW (Fastest Fourier Transform in the West) for highly efficient 
FFTs~\cite{fftwweb}

\item Allows simple and transparent comparison of simulations with other 
researchers

\item The script documents the simulation

\item Simulation script (and therefore parameters) are output with the
simulation data, so the data and the variables that generated it are kept
together for future reference 
\end{itemize}
""")

col1.add_box(why_xmds)

nlse = ColumnBox(poster)
nlse.set_title(r"Nonlinear Schr\"{o}diner Equation")
nlse.add_TeXBox(r"""
\begin{equation}
\frac{\partial \phi}{\partial z } = i\left[\frac{1}{2} \frac{\partial ^{2}
\phi}{\partial t ^{2}} + |\phi|^{2} \phi + i \Gamma (t) \phi
\right]
\end{equation}
Where $\phi$ is the field, $z$ is the spatial dimension,
$t$ is time and $\Gamma(t)$ is a damping term.
""")

nlseEpsf = Epsf("nlse.eps", width=4.29)

nlseCode = CodeBox(r"""
\begin{verbatim}
  <simulation>  <!-- outline xmds code; greatly compressed for space -->
  <name>nlse</name> <prop_dim>z</prop_dim>
  <field>  <!-- field to be integrated over -->
    <dimensions> t </dimensions>
    <vector>  <components>phi</components>
      <![CDATA[  phi = pcomplex(amp*exp(-t*t/w0/w0),vel*t);  ]]>
    </vector>
  </field>
  <sequence>  <!-- sequence of integrations to perform -->
    <integrate>  <algorithm>RK4IP</algorithm>
      <k_operators>
        <![CDATA[  L = rcomplex(0,-kt*kt/2);  ]]>
      </k_operators>
      <![CDATA[  dphi_dz =  L[phi] + i*~phi*phi*phi - phi*damping;  ]]>
    </integrate>
  </sequence>
  <output>  <!-- output to generate -->
    <group>
      <sampling>
        <moments>pow_dens</moments>
        <![CDATA[  pow_dens=~phi*phi;  ]]>
      </sampling>
    </group>
  </output>
  </simulation>
\end{verbatim}
""").scale(0.4, 0.4)

nlseAlign = Align(a1="ne", a2="nw", angle=90, space=0.1)
nlseAlign.append(nlseEpsf, nlseCode)

nlse.add_object(nlseAlign)

col1.add_box(nlse)

future = ColumnBox(poster)
future.set_title("Future Features")
future.add_TeXBox(r"""
\begin{itemize}
\setlength{\itemsep}{-2mm}
\item More algorithms, user-defined libraries of routines
\item Improved load balancing of parallel stochastic simulations
\item Timed output of simulation data to monitor data on-the-fly 
\item Reimplementation and generalisation of \xmds engine
%\item breakpoints: binary output of entire simulation state at end of
%simulation so that can restart the simulation from this point at next run
%of the simulation
\end{itemize}
""")

col1.add_box(future)

# add the column to the poster
poster.add_column(col1, side="left")

# start a new column
col2 = Column(poster)

# make a column box
fibre_optic = ColumnBox(poster)
fibre_optic.set_title("Fibre Optic Laser Field")
fibre_optic.add_TeXBox(r"""
Equation~(\ref{eq:fibre}) describes a one dimensional
damped field subject to a complex noise.\\ This is a stochastic PDE.
\begin{equation}
\frac{\partial \phi}{\partial t} = -i \frac{\partial^{2}
\phi}{\partial x^{2}} - \gamma \phi + \frac{\beta}{\sqrt{2}}
\left[\xi_1(x,t) + i\xi_2(x,t)\right].
\label{eq:fibre}
\end{equation}
""")

fibre1 = VAlign(space=0.2)
fibre1.append(Epsf("fibre1.eps", width=4))
fibre1.append(TeX("single path").scale(0.8, 0.8))

fibre2 = VAlign(space=0.2)
fibre2.append(Epsf("fibre2.eps", width=4))
fibre2.append(TeX("1024 path mean").scale(0.8, 0.8))

fibres = Align(a1="ne", a2="nw", angle=90, space=0.2)
fibres.append(fibre1, fibre2)

fibre_optic.add_object(fibres)

# add the column box to the poster
col2.add_box(fibre_optic)

# make a column box
process = ColumnBox(poster)
process.set_title("Process and Functionality")
process.add_TeXBox(r"""
The figures below describe the processes involved in creating an
\xmds simulation (left-hand diagram) and operating within an \xmds simulation
(right-hand diagram).  \xmds reads the XML script, parses it, generates
C/C++ code and then compiles the simulation binary using a C++ compiler.
The simulation when executed generates XSIL output, which can
then be converted for display in your favourite graphing package.
""")

procAlign = Align(a1="ne", a2="nw", angle=90, space=1.0)
procAlign.append(Epsf("xmdsProcess.eps", width=3))
procAlign.append(Epsf("xmdsFunctionality.eps", width=3))
process.add_object(procAlign)

# add the column box to the poster
col2.add_box(process)

other = ColumnBox(poster)
other.set_title("Other Features")

other1 = TeXBox(r"""
\begin{itemize}
\setlength{\itemsep}{-2mm}
\item ASCII and binary output
\item Benchmarking of simulations
\item User-defined preferences
\end{itemize}
""")
other1.set_fixed_width(4.5)
other1.make()

other2 = TeXBox(r"""
\begin{itemize}
\setlength{\itemsep}{-2mm}
\item Field initialisation from file
\item Command line arguments to simulations
\item \xmds script template output
\end{itemize}
""")
other2.set_fixed_width(5)
other2.make()

otherAlign = Align(a1="ne", a2="nw", angle=90, space=-0.2)
otherAlign.append(other1, other2)

other.add_object(otherAlign)

col2.add_box(other)

takehome = ColumnBox(poster)
takehome.set_title("Take-home message")
takehome.add_TeXBox(r"""
\xmds will save you time by solving your problems very quickly.
So why not give it a go?  See \texttt{http://www.xmds.org} and try it out.
""")

col2.add_box(takehome)

refs = ColumnBox(poster)
refs.set_title("References")
refs.add_TeXBox(r"""
\renewcommand*{\refname}{ }
\begin{thebibliography}{14}
\expandafter\ifx\csname natexlab\endcsname\relax\def\natexlab#1{#1}\fi
\expandafter\ifx\csname bibnamefont\endcsname\relax
  \def\bibnamefont#1{#1}\fi
\expandafter\ifx\csname bibfnamefont\endcsname\relax
  \def\bibfnamefont#1{#1}\fi
\expandafter\ifx\csname citenamefont\endcsname\relax
  \def\citenamefont#1{#1}\fi
\expandafter\ifx\csname url\endcsname\relax
  \def\url#1{\texttt{#1}}\fi
\expandafter\ifx\csname urlprefix\endcsname\relax\def\urlprefix{URL }\fi
\providecommand{\bibinfo}[2]{#2}
\providecommand{\eprint}[2][]{\url{#2}}

\setlength{\itemsep}{-2mm}

\bibitem{Ceperley:1999:1}
\bibinfo{author}{\bibfnamefont{D.~M.} \bibnamefont{Ceperley}},
  \bibinfo{journal}{Rev. Mod. Phys.} \textbf{\bibinfo{volume}{71}},
  \bibinfo{pages}{438} (\bibinfo{year}{1999}).

\bibitem{Drummond:1983:1}
\bibinfo{author}{\bibfnamefont{P.~D.} \bibnamefont{Drummond}},
  \bibinfo{journal}{Comp. Phys. Comm.} \textbf{\bibinfo{volume}{29}},
  \bibinfo{pages}{211} (\bibinfo{year}{1983}).

\bibitem{xmdsweb}
\emph{\bibinfo{title}{\xmds home page}},
  \urlprefix\url{http://www.xmds.org}.

\bibitem{fftwweb}
\emph{\bibinfo{title}{FFTW home page}},
  \urlprefix\url{http://www.fftw.org}.

\end{thebibliography}
""")

col2.add_box(refs)

poster.add_column(col2, side="right")

poster.make(file="ccp2004_poster.eps")
