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

# $Id: base.py,v 1.30 2006/06/06 12:39:48 paultcochrane Exp $

"""
Base objects
"""

__revision__ = '$Revision: 1.30 $'

import copy
import types

# -------------------------------------------------------------------------
class PyScriptError(Exception):
    """
    Handles a PyScript error
    """
    pass

class FontError(Exception):
    """
    Handles a font error
    """
    pass

# -------------------------------------------------------------------------
UNITS = {
    "inch":72,
    "points":1,
    "cm":28.346,
    "mm":2.8346
    }

# -------------------------------------------------------------------------

class PsObj(object):
    """
    Base Class that most pyscript objects should subclass
    """

    def __init__(self, **options):
        '''
        can pass a dict of atributes to set
        '''
        object.__init__(self)
        self(**options)

    def __call__(self, **options):
        '''
        Set a whole lot of attributes in one go
        
        eg::
          obj.set(bg=Color(.3),linewidth=2)

        @return: self 
        @rtype: self
        '''

        # first do non-property ones
        # this will raise an exception if class doesn't have attribute
        # I think this is good.
        prop = []
        for key, value in options.items():
            if isinstance(eval('self.__class__.%s'%key), property):
                prop.append((key, value))
            else:
                self.__class__.__setattr__(self, key, value)

        # now the property ones
        # (which are functions of the non-property ones)
        for key, value in prop:
            self.__class__.__setattr__(self, key, value)
                
        # for convenience return a reference to us
        return self
        
    def copy(self, **options):
        '''
        return a copy of this object
        with listed attributes modified

        eg::
          newobj=obj.copy(bg=Color(.3))
        @rtype: self
        '''
        # here for convenience
        obj = copy.deepcopy(self)

        obj(**options)

        return obj
    
    def __repr__(self):
        '''
        Return a representation of this object
        @rtype: string
        '''
        return str(self.__class__)

    def __str__(self):
        '''
        return actual postscript string to generate object
        @rtype: string
        '''
        return self.prebody()+self.body()+self.postbody()

    def prebody(self):
        '''
        convenience function to allow clean subclassing
        @rtype: string
        '''
        return ''

    def body(self):
        '''
        subclasses should overide this for generating postscipt code
        @rtype: string
        '''
        return ''

    def postbody(self):
        '''
        convenience function to allow clean subclassing
        @rtype: string
        '''
        return ''


    def bbox(self):
        """
        return objects bounding box
        this can be a Null Bbox() if object doesn't
        draw anything on the page.)

        NB that the bbox should be dynamically calculated and take
        into account the transformation matrix if it applies
        @return: A bounding box object
        @rtype: Bbox
        """

        return ''

# -------------------------------------------------------------------------

class Color(PsObj):
    """
    Class to encode a postscript color
 
    There are five ways to specify the color:

     - Color(C,M,Y,K) =CMYKColor
     - Color(R,G,B) =RGBColor
     - Color(G) = Gray
     - Color('yellow') etc, see L{COLORS}
     - Color('#FF0000') Hex string, must start with '#'
    """

    COLORS = {
        "aliceblue":(240, 248, 255),
        "antiquewhite":(250, 235, 215),
        "aqua":(0, 255, 255),
        "aquamarine":(127, 255, 212),
        "azure":(240, 255, 255),
        "beige":(245, 245, 220),
        "bisque":(255, 228, 196),
        "black":(0, 0, 0),
        "blanchedalmond":(255, 235, 205),
        "blue":(0, 0, 255),
        "blueviolet":(138, 43, 226),
        "brown":(165, 42, 42),
        "burlywood":(222, 184, 135),
        "cadetblue":(95, 158, 160),
        "chartreuse":(127, 255, 0),
        "chocolate":(210, 105, 30),
        "coral":(255, 127, 80),
        "cornflowerblue":(100, 149, 237),
        "cornsilk":(255, 248, 220),
        "crimson":(220, 20, 60),
        "cyan":(0, 255, 255),
        "darkblue":(0, 0, 139),
        "darkcyan":(0, 139, 139),
        "darkgoldenrod":(184, 134, 11),
        "darkgray":(169, 169, 169),
        "darkgrey":(169, 169, 169),
        "darkgreen":(0, 100, 0),
        "darkkhaki":(189, 183, 107),
        "darkmagenta":(139, 0, 139),
        "darkolivegreen":(85, 107, 47),
        "darkorange":(255, 140, 0),
        "darkorchid":(153, 50, 204),
        "darkred":(139, 0, 0),
        "darksalmon":(233, 150, 122),
        "darkseagreen":(143, 188, 143),
        "darkslateblue":(72, 61, 139),
        "darkslategray":(47, 79, 79),
        "darkslategrey":(47, 79, 79),
        "darkturquoise":(0, 206, 209),
        "darkviolet":(148, 0, 211),
        "deeppink":(255, 20, 147),
        "deepskyblue":(0, 191, 255),
        "dimgray":(105, 105, 105),
        "dimgrey":(105, 105, 105),
        "dodgerblue":(30, 144, 255),
        "firebrick":(178, 34, 34),
        "floralwhite":(255, 250, 240),
        "forestgreen":(34, 139, 34),
        "fuchsia":(255, 0, 255),
        "gainsboro":(220, 220, 220),
        "ghostwhite":(248, 248, 255),
        "gold":(255, 215, 0),
        "goldenrod":(218, 165, 32),
        "gray":(128, 128, 128),
        "grey":(128, 128, 128),
        "green":(0, 128, 0),
        "greenyellow":(173, 255, 47),
        "honeydew":(240, 255, 240),
        "hotpink":(255, 105, 180),
        "indianred":(205, 92, 92),
        "indigo":(75, 0, 130),
        "ivory":(255, 255, 240),
        "khaki":(240, 230, 140),
        "lavender":(230, 230, 250),
        "lavenderblush":(255, 240, 245),
        "lawngreen":(124, 252, 0),
        "lemonchiffon":(255, 250, 205),
        "lightblue":(173, 216, 230),
        "lightcoral":(240, 128, 128),
        "lightcyan":(224, 255, 255),
        "lightgoldenrod":(250, 250, 210),
        "lightgreen":(144, 238, 144),
        "lightgray":(211, 211, 211),
        "lightgrey":(211, 211, 211),
        "lightpink":(255, 182, 193),
        "lightsalmon":(255, 160, 122),
        "lightseagreen":(32, 178, 170),
        "lightskyblue":(135, 206, 250),
        "lightslategray":(119, 136, 153),
        "lightslategrey":(119, 136, 153),
        "lightsteelblue":(176, 196, 222),
        "lightyellow":(255, 255, 224),
        "lime":(0, 255, 0),
        "limegreen":(50, 205, 50),
        "linen":(250, 240, 230),
        "magenta":(255, 0, 255),
        "maroon":(128, 0, 0),
        "mediumaquamarine":(102, 205, 170),
        "mediumblue":(0, 0, 205),
        "mediumorchid":(186, 85, 211),
        "mediumpurple":(147, 112, 219),
        "mediumseagreen":(60, 179, 113),
        "mediumslateblue":(123, 104, 238),
        "mediumspringgreen":(0, 250, 154),
        "mediumturquoise":(72, 209, 204),
        "mediumvioletred":(199, 21, 133),
        "midnightblue":(25, 25, 112),
        "mintcream":(245, 255, 250),
        "mistyrose":(255, 228, 225),
        "moccasin":(255, 228, 181),
        "navajowhite":(255, 222, 173),
        "navy":(0, 0, 128),
        "oldlace":(253, 245, 230),
        "olive":(128, 128, 0),
        "olivedrab":(107, 142, 35),
        "orange":(255, 165, 0),
        "orangered":(255, 69, 0),
        "orchid":(218, 112, 214),
        "palegoldenrod":(238, 232, 170),
        "palegreen":(152, 251, 152),
        "paleturquoise":(175, 238, 238),
        "palevioletred":(219, 112, 147),
        "papayawhip":(255, 239, 213),
        "peachpuff":(255, 218, 185),
        "peru":(205, 133, 63),
        "pink":(255, 192, 203),
        "plum":(221, 160, 221),
        "powderblue":(176, 224, 230),
        "purple":(128, 0, 128),
        "red":(255, 0, 0),
        "rosybrown":(188, 143, 143),
        "royalblue":(65, 105, 225),
        "saddlebrown":(139, 69, 19),
        "salmon":(250, 128, 114),
        "sandybrown":(244, 164, 96),
        "seagreen":(46, 139, 87),
        "seashell":(255, 245, 238),
        "sienna":(160, 82, 45),
        "silver":(192, 192, 192),
        "skyblue":(135, 206, 235),
        "slateblue":(106, 90, 205),
        "slategray":(112, 128, 144),
        "slategrey":(112, 128, 144),
        "snow":(255, 250, 250),
        "springgreen":(0, 255, 127),
        "steelblue":(70, 130, 180),
        "tan":(210, 180, 140),
        "teal":(0, 128, 128),
        "thistle":(216, 191, 216),
        "tomato":(255, 99, 71),
        "turquoise":(64, 224, 208),
        "violet":(238, 130, 238),
        "wheat":(245, 222, 179),
        "white":(255, 255, 255),
        "whitesmoke":(245, 245, 245),
        "yellow":(255, 255, 0),
        "yellowgreen":(154, 205, 50),
        }


    def __init__(self, *col, **options):
        """
        Initialisation of the colour object

        @param col:
        @type col:

        @param options:
        @type options:
        """

        # some sanity checks
        if type(col[0]) == types.StringType:
            
            if col[0][0] == "#" and len(col[0]) == 7:
                # hex color scheme  eg #A0FF00
                col = (int(col[0][1:3], 16) / 255.0,
                     int(col[0][3:5], 16) / 255.0,
                     int(col[0][5:7], 16) / 255.0)
            else:
                # named color
                col = col[0].lower()
                col = self.COLORS[col]
                # renormalise so that values are in [0,1]
                col = (col[0]/255., col[1]/255., col[2]/255.)
            
        assert len(col) > 0 and len(col) < 5
        for ii in col: 
            assert ii >= 0 and ii <= 1
        
        self.color = col
            
        PsObj.__init__(self, **options)
        
        
    def body(self):
        """
        Returns the body of the postscript for the Color object
        """

        color = self.color
        if len(color) == 1:
            # grayscale color
            ps = " %g setgray " % color
        elif len(color) == 3:
            # rgb color
            ps = " %g %g %g setrgbcolor " % color
        elif len(color) == 4:
            # cmyk color
            ps = " %g %g %g %g setcmykcolor " % color
        else:
            raise ValueError, "Unknown color"
        return ps
    
    def __mul__(self, other):
        '''
        colors can be multiplied by a numeric factor.
        factors less than 1 will darken the colors,
        factors grater than will will lighten the colors.
        (this depends on how the colors where specified)

        eg::
          Color(.2,.6,.6)*.5 = Color(.1,.3,.3)

        '''
        assert other >= 0 #and other<=1
        newcol = []
        for ii in self.color:
            newcol.append(min(1, ii*other))

        # XXX this breaks things:
        #return Color(tuple(newcol))
        return apply(Color, tuple(newcol))
# -------------------------------------------------------------------------
class Dash(PsObj):
    """
    Class to encode postscript dash pattern

    Argument is a list of lengths for alternating dash and spaces

    eg:
    Dash(3)            3 on, 3 off, ...
    Dash(2,offset=1)   1 on, 2 off, 2 on, 2 off, ...
    Dash(2,1)          2 on, 1 off, 2 on, 1 off, ...
    Dash(3,5,offset=6) 2 off, 3 on, 5 off, 3 on, 5 off, ...
    Dash(3,1,1,1)      3 on, 1 off, 1 on, 1 off, 3 on, ...

    @cvar offset: initially fastforward this much into the pattern

    """

    pattern = (2, )
    offset = 0

    def __init__(self, *args, **options):
        """
        Initialisation of the Dash object
        """

        if len(args) > 0:
            self.pattern = args
     
        PsObj.__init__(self, **options)
       
       
    def body(self):
        """
        Returns the postscript of a Dash object
        """

        # if we don't start with a space horrible things happen
    
        pat = ""
        delim = ' '
        pat = "[ "+ delim.join(["%g" % l for l in self.pattern])    

        pat = pat + "] %g setdash " % self.offset

        return pat


# vim: expandtab shiftwidth=4:
