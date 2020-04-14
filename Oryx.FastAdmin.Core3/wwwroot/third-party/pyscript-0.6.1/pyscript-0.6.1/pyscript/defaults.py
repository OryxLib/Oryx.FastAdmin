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

# $Id: defaults.py,v 1.12 2006/02/28 17:08:10 paultcochrane Exp $

"""
Default settings for TeX and PyScript
"""

__revision__ = '$Revision: 1.12 $'

from pyscript.base import UNITS  # , Color
#from pyscript.arrowheads import *


class defaults:
    """
    A class to hold default settings
    """

    tex_head = r"""\documentclass{article}
    \pagestyle{empty}
    \begin{document}
    """
    tex_tail = r"\end{document}"
    tex_command = "latex -interaction=batchmode %s"

    dvips_options = "-Ppdf"
  
    units = UNITS['cm']

    linewidth = 0.5
    linecap = 1  #0=butt, 1=round, 2=square
    linejoin = 0 #0=miter, 1=round, 2=bevel

    # miterlimit:
    # 1.414 cuts off miters at angles less than 90 degrees.
    # 2.0 cuts off miters at angles less than 60 degrees.
    # 10.0 cuts off miters at angles less than 11 degrees.
    # 1.0 cuts off miters at all angles, so that bevels are always produced
    miterlimit = 10  

    dash = None

    # the default arrow head to use in Arrow and DoubleArrow
    # this causes import recursiveness at the moment... 
    #arrowhead=ArrowHead()

    # a 'color' of None is transparent 
    #fg=Color(0)
    #bg=None

# vim: expandtab shiftwidth=4:
