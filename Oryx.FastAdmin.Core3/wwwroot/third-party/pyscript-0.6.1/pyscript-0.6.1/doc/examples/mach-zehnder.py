#!/usr/bin/env pyscript

# $Id: mach-zehnder.py,v 1.1 2006/03/03 15:26:45 paultcochrane Exp $

# Mach-Zehnder interferometer

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

# the "west" beam splitter
bs_w = BSBox(height=0.7)
bs_w.w = laser.e + P(1,0)
beam.append(Path(laser.e, bs_w.w))

# the "north" mirror
mirror_n = Mirror(angle=45)
mirror_n.s = bs_w.n + P(0,3)
beam.append(Path(bs_w.n, mirror_n.c))

# the "east" mirror
mirror_e = Mirror(angle=45)
mirror_e.w = bs_w.e + P(3,0)
beam.append(Path(bs_w.e, mirror_e.c))

# the "east" beam splitter
bs_e = BSBox(height=0.7)
bs_e.c = P(mirror_e.c.x, mirror_n.c.y)
beam.append(Path(mirror_e.c, bs_e.s))
beam.append(Path(mirror_n.c, bs_e.w))

# the "north" detector
det_n = Detector(angle=-90)
det_n.s = bs_e.n + P(0,1)
beam.append(Path(bs_e.n, det_n.s))

# the "east" detector
det_e = Detector()
det_e.w = bs_e.e + P(1,0)
beam.append(Path(bs_e.e, det_e.w))

# set the colour of the beam
beam.apply(fg=Color("red"))

# collect all the objects together
fig = Group(
        laser,
        bs_w,
        mirror_n, mirror_e,
        bs_e,
        det_n, det_e,
        beam,
        )

# render the figure
render(fig, 
        file="mach-zehnder.eps")

# vim: expandtab shiftwidth=4:
