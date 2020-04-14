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

# $Id: __init__.py,v 1.19 2006/03/01 09:59:03 paultcochrane Exp $

"""
PyScript - Postscript graphics from python
"""

from pyscript.defaults import defaults
from pyscript.groups \
        import Group, Align, VAlign, HAlign, Distribute, \
        PSMacros, collecttex, TeXstuff, Eps, Page, Pages
from pyscript.objects \
        import AffineObj, Area, TeX, Text, Rectangle, Circle, \
        Dot, Paper, Epsf
from pyscript.render import render
from pyscript.path import C, Path, Arrow, DoubleArrow
from pyscript.arrowheads import ArrowHead, ArrowHead1, ArrowHead2, \
        ArrowHead3, ArrowHead4
from pyscript.base \
        import PsObj, Dash, UNITS, Color, PyScriptError, FontError, Dash
from pyscript.vectors import P, Matrix, R, U, Cusp, Identity, Bbox

__revision__ = '$Revision: 1.19 $'

# vim: expandtab shiftwidth=4:
