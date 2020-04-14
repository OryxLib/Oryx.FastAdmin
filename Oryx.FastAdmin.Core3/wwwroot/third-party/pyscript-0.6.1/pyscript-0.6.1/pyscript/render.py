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

# $Id: render.py,v 1.31 2006/02/28 17:08:10 paultcochrane Exp $

"""
Create the actual postscript
"""

__revision__ = '$Revision: 1.31 $'

from pyscript.defaults import defaults
from pyscript.groups import Eps, Page, Pages
from pyscript.vectors import P


# ---------------------------------------------------------------------------
# Create the actual postscript
# ---------------------------------------------------------------------------


def render(*objects, **options):
    '''
    render the file

    @param objects: list of objects to render
    @type objects: list

    @param options: dictionary of options to use when rendering
    @type options: dict
    '''

    if not options.has_key('file'):
        raise LookupError, "No filename given"

    out = open(options['file'], "w")

    if len(objects) == 0:
        raise ValueError, "No objects to render!"
    elif len(objects) == 1:
        if isinstance(objects[0], Eps):
            obj = objects[0]
        elif isinstance(objects[0], Pages):
            obj = objects[0]
        elif isinstance(objects[0], Page):
            # wrap in Pages environment
            obj = apply(Pages, objects)
        else:
            # assume it's an eps and wrap
            obj = apply(Eps, objects)
    else:
        if isinstance(objects[0], Page):
            # assume we have pages
            obj = apply(Pages, objects)
        else:
            # we have an eps
            obj = apply(Eps, objects)
        
    if isinstance(obj, Eps):
        # Make the sw corner (0,0) since some brain-dead previewers 
        # don't understand bounding-boxes
        x1, y1, x2, y2 = obj.bbox_pp()
        obj.move( (P(0, 0) - P(x1, y1))/float(defaults.units) )

    obj.write(out)
    out.close()

    print "Wrote", options['file']


# vim: expandtab shiftwidth=4:
