# $Id: Makefile,v 1.8 2006/04/24 12:40:54 paultcochrane Exp $

MAIN = align.eps\
       align2.eps\
       atom.eps\
       bifurcate.eps\
       detector.eps\
       distribute.eps\
       div_con.eps\
       fabry-perot_pdh.eps\
       flower_algorithm.eps\
       mach-zehnder.eps\
       michelson-morely.eps\
       qcirc.eps\
       sagnac.eps\
       sphere.eps\
       stateSwap.eps\
       teleport.eps\
       tex.eps\
       tutorial.eps\
       twoqubitQST.eps

all: $(MAIN) Makefile

view: $(MAIN)
	$(foreach i, $(MAIN), $(shell gv $i))

# standard TeX stuff
%.eps:  %.py
	pyscript $<

clean:
	rm -f $(MAIN)

# vim: shiftwidth=4:
