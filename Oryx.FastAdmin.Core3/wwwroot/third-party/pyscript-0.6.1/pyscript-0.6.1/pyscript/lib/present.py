# Copyright (C) 2003-2006  Alexei Gilchrist and Paul Cochrane
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

'''
PyScript presentation library (posters and talks)
'''

from pyscript.defaults import defaults
from pyscript import Group, Rectangle, Color, TeX, Area, Align, Page, \
        VAlign, Page, Text, P, Bbox, Pages

__revision__ = '$Revision: 1.8 $'

# ----------------------------------------------------------------------
# First some useful components
# ----------------------------------------------------------------------

class Box(Group, Rectangle):
    '''
    Draws a box around an object,
    the box can be placed acording to standard Area tags

    @cvar pad: padding around object
    @cvar width: overide the width of the box
    @cvar height: override the height of the box
    '''

    # set these preferences different from Rectangle:
    fg = Color(0)
    bg = Color(1)
    pad = .2

    width = None
    height = None

    def __init__(self, obj, **options):
        Rectangle.__init__(self, **options)
        Group.__init__(self, **options)

        bbox = obj.bbox()

        self.object = obj

        w = bbox.width+2*self.pad
        h = bbox.height+2*self.pad


        # overide the width and height if supplied
        if self.width is None:
            self.width = options.get('width', w)
        if self.height is None:
            self.height = options.get('height', h)

        self.append(
            Rectangle(width=self.width, height=self.height,
                      bg=self.bg, fg=self.fg,
                      c=obj.c, r=self.r, 
                      linewidth=self.linewidth, dash=self.dash),
            obj,
            )

# ----------------------------------------------------------------------
class  TeXArea(Group):
    '''
    Typeset some LaTeX within a fixed width minipage environment.

    @cvar width: the width of the environment
    @type width: float

    @cvar iscale: initial scale of the tex
    @type iscale: float

    @cvar align: alignment of the LaTeX to box if it iss smaller than 
    the full width
    @type align: string (anchor point)

    @cvar fg: color of TeX
    @type fg: L{Color} object
    '''

    # has to be different from groups width attribute
    width = 9.4
    iscale = 1
    fg = Color(0)
    align = "w"

    def __init__(self, text, **options):
        Group.__init__(self, **options)

        # set up tex width ... this relies on latex notion of
        # a point being accurate ... adjust for tex_scale too
        width_pp = int(self.width/float(self.iscale)*defaults.units)

        t = TeX(r'\begin{minipage}{%dpt}%s\end{minipage}'%(width_pp, text),
              fg=self.fg, iscale=self.iscale)

        # use this for alignment as the latex bounding box may be smaller
        # than the full width
        a = Area(width=self.width, height=0)

        Align(t, a, a1=self.align, a2=self.align, space=0)

        self.append(a, t)
        #apply(self, (), options)


# ----------------------------------------------------------------------
# Poster class
# ----------------------------------------------------------------------

class Poster(Page, VAlign):
    '''
    A poster class  

    @cvar size: the size of the poster eg A0
    @cvar orientation: portrait or landscape
    @cvar space: space between vertically aligned objects appended to poster
    @cvar topspace: initial space at top of poster
    @cvar bg: background color of poster (unless background() method 
    is overiden)
    '''

    size = "A0"
    orientation = "portrait"

    bg = Color('DarkSlateBlue')

    space = 1

    topspace = 2

    def __init__(self, *objects, **options):
        Page.__init__(self, **options)
        VAlign.__init__(self, **options)

        back = self.background()

        # use Page's append so background doesn't get aligned
        Page.append(self, back)

        # add invisible area at top to start alignment
        a = Area(width=0, height=self.topspace-self.space, n=self.area().n)
        self.append(a)


    def background(self):
        '''
        Return background for poster
        '''
        area = self.area()

        signature = Text(
                'Created with PyScript.  http://pyscript.sourceforge.net', 
                size=14, fg=Color(1))

        signature.se = area.se+P(-.5, .5)
        return Group(
                Rectangle(width=area.width, height=area.height, 
                    fg=None, bg=self.bg),
                signature,
                )


# ----------------------------------------------------------------------
# Talk Class
# ----------------------------------------------------------------------
class Pause(object):
    '''
    A marker object to split slides to simulate a pause
    can appear anywhere in the talk
    '''
    def bbox(self):
        """
        Return the bounding box
        """
        return Bbox()

# ----------------------------------------------------------------------
class Talk(Pages):
    """
    A Talk class for seminar presentations
    """

    def append(self, *slides_raw):
        """
        Append slides to the Talk

        @param slides_raw: list of slides to append to the talk
        @type slides_raw: list
        """

        slides = []

        pg = 1    
        for slide in slides_raw:

            slide(label = str(pg))
            pg += 1
            # find any Pauses present
            pauses = []
            f = slide.flatten()
            for ii in range(len(f)):
                if isinstance(f[ii][0], Pause):
                    pauses.append(ii)

            for pause in pauses:
                # create a copy and remove everything from Pause onwards
                print "Found Pause(): splitting slide in two"
                s = slide.copy()
                fs = s.flatten()
                for obj, group in fs[pause:]:
                    group.objects.remove(obj)
                slides.append(s)

            slides.append(slide)

        for slide in slides:
            Pages.append(self, slide)

    def write(self, fp, title="PyScriptPS"):
        """
        Write the talk out to file

        @param fp: the file handle of the file to write to
        @type fp: filehandle

        @param title: the title to use in the output postscript
        @type title: string
        """
        tot = len(self)
        for pp in range(tot):
            self[pp].make(page=pp, total=tot)

        Pages.write(self, fp, title)


# ----------------------------------------------------------------------
class EmptySlide(Page):
    """
    An empty slide class
    """

    title = None
    orientation = "Landscape"
    size = "screen"

    def flatten(self, thegroup=None, objects=[]):
        '''
        Return a flattened list of objects
        '''
        if thegroup is None:
            objects = []
            objects = self.flatten(self.objects, objects)
        else:
            for obj in thegroup:
                objects.append((obj, thegroup))
                if isinstance(obj, Group):
                    objects = self.flatten(obj, objects)
        return objects

    def append(self, *items, **options):
        """
        Append items to the slide

        @param items: list of PyScript objects to append
        @type items: list

        @param options: dictionary of options (where to append object etc)
        @type options: dict
        """

        a1 = options.get('a1', None)
        a2 = options.get('a2', None)

        if (a1 is not None) and (a2 is not None) and len(items)>0:
            assert a1 in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]
            assert a2 in ["n", "ne", "e", "se", "s", "sw", "w", "nw", "c"]

            area = self.main()    
            setattr(items[0], a1, getattr(area, a2))

        return Page.append(self, *items)

    def append_n(self, *items):
        """
        Append items using the "north" attribute of the objects

        @param items: list of objects to append
        @type items: list
        """
        return apply(self.append, items, {'a1':'n', 'a2':'n'})
    def append_s(self, *items):
        """
        Append items using the "south" attribute of the objects

        @param items: list of objects to append
        @type items: list
        """
        return apply(self.append, items, {'a1':'s', 'a2':'s'})
    def append_e(self, *items):
        """
        Append items using the "east" attribute of the objects

        @param items: list of objects to append
        @type items: list
        """
        return apply(self.append, items, {'a1':'e', 'a2':'e'})
    def append_w(self, *items):
        """
        Append items using the "west" attribute of the objects

        @param items: list of objects to append
        @type items: list
        """
        return apply(self.append, items, {'a1':'w', 'a2':'w'})
    def append_c(self, *items):
        """
        Append items using the "centre" attribute of the objects

        @param items: list of objects to append
        @type items: list
        """
        return apply(self.append, items, {'a1':'c', 'a2':'c'})

    def main(self):
        """
        Return the bounding box of the slide
        """
        bbox = self.bbox()
        return bbox

    def make_back(self):
        """
        Make the background of the slide
        """
        return None

    def make_title(self):
        """
        Make the title of the slide
        """
        return None

    def clear(self):
        """
        Clear the slide of objects
        """
        Page.clear(self)

    def make(self, page=1, total=1):
        """
        Make the slide

        @param page: the page in the sequence of slides to make (default=1)
        @type page: int

        @param total: the total number of slides in the talk (default=1)
        @type total: int
        """
        self.page = page
        self.total = total

        b = self.make_back()
        if b is not None:
            self.insert(0, b)

        t = self.make_title()
        if t is not None:
            self.append(t)

# vim: expandtab shiftwidth=4:

