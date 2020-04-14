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

# $Id: presentation.py,v 1.39 2006/04/24 14:07:10 paultcochrane Exp $

'''
PyScript presentation library (posters and talks)

There are some common useful component classes such as TeXBox and Box_1, 
followed by Poster and Talk classes
'''

__revision__ = '$Revision: 1.39 $'

from pyscript.defaults import defaults
from pyscript import Color, Group, Epsf, Area, P, Align, Rectangle, TeX, \
        Page, Distribute, Text, Pages, VAlign, Path
from pyscript.render import render
import os, types

class TeXBox(Group):
    '''
    Typeset some LaTeX within a fixed width box.
    
    @ivar fixed_width: the width of the box
    @type fixed_width: float

    @ivar tex_scale: The amount by which to scale the TeX
    @type tex_scale: float

    @ivar align: alignment of the LaTeX to box if it is smaller
    @type align: anchor string
    '''

    def __init__(self, text, fixed_width=9.4, tex_scale=0.7, align="w",
            fg = Color(0), text_style="",
            **options):
        Group.__init__(self, **options)

        self.text = text
        self.fixed_width = fixed_width
        self.tex_scale = tex_scale
        self.fg = fg
        self.align = align
        self.text_style = text_style

    def set_fg(self, fg):
        """
        Set the foreground colour
        """
        self.fg = fg

    def set_fixed_width(self, fixed_width):
        """
        Set the fixed width attribute
        """
        self.fixed_width = fixed_width

    def set_tex_scale(self, tex_scale):
        """
        Set the scale of TeX objects
        """
        self.tex_scale = tex_scale

    def set_align(self, align):
        """
        Set the anchor point where to align objects
        """
        self.align = align

    def set_text_style(self, text_style):
        """
        Set the text style (in LaTeX)
        """
        self.text_style = text_style

    def make(self):
        """
        Make the TeXBox
        """
        width_pp = int(self.fixed_width/float(self.tex_scale)*defaults.units)

        al = Align(a1=self.align, a2=self.align, space=0)

        t = TeX(r'\begin{minipage}{%dpt}%s %s\end{minipage}' \
                % (width_pp, self.text_style, self.text), 
                fg=self.fg)

        t.scale(self.tex_scale, self.tex_scale)
        al.append(t)
        
        a = Area(width=self.fixed_width, height=0)
        al.append(a)

        self.append(al)
        return self
        #apply(self, (), options)  # why do we do this???

class Box_1(Group):
    '''
    A box of fixed width. Items added to it are aligned vertically and
    separated by a specified padding
    
    @cvar border: width of the border (in pts)
    @type border: int

    @cvar fg: color of border
    @type fg: L{Color} object

    @cvar bg: color of box background
    @type bg: L{Color} object

    @cvar fixed_width: width of box
    @type fixed_width: float

    @cvar pad: vertical padding between items
    @type pad: float

    @cvar r: corner radius
    @type r: float
    '''
    
    bg = Color('Lavender')
    fg = Color(0)
    border = 1
    fixed_width = 9.6
    pad = 0.2
    r = 0

    def __init__(self, *items, **options):
        Group.__init__(self, **options)
        
        apply(self.append, items)

        Align(self, a1="s", a2="n", angle=180, space=self.pad)

        gb = self.bbox()

        r = Rectangle(n=gb.n+P(0, self.pad),
                    width=self.fixed_width,
                    height=gb.height+2*self.pad,
                    bg=self.bg,
                    fg=self.fg,
                    linewidth=self.border,
                    r=self.r,
                    )

        self.insert(0, r)

class CodeBox(Group):
    """
    A box with a 'dog-ear' to contain code fragments
    """

    def __init__(self, text, **options):
        Group.__init__(self, **options)

        obj = TeXBox(text)
        obj.make()

        bg = Color('Orange')*1.3
        fg = Color('black')*0.4
        border = 0.75
        #fixed_width = 2.5
        pad = 0.2
        dogear = 0.25
        gb = obj.bbox()
        h = gb.height
        w = gb.width

        self.append(Path(
            P(-pad, -pad),
            P(-pad, h+pad),
            P(w+pad, h+pad),
            P(w+pad, -pad+dogear),
            P(w+pad-dogear, -pad),
            P(w+pad-dogear, -pad+dogear),
            P(w+pad, -pad+dogear),
            P(w+pad-dogear, -pad),
            P(-pad, -pad),
            bg=bg,
            fg=fg,
            linewidth=border,
            miterlimit=1.0)
            )
        obj.c = self.bbox().c

        self.append(obj)

class Poster(Page):
    """
    A poster class.

    More docs forthcoming...
    """
    def __init__(self, size, style=None):
        Page.__init__(self)

        # set stuff up
        self.size = size
        self.orientation = "Portrait"
        self.num_columns = 2

        # set the default style settings
        self.title = ""
        self.title_fg = Color(0)
        self.title_scale = 1.4
        self.title_width = 0.8  # as a fraction of the total poster width
        self.title_text_style = "\large"

        self.authors = ""
        self.authors_fg = Color(0)
        self.authors_scale = 1
        self.authors_width = 0.8  # as a fraction of the total poster width
        self.authors_text_style = ""

        self.address = ""
        self.address_fg = Color(0)
        self.address_scale = 0.9
        self.address_width = 0.8  # as a fraction of the total poster width
        self.address_text_style = ""

        self.abstract = ""
        self.abstract_fg = Color(0)
        self.abstract_scale = 0.8
        self.abstract_width = 0.92  # relative to total width of poster
        self.abstract_text_style = ""

        self.gutter = 0.2
        self.pad = 0  # should get set by add_column()
        self.item_sep = 0.3

        self.bg = Color(1)

        self.signature_fg = Color(0)

        self.logo_height = 1.2

        # styles for columns
        self.column_item_sep = 0.3

        # styles for column boxes
        # the title's style...
        self.column_box_title_align = "c"
        self.column_box_title_tex_scale = 1.4
        self.column_box_title_fixed_width = 9.4
        self.column_box_title_text_style = r""
        self.column_box_title_fg = Color(0)

        # the text styles of the column box
        self.column_box_text_align = "w"
        self.column_box_tex_scale = 0.7
        self.column_box_text_width = 9.4
        self.column_box_text_style = r""
        self.column_box_text_fg = Color(0)

        # the column box styles
        self.column_box_item_sep = 0.1
        self.column_box_width = 9.9
        self.column_box_bg = Color(1)
        self.column_box_border = 1

        # process the style option
        if style is not None:
            # make sure the file exists in either the .pyscript/styles
            # directory, or the current directory
            styleFname = style + ".py"
            HOME = os.path.expandvars("$HOME")
            if os.path.exists(HOME + "/.pyscript/styles/" + styleFname):
                print "Found %s in .pyscript/styles dir" % style
                self._read_style(HOME + "/.pyscript/styles/" + styleFname)
            elif os.path.exists(styleFname):
                print "Found %s in current dir" % style
                self._read_style(styleFname)
            else:
                # barf
                raise ValueError, "Style %s not found!" % style

        self.logos = []
        self.columns = []

        self.area = self.area()

        # subtract the gutter to get the printing area
        self.printing_area = Area(
                sw=self.area.sw + P(1, 1)*self.gutter,
                width=self.area.width - 2*self.gutter,
                height=self.area.height - 2*self.gutter
                )

    def _read_style(self, styleFname):
        """
        Read the talk style file

        @param styleFname: The name of the style file to process
        @type styleFname: string
        """
        # slurp in the text
        fp = open(styleFname, "r")
        lines = fp.readlines()
        fp.close()

        # make one big string...
        styleText = ""
        for line in lines:
            styleText += line

        # exec the text
        exec(styleText)

    def set_title(self, title):
        """
        Set the title to use for the poster

        @param title: the text of the poster title
        @type title: string
        """
        self.title = title
        pass

    def set_authors(self, authors):
        """
        Set the authors of the poster

        @param authors: the text of the poster authors
        @type authors: string
        """
        self.authors = authors
        pass

    def set_address(self, address):
        """
        Set the address of the institution of those presenting the poster

        @param address: the text of the address
        @type address: string
        """
        self.address = address
        pass

    def set_abstract(self, abstract):
        """
        Set the abstract of the poster

        @param abstract: the text of the poster abstract
        @type abstract: string
        """
        self.abstract = abstract
        pass

    def set_size(self, size):
        """
        Set the size of the poster.
        
        These are standard page sizes.   It is a good idea to develop a
        poster at a size of "a4" and then for the final poster use "a0".  It
        is also handy when at a poster session at a conference to have a4
        size versions of the a0 poster to give out to people, so the a4
        setting is also handy for that.

        @param size: the size of the poster
        @type size: string
        """
        self.size = size
        pass

    def set_style(self, style):
        """
        Set the style of the poster.  This is the name of a set of
        predefined fonts, sizes, colours etc for the text, the columns of
        the poster and the poster background.

        @param style: the text of the name of the poster style to use
        @type style: string
        """
        self.style = style
        pass

    def set_orientation(self, orientation):
        """
        Set the orientation of the poster.  Options are "portrait" or
        "landscape".

        @param orientation: the page orientation of the poster
        @type orientation: string
        """
        self.orientation = orientation
        pass

    def set_num_columns(self, num_columns):
        """
        Set the number of columns to use for the poster.  Typically one
        would use two columns for portrait, and three for landscape.

        @param num_columns: the number of columns to use
        @type num_columns: int
        """
        self.num_columns = num_columns
        pass

    def add_logo(self, logo, height=None):
        """
        Add a logo to the poster.

        If only one logo is added to the poster, it is by default located at
        the top left-hand corner.  The second logo is then positioned in the
        top right-hand corner.  The third is positioned in the top middle.
        If you add more than that, the logos are distributed evenly across
        the top of the poster.

        @param logo: the file name of the eps file of the logo to add
        @type logo: text

        @param height: the height of the logo
        @type height: float
        """
        if height is None:
            height = self.logo_height

        obj = Epsf(logo, height=height)
        self.logos.append(obj)

    def add_logos(self, *logos, **options):
        """
        Add several logos to the poster at one time.

        If only one logo is added to the poster, it is by default located at
        the top left-hand corner.  The second logo is then positioned in the
        top right-hand corner.  The third is positioned in the top middle.
        If you add more than that, the logos are distributed evenly across
        the top of the poster.

        @param logos: list of file names of the eps files of the logos to add
        @type logos: list of strings

        @keyword height: the height of the logo
        @type height: float
        """
        # process the options, if any
        if options.has_key('height'):
            height = options['height']
        else:
            height = self.logo_height

        for logo in logos:
            obj = Epsf(logo, height=height)
            self.logos.append(obj)

    def add_column(self, column, side):
        """
        Add a column of the poster.

        @param column: the Column object to add as the poster column
        @type column: Column object

        @param side: the side on which the column is to be on the poster.
        Valid values are "left", "middle" (useful for landscape only) and
        "right".
        @type side: string
        """
        # there must be a better way to write this if statement,
        # something like if side is not in [left, middle, right] ???
        if side != 'left' and side != 'middle' and side != 'right':
            errMsg = "You must specify either 'left', 'middle', or 'right'\n"
            errMsg += "I got: '%s'" % side
            raise ValueError, errMsg
                    
        self.columns.append(column._make())

    def _make_title(self):
        """
        Make the title
        """
        titlebox = TeXBox(text=self.title)
        titlebox.set_fg(self.title_fg)
        titlebox.set_fixed_width(self.printing_area.width*self.title_width)
        titlebox.set_tex_scale(self.title_scale)
        titlebox.set_align("c")
        titlebox.set_text_style(self.title_text_style)
        titlebox.make()

        return titlebox

    def _make_authors(self):
        """
        Make the authors
        """
        authorbox = TeXBox(self.authors)
        authorbox.set_fg(self.authors_fg)
        authorbox.set_tex_scale(self.authors_scale)
        authorbox.set_fixed_width(self.printing_area.width*self.authors_width)
        authorbox.set_align("c")
        authorbox.set_text_style(self.authors_text_style)
        authorbox.make()

        return authorbox

    def _make_address(self):
        """
        Make the address
        """
        addressbox = TeXBox(self.address)
        addressbox.set_fg(self.address_fg)
        addressbox.set_tex_scale(self.address_scale)
        addressbox.set_fixed_width(self.printing_area.width*self.address_width)
        addressbox.set_align("c")
        addressbox.set_text_style(self.address_text_style)
        addressbox.make()

        return addressbox

    def _make_abstract(self):
        """
        Make the abstract
        """
        abstractbox = TeXBox(self.abstract)
        abstractbox.set_fg(self.abstract_fg)
        abstractbox.set_tex_scale(self.abstract_scale)
        abstractbox.set_fixed_width(
                self.printing_area.width*self.abstract_width)
        abstractbox.set_align("c")
        abstractbox.set_text_style(self.abstract_text_style)
        abstractbox.make()

        return abstractbox

    def _make_logos(self):
        """
        Make the logos
        """
        logos = Align(a1="e", a2="w", angle=90, space=None)
        for logo in self.logos:
            logos.append(logo)

        Distribute(logos, a1="e", a2="w",
                p1=self.printing_area.nw,
                p2=self.printing_area.ne,
                )

        return logos

    def _make_columns(self):
        """
        Make the columns
        """

        #print "Number of columns is: %d" % len(self.columns)

        # vertically align the columns items, but with no spacing yet
        #for col in self.columns:
            #VAlign(col, space=None)

        # distribute the columns horizontally
        if self.num_columns == 2:
            Distribute(Area(width=0, height=0),
                    self.columns[0], self.columns[1],
                    Area(width=0, height=0),
                    p1=self.printing_area.w,
                    p2=self.printing_area.e,
                    a1="e", a2="w")
        elif self.num_columns == 3:
            Distribute(Area(width=0, height=0),
                    self.columns[0], self.columns[1], self.columns[2],
                    Area(width=0, height=0),
                    p1=self.printing_area.w,
                    p2=self.printing_area.e,
                    a1="e", a2="w")
        else:
            raise ValueError, \
                "Incorrect number of columns. Should be 2 or 3. I got %d" % \
                self.num_columns

        # find the distance between two of the columns
        self.pad = (self.columns[1].bbox().w - self.columns[0].bbox().e)[0]

        # vertically align the column items
        #print self.pad
        #for col in self.columns:
            #VAlign(col, space=self.pad)

        # now align the columns themselves
        all_cols = Align(angle=90, space=None, a1="ne", a2="nw")
        for col in self.columns:
            #col.set_space(self.pad)
            col.set_space(0)
            #print col.get_space()
            all_cols.append(col._make())

        return all_cols

    def _make_background(self):
        """
        Make the background of the poster
        """
        area = self.area()

        return Rectangle(width=area.width,
                height=area.height,
                fg=None,
                bg=self.bg
                )

    def make(self, file):
        """
        Make the poster.

        @param file: the file name of the poster output eps file
        @type file: string
        """
        all = Align(a1="s", a2="n", angle=180, space=self.item_sep)
        all.append(self._make_logos())
        all.append(self._make_title())
        all.append(self._make_authors())
        all.append(self._make_address())
        all.append(self._make_abstract())
        all.append(self._make_columns())

        all.n = self.printing_area.n - P(0, 0.1)

        back = self._make_background()

        p = self.printing_area.se+P(0, 1.2)
        signature = Text(
                "Created with PyScript.  http://pyscript.sourceforge.net",
                size=6, sw=p, fg=self.signature_fg).rotate(-90, p)

        self.append(back)
        self.append(all)
        self.append(signature)

        # actually generate the postscript
        render(self, file=file)

class Column(VAlign):  # I *think* this should inherit from VAlign...
    """
    A column of a poster.  Basically just a container for various boxes.

    More docs forthcoming...
    """
    def __init__(self, poster):
        VAlign.__init__(self)
        #Group.__init__(self)

        self.boxes = []
        self.space = poster.column_item_sep

    def add_box(self, box):
        """
        Add a box to the column

        @param box: the box to add to the column
        @type box: ColumnBox object
        """
        self.append(box._make())

    def set_space(self, space):
        """
        Set the spacing of the column items

        @param space: the space between the items
        @type space: float
        """
        #print "Column.set_space()"
        self.space = space

    def get_space(self):
        """
        Get the spacing of the column items
        """
        return self.space

    def _make(self):
        """
        Make the column
        """
        #print "Column._make()"
        for box in self.boxes:
            self.append(box._make())

        return self

class ColumnBox(Group):
    """
    A box, containing various objects, with a fixed width, but variable
    height

    Should add more docs here too...
    """
    def __init__(self, poster):
        Group.__init__(self)

        self.title_align = poster.column_box_title_align
        self.title_tex_scale = poster.column_box_title_tex_scale
        self.title_fixed_width = poster.column_box_title_fixed_width
        self.title_text_style = poster.column_box_title_text_style
        self.title_fg = poster.column_box_title_fg

        self.align = poster.column_box_text_align
        self.tex_scale = poster.column_box_tex_scale
        self.fixed_width = poster.column_box_text_width
        self.text_style = poster.column_box_text_style
        self.fg = poster.column_box_text_fg

        self.item_sep = poster.column_box_item_sep
        self.box_width = poster.column_box_width
        self.box_bg = poster.column_box_bg
        self.box_border = poster.column_box_border

        self.title = ""
        self.items = []

    def set_title(self, title):
        """
        Set the title of the box within the column

        @param title: the title of the column box
        @type title: string
        """
        self.title = title

    def add_TeXBox(self, text):
        """
        Adds a TeXBox object to the column

        @param text: the text of the TeXBox object to add
        @type text: string
        """
        texbox = TeXBox(text)
        texbox.make()
        # other settings here...
        self.items.append(texbox)

    def add_fig(self, fig, height=None, width=None, bg=Color(1)):
        """
        Add an arbitrary figure to the column box, with a background.

        This could be a previously defined pyscript diagram (for instance).
        If only one of the height or width is given then the figure is
        scaled appropriately, maintaining the original aspect ratio.

        @param fig: the figure to add
        @type fig: PyScript object

        @param width: the width of the figure
        @type width: float

        @param height: the height of the figure
        @type height: float

        @param bg: the colour of the figure background 
        @type bg: Color object
        """
        # get the figure's current height and width
        oldHeight = fig.bbox().height
        oldWidth = fig.bbox().width

        # scale the figure appropriately
        if height is not None and width is None:
            scale = height/oldHeight
            fig = fig.scale(scale, scale)
        elif height is None and width is not None:
            scale = width/oldWidth
            fig = fig.scale(scale, scale)
        elif height is not None and width is not None:
            xscale = width/oldWidth
            yscale = height/oldHeight
            fig = fig.scale(xscale, yscale)
        else:
            # leave well alone...
            pass

        # put a white background on it
        gutter = 0.1
        rect = Rectangle(width=fig.bbox().width+gutter, 
                height=fig.bbox().height+gutter,
                c=fig.bbox().c,
                bg=bg, fg=None)

        # group everything together
        all = Group()
        all.append(rect, fig)

        # append it to the list of items in the ColumnBox
        self.items.append(all)

    def add_epsf(self, file, height=None, width=None):
        """
        Add an eps file to the column box.

        If only one of the height or width is given then the figure is
        scaled appropriately, maintaining the original aspect ratio.

        @param file: the file name of the eps file to add
        @type file: string

        @param width: the width of the figure
        @type width: float

        @param height: the height of the figure
        @type height: float
        """
        # load the eps with the appropriate dimensions
        if height is not None and width is None:
            eps = Epsf(file=file, height=height)
        elif height is None and width is not None:
            eps = Epsf(file=file, width=width)
        elif height is not None and width is not None:
            eps = Epsf(file=file, width=width, height=height)
        else:
            # use the file's own size
            eps = Epsf(file=file)

        # append it to the list of items in the ColumnBox
        self.items.append(eps)

    def add_object(self, obj):
        """
        Add a pre-defined object to the box, this could be an Align or Group
        object for example

        @param obj: the object to be added
        @type obj: pyscript object
        """
        self.items.append(obj)

    def add_text(self, text):
        """
        Add arbitrarily placed text to the box

        @param text: the text to be added
        @type text: string
        """
        tex = TeX(text)

        self.items.append(tex)

    def _make_title(self):
        """
        Make the title
        """
        titlebox = TeXBox(self.title)
        titlebox.set_align(self.title_align)
        titlebox.set_tex_scale(self.title_tex_scale)
        titlebox.set_fixed_width(self.title_fixed_width)
        titlebox.set_text_style(self.title_text_style)
        titlebox.set_fg(self.title_fg)
        titlebox.make()

        return titlebox

    def _make(self):
        """
        Make the column box object
        """
        valign = VAlign(space=self.item_sep)
        valign.append(self._make_title())
        #print "Number of items in the column box is: %d" % len(self.items)
        for item in self.items:
            valign.append(item)

        # the reason for the BasicBox class is to let the overall poster
        # style handle the width, foreground, etc. etc.
        box = BasicBox()
        box.set_height(valign.bbox().height + 2*box.pad)
        box.set_width(self.box_width)
        box.set_anchor("n", valign.bbox().n+P(0, 0.2))
        #box.n = valign.bbox().n+P(0, 0.2)  # absorb into style ???
        box.set_bg(self.box_bg)
        box.set_border(self.box_border)

        # append the objects to the group
        self.append(box)
        self.append(valign)

        return self

class BasicBox(Rectangle):
    """
    A basic box, with border, and background to use in behind textual and
    other objects
    """
    def __init__(self):
        Rectangle.__init__(self)

        self.bg = Color("lavender")
        self.fg = Color(0)
        self.border = 1
        self.fixed_width = 9.6
        self.pad = 0.2
        self.radius = 0
        self.width = 9.9
        self.height = 1
        self.anchor = "n"

    def set_height(self, height):
        """
        Set the height of the box
        """
        self.height = height

    def set_width(self, width):
        """
        Set the width of the box
        """
        self.width = width

    def set_bg(self, bg):
        """
        Set the background colour
        """
        self.bg = bg

    def set_fg(self, fg):
        """
        Set the foreground colour
        """
        self.fg = fg

    def set_border(self, border):
        """
        Set the width of the border around the box
        """
        self.linewidth = border

    def set_radius(self, radius):
        """
        Set the radius of the corners of the box, if they are rounded
        """
        self.r = radius

    def set_pad(self, pad):
        """
        Set the padding around the box
        """
        self.pad = pad

    def set_anchor(self, anchor, location):
        """
        Set the anchor location (c, n, ne, e, se, s, sw, w, nw)
        """
        exec("self.%s = location" % anchor)

class Poster_1(Page):
    '''
    A poster style, portrait orientation very similar to a 
    journal article's front page. Title, authors and abstract across
    top. two columns for boxes with details. It is set up for A4 paper
    which can then be scaled for A0 etc.
    
    @cvar bg: poster background
    
    @cvar gutter: nonprintable margin around entire poster
    
    @cvar title: TeX of title
    @cvar title_fg: fg color of title
    @cvar title_scale: scale of title TeX
    @cvar title_width: proportion of total width for title
    
    @cvar authors: TeX of authors
    @cvar authors_fg: fg color of authors
    @cvar authors_scale: scale of authors TeX
    @cvar authors_width: proportion of total width for authors
    
    @cvar address: TeX of address
    @cvar address_fg: fg color of address
    @cvar address_scale: scale of address TeX
    @cvar address_width: proportion of total width for address
    
    @cvar abstract: TeX of abstract
    @cvar abstract_fg: fg color of abstract
    @cvar abstract_scale: scale of abstract TeX
    @cvar abstract_width: proportion of total width for abstract
    
    @cvar logos: a list of filenames for the logos
    @cvar logo_height: the height to which to scale the logos
    
    @cvar printing_area: an Area the size of the page minus the gutter
    
    @cvar col1: a Group() containing left column objects
    @cvar col2: a Group() containing right column objects

    '''
    col1 = Group()
    col2 = Group()
    logos = ()

    def __init__(self):

        Page.__init__(self)
        
        self.size = "A4"
        self.gutter = 0.2 # paper margin for A4 in cm

        self.bg = Color('DarkSlateBlue')

        self.title = ""
        self.title_fg = Color('Yellow')
        self.title_scale = 1.4
        self.title_width = 0.8

        self.address = ""
        self.address_fg = Color(0)
        self.address_scale = 1
        self.address_width = 0.8

        self.authors = ""
        self.authors_fg = Color(0)
        self.authors_scale = 1
        self.authors_width = 0.8
        
        self.abstract = ""
        self.abstract_fg = Color(0)
        self.abstract_scale = 0.8
        self.abstract_width = 0.8
        
        self.logo_height = 0.8
        #self.logos = ()

        #self.col1 = Group()
        #self.col2 = Group()

        self.signature_fg = self.bg*0.8
        
        area = self.area()
        
        # subtract the gutter to get printing area
        self.printing_area = Area(
            sw=area.sw+P(1, 1)*self.gutter,
            width=area.width-2*self.gutter,
            height=area.height-2*self.gutter
            )

    def add_fig(self, file, width=5.0):
        """
        This method needs to be fixed up.  It's not to put a figure on the
        page, but an eps file...
        """
        
        fig = Epsf(file)
        rect1 = Rectangle(c=fig.c,
                width=fig.bbox().width+0.1,
                height=fig.bbox().height+0.1,
                fg=Color('black'),
                bg=Color('white'),
                linewidth=0.5,
                )
        out_fig = Group(rect1,fig)
        out_fig.scale(width/out_fig.bbox().width,width/out_fig.bbox().width)
        return out_fig

    def add_epsf(self):
        """
        Add and EPS file to the poster
        """
        pass

    def _make_logos(self):
        """
        Make and return a Group object of the logos
        """

        #thelogos = Group()
        thelogos = Align(a1="e", a2="w", angle=90, space=None)
        for logo in self.logos:
            thelogos.append(Epsf(logo, height=self.logo_height))
            
        Distribute(thelogos, a1="e", a2="w",
                   p1=self.printing_area.nw,
                   p2=self.printing_area.ne)

        #Align(thelogos, a1="e", a2="w", angle=90, space=None)

        return thelogos

    def _make_title(self):
        '''
        Return a title object
        '''

        return TeXBox(self.title, fg=self.title_fg,
                      fixed_width=self.printing_area.width*self.title_width,
                      tex_scale=self.title_scale,
                      align="c").make()

    def _make_address(self):
        """
        Return an address object
        """
        return TeXBox(self.address,
                    fg=self.address_fg,
                    fixed_width=self.printing_area.width*self.address_width,
                    tex_scale=self.address_scale,
                    align="c").make()

    def _make_abstract(self):
        '''
        Return the abstract object
        '''
        
        return TeXBox(self.abstract,
                      fixed_width=self.printing_area.width*self.abstract_width,
                      tex_scale=self.abstract_scale,
                      fg=self.abstract_fg, align="c").make()

    def _make_authors(self):
        '''
        Return authorlist object
        '''

        return TeXBox(self.authors,
                      fg=self.authors_fg,
                      tex_scale=self.authors_scale,
                      fixed_width=self.printing_area.width*self.authors_width,
                      align="c").make()

    def _make_background(self):
        '''
        Return background (block color)
        '''
        area = self.area()
        
        return Rectangle(width=area.width,
                         height=area.height,
                         fg=None,
                         bg=self.bg
                         )
        
    def make(self):
        '''
        Create the actual poster aligning everything up.
        calls make_title(), make_authors() etc
        '''

        # NB: A0 = 4x A4
        
        # vertically align the column items ... no spacing yet!
        Align(self.col1, a1="s", a2="n", angle=180, space=None)
        Align(self.col2, a1="s", a2="n", angle=180, space=None)

        # Distribute the cols horizontally
        Distribute(Area(width=0, height=0),
                   self.col1, self.col2,
                   Area(width=0, height=0),
                   p1=self.printing_area.w,
                   p2=self.printing_area.e, 
                   a1="e", a2="w")
        
        # find the distance between the cols
        pad = (self.col2.bbox().w-self.col1.bbox().e)[0]

        # vertically align the column items
        Align(self.col1, a1="s", a2="n", angle=180, space=pad)
        Align(self.col2, a1="s", a2="n", angle=180, space=pad)        

        # align the two columns themselves
        cols = Align(self.col1, self.col2, 
                angle=90, space=None, a1="ne", a2="nw")
        
        all = Align(
            self._make_logos(),
            self._make_title(),
            self._make_authors(),
            self._make_address(),
            self._make_abstract(),
            cols,
            a1="s", a2="n", angle=180, space=pad
            )

        all.n = self.printing_area.n-P(0, 0.1)

        back = self._make_background()

        p = self.printing_area.se+P(0, 1.2)
        signature = Text(
                'Created with PyScript.  http://pyscript.sourceforge.net',
                size=6, sw=p, fg=self.signature_fg
                ).rotate(-90, p)

        self.append(back, all, signature)
        
        # return a reference for convenience
        return self


class Talk(Pages):
    """
    A talk class
    """

    def __init__(self, style=None):
        Pages.__init__(self)

        self.slides = []

        self.bg = Color('RoyalBlue')*0.9
        self.fg = self.bg

        self.logos = []
        self.logo_height = 0.8
        
        self.title = ""
        self.title_fg = Color('white')
        self.title_scale = 5
        self.title_textstyle = ""

        self.slide_title = ""
        self.slide_title_fg = Color('white')
        self.slide_title_scale = 5
        self.slide_title_textstyle = ""

        self.footerScale = 1

        self.waitbar_fg = Color('orangered')
        self.waitbar_bg = Color('black')
        
        self.authors = ""
        self.authors_fg = Color('white')
        self.authors_scale = 3
        self.authors_textstyle = ""

        self.speaker = ""   # i.e. who's actually giving the talk
        self.speaker_fg = Color(0)
        self.speaker_textstyle = ""

        self.address = ""
        self.address_fg = Color('white')
        self.address_scale = 2
        self.address_textstyle = ""
        
        self.box_bg = Color('lavender')
        self.box_fg = Color(0)
        self.box_border = 2

        self.text_scale = 3
        self.text_fg = Color(0)
        self.text_textstyle = ""
        
        self.headings_fgs = {
                1 : Color('white'), 
                2 : Color('white'), 
                3 : Color('white'),
                "equation" : Color('white'),
                "default" : Color('white'),
                "space" : self.fg,
                }
        self.headings_scales = { 
                1 : 3, 
                2 : 2.5, 
                3 : 2.2,
                "equation" : 2.5,
                "default" : 1.5,
                "space" : 3,
                }
        self.headings_bullets = {
                1 : TeX(r"$\bullet$"), 
                2 : TeX(r"--"), 
                3 : TeX(r"$\gg$"),
                "equation" : Rectangle(height=1, fg=self.bg, bg=self.bg),
                "default" : TeX(r"$\cdot$"),
                "space" : Rectangle(height=1, fg=self.bg, bg=self.bg),
                }
        self.headings_indent = {
                1 : 0,
                2 : 0.5,
                3 : 1,
                "equation" : 2,
                "default" : 2,
                "space" : 0,
                }
        self.headings_textstyle = {
                1 : "",
                2 : "",
                3 : "",
                "equation" : "",
                "default" : "",
                "space" : "",
                }

        # process the style option
        if style is not None:
            # make sure the file exists in either the .pyscript/styles
            # directory, or the current directory
            styleFname = style + ".py"
            HOME = os.path.expandvars("$HOME")
            if os.path.exists(HOME + "/.pyscript/styles/" + styleFname):
                print "Found %s in .pyscript/styles dir" % style
                self._read_style(HOME + "/.pyscript/styles/" + styleFname)
            elif os.path.exists(styleFname):
                print "Found %s in current dir" % style
                self._read_style(styleFname)
            else:
                # barf
                raise ValueError, "Style %s not found!" % style

    def _read_style(self, styleFname):
        """
        Read the talk style file

        @param styleFname: The name of the style file to process
        @type styleFname: string
        """
        # slurp in the text
        fp = open(styleFname, "r")
        lines = fp.readlines()
        fp.close()

        # make one big string...
        styleText = ""
        for line in lines:
            styleText += line

        # exec the text
        exec(styleText)

    def set_title(self, title):
        """
        Set the title of the talk as a whole

        @param title: the title of the talk
        @type title: string
        """
        self.title = title
        return

    def set_authors(self, authors):
        """
        Set the authors of the talk

        @param authors: the author list for the talk
        @type authors: string
        """
        self.authors = authors
        return

    def set_speaker(self, speaker):
        """
        Set the name of the person actually giving the talk/presentation

        @param speaker: the name of the person giving the talk
        @type speaker: string
        """
        self.speaker = speaker
        return

    def set_address(self, address):
        """
        Set the address for the institution (or equivalent) of the speaker
        
        @param address: the address to use
        @type address: string
        """
        self.address = address
        return

    def add_logo(self, logo, height=None):
        """
        Add a logo to the talk

        @param logo: eps file name of logo
        @type logo: string
        """
        if height is None:
            height = self.logo_height

        self.logos.append(Epsf(file=logo, height=height))

    def _make_authors(self):
        """
        Generate the authors text on the titlepage
        """
        ttext = "%s %s" % (self.authors_textstyle, self.authors)
        return TeX(ttext, fg=self.authors_fg
            ).scale(self.authors_scale, self.authors_scale)

    def _make_address(self):
        """
        Generate the address text on the titlepage
        """
        if isinstance(self.address, types.StringType):
            ttext = "%s %s" % (self.address_textstyle, self.address)
            return TeX(ttext, fg=self.address_fg
                ).scale(self.address_scale, self.address_scale)
        else:
            #raise ValueError, "Can't handle non-string arguments yet"
            return self.address

    def make(self, *slides, **options):
        """
        Routine to collect all of slides together and render them all as
        the one document
        """
        # create the titlepage automatically
        titlepage = Slide(self)
        titlepage.set_titlepage()
        self.slides.append(titlepage)
        
        # create the list of slides
        for slide in slides:
            self.slides.append(slide)

        # add all the slides to the talk
        i = 1
        temp = Pages()
        for slide in self.slides:
            slide.pageNumber = i
            print 'Adding slide', str(i), '...'
            temp.append(slide._make(self))
            i += 1

        # determine the file name to use
        if not options.has_key('file'):
            raise ValueError, "No filename given"
        file = options['file']
        
        # render it!
        render(temp, file=file)

class Slide(Page):
    """
    A slide class.  Use this class to generate the individual slides in a talk
    """
    def __init__(self, talk):
        Page.__init__(self)
        
        self.size = "a4"
        self.orientation = "Landscape"
        self.pageNumber = None
        self.pages = 0  # need to set up an initial value
        self.titlepage = False
        self.authors = None
        self.headings = []
        self.epsf = []
        self.figs = []
        self.area = self.area()
        self.title = None
        self.logos = talk.logos
        self.text_scale = talk.text_scale
        self.text_textstyle = talk.text_textstyle
        self.text_fg = talk.text_fg
        self.textObjs = []

    def _make_logos(self):
        """
        Put the logos on the page
        """
        if len(self.logos) == 0:
            return Area(width=0, height=0)
        elif len(self.logos) == 1:
            return Group(
                Area(width=self.area.width-0.4, height=0),
                self.logos[0]
                )

        width = self.area.width -\
                self.logos[0].bbox().width -\
                self.logos[-1].bbox().width -\
                0.4

        for logo in self.logos[1:-1]:
            width -= logo.bbox().width

        space = width/(len(self.logos)-1)
        a = Align(a1="e", a2="w", angle=90, space=space)
        for logo in self.logos:
            a.append(logo)

        return a

    def add_fig(self, obj, **options):
        """
        Put an arbitrary figure onto the page, with a white background

        @param obj: the PyScript object to use for the figure
        @type obj: PyScript object
        """
        if options.has_key('bg'):
            backColor = options['bg']
        else:
            backColor = Color('white')

        if options.has_key('fg'):
            frontColor = options['fg']
        else:
            frontColor = None

        if options.has_key('height'):
            figHeight = options['height']
        else:
            figHeight = None

        if options.has_key('width'):
            figWidth = options['width']
        else:
            figWidth = None

        gutter = 0.1
        back = Rectangle(width=obj.bbox().width+gutter,
                    height=obj.bbox().height+gutter,
                    bg=backColor, fg=frontColor)
        back.sw = obj.bbox().sw-P(gutter/2.0, gutter/2.0)

        fig = Group(back, obj)

        # now scale the height/width appropriately if figWidth and/or
        # figHeight are set
        if figHeight is not None and figWidth is None:
            if fig.bbox().height == 0.0:
                raise ValueError, "Initial figure height is zero!!"
            else:
                scale = figHeight/fig.bbox().height
            fig.scale(scale, scale)
        elif figHeight is None and figWidth is not None:
            if fig.bbox().width == 0.0:
                raise ValueError, "Initial figure width is zero!!"
            else:
                scale = figWidth/fig.bbox().width
            fig.scale(scale, scale)
        elif figHeight is not None and figWidth is not None:
            if fig.bbox().height == 0.0:
                raise ValueError, "Initial figure height is zero!!"
            elif fig.bbox().width == 0.0:
                raise ValueError, "Initial figure width is zero!!"
            else:
                scalex = figWidth/fig.bbox().width
                scaley = figHeight/fig.bbox().height
                fig.scale(scalex, scaley)

        # there must be a better way to do this!!!
        if options.has_key('e'):
            fig.e = options['e']
        elif options.has_key('se'):
            fig.se = options['se']
        elif options.has_key('s'):
            fig.s = options['s']
        elif options.has_key('sw'):
            fig.sw = options['sw']
        elif options.has_key('w'):
            fig.w = options['w']
        elif options.has_key('nw'):
            fig.nw = options['nw']
        elif options.has_key('n'): 
            fig.n = options['n']
        elif options.has_key('ne'):
            fig.ne = options['ne']
        elif options.has_key('c'):
            fig.c = options['c']
        else:
            fig.sw = P(0.0, 0.0)

        # add the figure to the list of figures
        self.figs.append(fig)

    def set_titlepage(self):
        """
        Set the current slide to be the titlepage
        """
        self.titlepage = True
        return

    def set_title(self, title=None):
        """
        Set the title of the slide
        """
        self.title = title
        return

    def _make_title(self, talk):
        """
        Make the title of the slide (note that this is *not* the title of
        the talk)
        """
        if self.title is None or self.title == "":
            return Area(width=0, height=0)

        # if we just get a string, put it in a TeX object in the current style
        if isinstance(self.title, types.StringType):
            ttext = "%s %s" % (talk.title_textstyle, self.title)
            return TeX(ttext, fg=talk.title_fg).scale(talk.title_scale*0.8,
                                                      talk.title_scale)
        else:
            # just return the object itself
            return self.title
    
    def add_heading(self, level, text):
        """
        Add a heading to the slide

        @param level: the heading level as a number starting from 1 (the most
        significant level)
        @type level: int (1,2,3) or string ("space", "equation")

        @param text: the text to be used for the heading
        @type text: string
        """
        temp = [ level, text ]
        self.headings.append(temp)

    def add_text(self, text, **options):
        """
        Add, and arbitrarily place, text on the slide

        @param text: the text to place
        @type text: string, TeX object or Text object
        """
        # process options
        if options.has_key('fg'):
            frontColor = options['fg']
        else:
            frontColor = self.text_fg

        if options.has_key('scale'):
            scale = options['scale']
        else:
            scale = self.text_scale

        # check for what kind of object we have...
        if isinstance(text, types.StringType):
            # prepend the style if it is just a string
            text = self.text_textstyle + " " + text
            obj = TeX(text, fg=frontColor).scale(scale, scale)
        else:
            raise ValueError, \
                    "Cannot yet handle non-string objects in Slide.add_text()"

        # there must be a better way to do this!!!
        if options.has_key('e'):
            obj.e = options['e']
        elif options.has_key('se'):
            obj.se = options['se']
        elif options.has_key('s'):
            obj.s = options['s']
        elif options.has_key('sw'):
            obj.sw = options['sw']
        elif options.has_key('w'):
            obj.w = options['w']
        elif options.has_key('nw'):
            obj.nw = options['nw']
        elif options.has_key('n'): 
            obj.n = options['n']
        elif options.has_key('ne'):
            obj.ne = options['ne']
        elif options.has_key('c'):
            obj.c = options['c']
        else:
            obj.sw = P(0.0, 0.0)

        #obj = TeX(r"test", fg=frontColor)
        #obj.c = self.area.c

        self.textObjs.append(obj)

    def _make_headings(self, talk):
        """
        Make the headings
        """
        heading_block = Align(a1="sw", a2="nw", angle=180, space=0.5)
        for heading in self.headings:
            heading_level = heading[0]
            if not talk.headings_bullets.has_key(heading_level):
                heading_level = "default"
            heading_text = "%s %s" % (talk.headings_textstyle[heading_level]
                                                            , heading[1])
            heading_bullet = talk.headings_bullets[heading_level]
            heading_fg = talk.headings_fgs[heading_level]
            heading_scale = talk.headings_scales[heading_level]
            heading_indent = talk.headings_indent[heading_level]

            tex = Align(a1='ne', a2='nw', angle=90, space=0.2)
            tex.append(heading_bullet)
            tex.append(TeXBox(text=heading_text,
                            fixed_width=self.area.width-5,
                            fg=heading_fg,
                            tex_scale=heading_scale))
 
            padding = Area(sw=tex.sw, width=heading_indent, height=0)
            heading_proper = Align(a1="e", a2="w", angle=90, space=0)
            heading_proper.append(padding, tex)
            heading_block.append(heading_proper)

        return heading_block
            
    def _make_waitbar(self, talk):
        """
        Make a waitbar
        """
        waitBarBack = Rectangle(se=self.area.se+P(-0.8, 0.4),
                        width=2.5,
                        height=0.5,
                        r=0.2,
                        fg=talk.waitbar_bg,
                        bg=talk.waitbar_bg)

        offset = 0.05
        waitBarFront = Rectangle(w=waitBarBack.w+P(offset, 0),
                        width=(waitBarBack.width-2*offset)*\
                                self.pageNumber/self.pages,
                        height=waitBarBack.height-2*offset,
                        r=0.2,
                        fg=talk.waitbar_fg,
                        bg=talk.waitbar_fg)
        waitBar = Group(waitBarBack, waitBarFront)
        return  waitBar

    def _make_footer(self, talk):
        """
        Make the footer.  A text block giving the title and the name of the
        person giving the talk
        """
        pageOf = False
        if pageOf:
            footerText = " - %s; page %i of %i" %  \
                        (talk.speaker, self.pageNumber, self.pages)
        else:
            footerText = " - %s" % (talk.speaker, )
        
        footer = Align(a1="e", a2="w", angle=90, space=0.1)
        footer.append(TeX(text="%s %s"%(talk.title_textstyle, talk.title),
                        fg=talk.title_fg,
                        ).scale(talk.footerScale, talk.footerScale))
        footer.append(TeX(text="%s %s"%(talk.speaker_textstyle, footerText),
                        fg=talk.title_fg
                        ).scale(talk.footerScale, talk.footerScale))
        footer.sw = self.area.sw+P(0.4, 0.4)
        return footer

    def add_epsf(self, file="", **options):
        """
        Add an eps file to the slide

        @param file: the filename of the eps file
        @type file: string
        
        @keyword width: the width of the image in the current default units.  
        If only this variable is given, then the aspect ratio of the image is
        maintained.
        @type width: float

        @keyword height: the height of the image in the current default
        units.  If only this variable is given, then the aspect ratio of 
        the image is maintainted.
        @type height: float

        @keyword c, n, ne, e, se, s, sw, w, nw: the location of the anchor point
        """
        if options.has_key('width'):
            picture = Epsf(file, width=options['width'])
        elif options.has_key('height'):
            picture = Epsf(file, height=options['height'])
        elif options.has_key('width') and options.has_key('height'):
            picture = Epsf(file, width=options['width'], 
                    height=options['height'])
        else:
            picture = Epsf(file)

        # there must be a better way to do this!!!
        if options.has_key('e'):
            picture.e = options['e']
        elif options.has_key('se'):
            picture.se = options['se']
        elif options.has_key('s'):
            picture.s = options['s']
        elif options.has_key('sw'):
            picture.sw = options['sw']
        elif options.has_key('w'):
            picture.w = options['w']
        elif options.has_key('nw'):
            picture.nw = options['nw']
        elif options.has_key('n'):
            picture.n = options['n']
        elif options.has_key('ne'):
            picture.ne = options['ne']
        elif options.has_key('c'):
            picture.c = options['c']
        else:
            picture.sw = P(0.0, 0.0)

        offset = 0.2
        background = Rectangle(width=picture.bbox().width+offset,
                                height=picture.bbox().height+offset,
                                bg=Color('white'),
                                fg=Color('white'),
                                )
        background.sw = picture.sw-P(offset/2.0, offset/2.0)
        figure = Group(background, picture)
        self.epsf.append(figure)

    def _make_epsf(self):
        """
        Collects all of the eps images together
        """
        pictures = Group()
        for file in self.epsf:
            pictures.append(file)
        return pictures

    def _make_figs(self):
        """
        Collects all of the figures together
        """
        figs = Group()
        for fig in self.figs:
            figs.append(fig)
        return figs

    def _make_textObjs(self):
        """
        Collects all the text objects together
        """
        textObjs = Group()
        for text in self.textObjs:
            textObjs.append(text)
        return textObjs

    def _make_titlepage(self, talk):
        """
        Makes the titlepage of the talk
        """
        titlepage = Align(a1="s", a2="n", angle=180, space=0.4)

        if isinstance(talk.title, types.StringType):
            ttext = "%s %s" % (talk.title_textstyle, talk.title)
            titlepage.append(TeX(ttext,
                                fg=talk.title_fg)\
                                .scale(talk.title_scale, talk.title_scale))
        else:
            #raise ValueError, "Can't yet handle non-string arguments")
            titlepage.append(Text(ttext))

        if talk.authors is not None:
            titlepage.append(talk._make_authors())
        if talk.address is not None:
            titlepage.append(talk._make_address())

        return titlepage

    def _make_background(self, talk):
        """
        Makes the background of the slide
        """
        back = Group()
        back.append(Rectangle(sw=self.area.sw,
                        width=self.area.width,
                        height=self.area.height,
                        fg=None,
                        bg=talk.bg,
                        )
                    )
        back.append(Rectangle(sw=self.area.sw,
                        width=2.5,
                        height=self.area.height,
                        fg=None,
                        bg=talk.bg*0.5,
                        )
                    )
        back.append(Rectangle(sw=self.area.sw,
                        width=self.area.width,
                        height=1.5,
                        fg=None,
                        bg=talk.bg*0.5,
                        )
                    )
        back.append(Rectangle(nw=self.area.nw,
                        width=self.area.width,
                        height=2.5,
                        fg=None,
                        bg=talk.bg*0.5,
                        )
                    )
        back.append(Rectangle(nw=self.area.nw,
                        width=2.5,
                        height=2.5,
                        fg=None,
                        bg=Color('firebrick'),
                        )
                    )

        return back
        
    def _make(self, talk, scale=1):
        """
        Make the slide.  Collect all of the objects together into one Page()
        object ready for rendering.
        """
        
        if self.titlepage:
            all = self._make_titlepage(talk)
            all.c = self.area.c + P(0.0, 0.8)
        else:
            all = Align(a1="s", a2="n", angle=180, space=0.4)
            all.append(self._make_title(talk))
            all.nw = self.area.nw + P(2.5, -0.2)

        # I'm aware that this isn't a good way to do this, but
        # it's late at night, and I want to get *something* going

        headings = self._make_headings(talk)
        headings.nw = self.area.nw + P(3.0, -3.0)
    
        back = self._make_background(talk)

        p = self.area.se + P(-0.1, 0.1)
        signature = Text(
                'Created with PyScript.  http://pyscript.sourceforge.net', 
                size=15, 
                sw=p, 
                fg=talk.bg*0.8
                ).rotate(-90, p)

        logos = self._make_logos()
        logos.nw = self.area.nw + P(0.2, -0.2)

        self.pages = len(talk.slides)

        All = Group(
                back,
                all,
                headings,
                self._make_epsf(),
                self._make_figs(),
                self._make_textObjs(),
                signature,
                self._make_footer(talk),
                logos,
                self._make_waitbar(talk)
                ).scale(scale, scale)

        return Page(All, orientation=self.orientation)

# vim: expandtab shiftwidth=4:
