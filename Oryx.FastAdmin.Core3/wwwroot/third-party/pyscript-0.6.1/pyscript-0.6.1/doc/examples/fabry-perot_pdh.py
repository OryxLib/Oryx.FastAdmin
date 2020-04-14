#!/usr/bin/env pyscript

# $Id: fabry-perot_pdh.py,v 1.1 2006/03/01 15:13:16 paultcochrane Exp $

# a Fabry-Perot cavity in a Pound-Drever-Hall setup

# import the pyscript objects
from pyscript import *
# import the optics library
from pyscript.lib.optics import *

# set up some handy defaults
defaults.units=UNITS['cm']

# initialise a laser beam
beam = Group()

# the laser
laser = Laser(c=P(0,0))

# the EOM
eom = Modulator()
eom.w = laser.e + P(1,0)
beam.append(Path(laser.e, eom.w))

# the "west" mirror
mirror_w = Mirror()
mirror_w.w = eom.e + P(1,0)
beam.append(Path(eom.e, mirror_w.w))

# some free space
fs = FreeSpace()
fs.w = mirror_w.e + P(1,0)
beam.append(Path(mirror_w.e, fs.w))

# the "east" mirror
mirror_e = Mirror()
mirror_e.w = fs.e + P(1,0)
beam.append(Path(fs.e, mirror_e.w))

# set the colour of the beam
beam.apply(fg=Color("red"))

# collect all the objects together
fig = Group(
        beam,
        laser,
        eom,
        mirror_e, mirror_w,
        fs,
        )

# render the figure
render(fig, 
        file="fabry-perot_pdh.eps")

# vim: expandtab shiftwidth=4:
