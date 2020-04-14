# Copyright (C) 2002-2006  Alexei Gilchrist and Paul Cochrane
# 
# This program is free software; you can redistribute it and/or
# modify it under the terms of the GNU General Public License
# as published by the Free Software Foundation; either version 2
# of the License, or (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

# $Id: Makefile,v 1.11 2006/06/06 12:38:11 paultcochrane Exp $

MAIN = pyscript
FIGS = 

all: $(FIGS) $(MAIN).pdf

view: $(FIGS) $(MAIN).pdf
	gv $(MAIN).pdf

acro: $(FIGS) $(MAIN).pdf
	acroread $(MAIN).pdf

pdf: $(MAIN).pdf

dvi: $(MAIN).dvi

$(FIGS) : %.pdf : %.py
	./$<
	epstopdf $*.eps

%.pdf:  %.tex $(FIGS) pyscript_defs.tex libpresent.tex libpresentation.tex libqi.tex pyscriptElectronics.tex pyscriptOptics.tex pyscript.bib
	pdflatex $<
	-bibtex $*
	pdflatex $<
	pdflatex $<

%.dvi:  %.tex $(FIGS) pyscript_defs.tex libpresent.tex libpresentation.tex libqi.tex pyscriptElectronics.tex pyscriptOptics.tex pyscript.bib
	latex $<
	-bibtex $*
	latex $<
	latex $<

# standard TeX stuff
#%.dvi:  %.tex $(FIGS)
#	latex $<
#	-bibtex $*
#	latex $<
#	latex $<

#%.ps: %.dvi
#	dvips -o $*.ps $*.dvi

#%.pdf: %.tex
#	ps2pdf $*.ps $*.pdf

#dvi: $(MAIN).dvi
#ps: $(MAIN).ps
#pdf: $(MAIN).pdf
figs: $(FIGS)

clean:
	-rm -f *~ *.mpx *.log *.aux *.blg

reallyclean: clean
	-rm -f figs.[0-9]*.eps *.pdf *.dvi $(FIGS) *.bbl
