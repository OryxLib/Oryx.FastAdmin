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

# $Id: afm.py,v 1.14 2006/04/24 14:20:20 paultcochrane Exp $

"""
Module for reading and writing AFM files.
"""

__revision__ = '$Revision: 1.14 $'

# It does not implement the full spec (Adobe Technote 5004, Adobe Font Metrics
# File Format Specification). Still, it should read most "common" AFM files.
# Taken and adapted from afmLib.py in fonttools by Just van Rossum

import re, os, types, cPickle, sys
from pyscript.base import FontError
import pyscript

FONTDIR = os.path.join(pyscript.__path__[0], "fonts")

# every single line starts with a "word"
identifierRE = re.compile("^([A-Za-z]+).*")

# regular expression to parse char lines
charRE = re.compile(
    "(-?\d+)"          # charnum
    "\s*;\s*WX\s+"     # ; WX 
    "(\d+)"            # width
    "\s*;\s*N\s+"      # ; N 
    "([.A-Za-z0-9_]+)" # charname
    "\s*;\s*B\s+"      # ; B 
    "(-?\d+)"          # left
    "\s+"              # 
    "(-?\d+)"          # bottom
    "\s+"              # 
    "(-?\d+)"          # right
    "\s+"              # 
    "(-?\d+)"          # top
    "\s*;\s*"          # ; 
    )

# regular expression to parse kerning lines
kernRE = re.compile(
    "([.A-Za-z0-9_]+)" # leftchar
    "\s+"              # 
    "([.A-Za-z0-9_]+)" # rightchar
    "\s+"              # 
    "(-?\d+)"          # value
    "\s*"              # 
    )

# regular expressions to parse composite info lines of the form:
# Aacute 2 ; PCC A 0 0 ; PCC acute 182 211 ;
compositeRE = re.compile(
    "([.A-Za-z0-9_]+)" # char name
    "\s+"              # 
    "(\d+)"            # number of parts
    "\s*;\s*"          # 
    )
componentRE = re.compile(
    "PCC\s+"           # PPC
    "([.A-Za-z0-9_]+)" # base char name
    "\s+"              # 
    "(-?\d+)"          # x offset
    "\s+"              # 
    "(-?\d+)"          # y offset
    "\s*;\s*"          # 
    )


class AFMError(Exception): 
    """
    Class for handling errors
    """
    pass

class ConvertAFM:
    """
    Convert Adobe Font Metrics
    """

    _keywords = ['StartFontMetrics',
                 'EndFontMetrics',
                 'StartCharMetrics',
                 'EndCharMetrics',
                 'StartKernData',
                 'StartKernPairs',
                 'EndKernPairs',
                 'EndKernData',
                 'StartComposites',
                 'EndComposites',
                 ]
        
    def __init__(self, filename):
        """
        Initialisation of object

        @param filename: the name of the font file name
        @type filename: string
        """

        self._attrs = {}
        self._chars = {}
        self._kerning = {}
        self._comments = []
        self._composites = {}

        self.parse(filename)

    def parse(self, path):
        """
        Parse the afm file

        @param path: path to the afm file
        @type path: string
        """

        f = open(path, 'rb')
        data = f.read()
        f.close()
        # read any text file, regardless whether it's
        # formatted for Mac, Unix or Dos
        sep = ""
        if '\r' in data: 
            sep = sep + '\r'    # mac or dos
        if '\n' in data: 
            sep = sep + '\n'    # unix or dos
        #lines = string.split(data, sep)
        lines = data.split(sep)

        for line in lines:
            if not line.strip():
                continue
            m = identifierRE.match(line)
            if m is None:
                raise AFMError, "syntax error in AFM file: " + `line`

            pos = m.regs[1][1]
            word = line[:pos]
            rest = line[pos:].strip()
            if word in self._keywords:
                continue
            if word == "C":
                self.parsechar(rest)
            elif word == "KPX":
                self.parsekernpair(rest)
            elif word == "CC":
                self.parsecomposite(rest)
            else:
                self.parseattr(word, rest)

    def write(self, filename):
        """
        Write the font file

        @param filename: the name of the font file to write
        @type filename: string
        """

        out = open(filename, "w")

        # the new ugly way, that produces nice files

        # docstring
        out.write('"""\n')
        out.write("PyScript font module for %s\n" %
                self._attrs['FullName'])
        out.write('"""\n')

        # this is the revision attribute useful for python
        # with extra code due to cvs keyword expansion issues
        out.write("__revision__ = '$")
        out.write("Revision$'\n")


        
        # write out the attributes
        out.write("attrs = {\n")
        for key in self._attrs.keys():
            value = self._attrs[key]
            if str(value.__class__) == "<type 'str'>":
                out.write("'%s' : '%s',\n" % \
                        (key, value))
            else:
                out.write("'%s' : %s,\n" % \
                        (key, value))
        out.write("}\n")

        # write out the characters
        out.write("chars = {\n")
        for key in self._chars.keys():
            value = self._chars[key]
            out.write("%s : %s,\n" % \
                    (key, value))
        out.write("}\n");

        # write out the kerning
        out.write("kerning = {\n")
        for key in self._kerning.keys():
            value = self._kerning[key]
            out.write("%s : %s,\n" % \
                    (key, value))
        out.write("}\n")

        # write out the comments
        out.write("comments = [\n")
        for elem in self._comments:
            out.write("'%s',\n" % elem)
        out.write("]\n")

        # write out the composites
        out.write("composites = {\n")
        for key in self._composites.keys():
            value = self._composites[key]
            out.write("%s : %s,\n" % \
                    (key, value))
        out.write("}\n")

        # the old, simple way, that produces ugly files
        # out.write("attrs = %s"%repr(self._attrs))
        # out.write("\n")
        # out.write("chars = %s"%repr(self._chars))
        # out.write("\n")
        # out.write("kerning = %s"%repr(self._kerning))
        # out.write("\n")
        # out.write("comments = %s"%repr(self._comments))
        # out.write("\n")
        # out.write("composites = %s"%repr(self._composites))
        # out.write("\n")

        out.close()

    def write2(self, filename):
        """
        Another version of writing the font file, uses the CPickle module

        @param filename: the name of the font file to write
        @type filename: string
        """

        afm = AFM()

        afm._attrs = self._attrs
        afm._chars = self._chars
        afm._kerning = self._kerning
        afm._comments = self._comments
        afm._composites = self._composites

        fp = open(filename, "w")
        cPickle.dump(afm, fp)
        fp.close()
        
    def parsechar(self, rest):
        """
        Parse a character

        @param rest: the character to parse
        @type rest: string
        """
        m = charRE.match(rest)
        if m is None:
            raise AFMError, "syntax error in AFM file: " + `rest`
        things = []
        for fr, to in m.regs[1:]:
            things.append(rest[fr:to])
        #charname = things[2]
        del things[2]
        #charnum, width, l, b, r, t = map(string.atoi, things)
        charnum, width, l, b, r, t = map(int, things)
        # width l b r t
        self._chars[charnum] = width, l, b, r, t

    def parsekernpair(self, rest):
        """
        Parse a kerning pair

        @param rest: the kerning pair to parse
        @type rest: string
        """
        m = kernRE.match(rest)
        if m is None:
            raise AFMError, "syntax error in AFM file: " + `rest`
        things = []
        for fr, to in m.regs[1:]:
            things.append(rest[fr:to])
        leftchar, rightchar, value = things

        value = int(value)
        #self._kerning[(leftchar, rightchar)] = value
        # fix for all kernings
        if len(leftchar) == len(rightchar) == 1:
            self._kerning[(ord(leftchar), ord(rightchar))]=value
        
    def parseattr(self, word, rest):
        """
        Parse an attribute

        @param word: the kind of attribute to be parsed (?)
        @type word: string

        @param rest: the attribute to parse
        @type rest: string
        """
        if word == "FontBBox":
            #l, b, r, t = map(string.atoi, string.split(rest))
            l, b, r, t = map(int, rest.split())
            self._attrs[word] = l, b, r, t
        elif word == "Comment":
            self._comments.append(rest)
        else:
            try:
                value = int(rest)
            except (ValueError, OverflowError):
                self._attrs[word] = rest
            else:
                self._attrs[word] = value
        
    def parsecomposite(self, rest):
        """
        Parse a composite string/expression/thing

        @param rest: the string to parse
        @type rest: string
        """
        m = compositeRE.match(rest)
        if m is None:
            raise AFMError, "syntax error in AFM file: " + `rest`
        charname = m.group(1)
        ncomponents = int(m.group(2))
        rest = rest[m.regs[0][1]:]
        components = []
        while 1:
            m = componentRE.match(rest)
            if m is None:
                raise AFMError, "syntax error in AFM file: " + `rest`
            basechar = m.group(1)
            xoffset = int(m.group(2))
            yoffset = int(m.group(3))
            components.append((basechar, xoffset, yoffset))
            rest = rest[m.regs[0][1]:]
            if not rest:
                break
        assert len(components) == ncomponents
        self._composites[charname] = components
        
# -------------------------------------------------------------------
        
class AFM:
    """
    Class for handling Adobe Font Metric objects
    """
        
    def __init__(self, fontname):
        """
        Initialisation of the AFM object
        
        @param fontname: the name of the font
        @type fontname: string
        """

        # this should be a better name, but will stop the possible error
        # from occurring
        self.FullName = fontname

        fontname = fontname.lower()
        fontname = fontname.replace("-", "_")

        # the import statement seem a little bit of a hack
        # but this will work for now.
        try:
            f = __import__('pyscript.fonts.%s'%fontname, None, None, [fontname])
        except ImportError, x:
            # rename the exception
            raise FontError, x
        self.f = f
        
    def has_kernpair(self, pair):
        """
        Determines if the kerning pair exists in the font

        @param pair: the kering pair to look for
        @type pair: ?
        """
        return self.f.kerning.has_key(pair)
        
    def kernpairs(self):
        """
        Returns the kerning pairs in the font
        """
        return self.f.kerning.keys()
        
    def has_char(self, char):
        """
        Determins if the character exists in the font

        @param char: the character to look for
        @type char: ?
        """
        return self.f.chars.has_key(char)
        
    def chars(self):
        """
        Returns the characters in the font
        """
        return self.f.chars.keys()
        
    def comments(self):
        """
        Returns the comments in the font
        """
        return self.f.comments
        
    def __getattr__(self, attr):
        """
        Gets an attribute of the font

        @param attr: the attribute to get
        @type attr: ?
        """
        if self.f.attrs.has_key(attr):
            return self.f.attrs[attr]
        else:
            raise AttributeError, attr
                
    def __getitem__(self, key):
        """
        Gets an item within the font

        @param key: the item to get
        @type key: ?
        """
        if type(key) == types.TupleType:
            # key is a tuple, return the kernpair
            return self.f.kerning.get(key, 0)
        else:
            # return the metrics instead
            return self.f.chars[key]
        
    def __repr__(self):
        """
        Returns the representation of the font object
        """
        if hasattr(self, "FullName"):
            return '<AFM object for %s>' % self.FullName
        else:
            return '<AFM object at %x>' % id(self)

    def bbox(self, string, size=1, kerning=0):
        '''
        Return a strings boundingbox in this font
        at the scale provided (relative to 1 point?)

        @param string: the string to measure
        @type string: string

        @param size: the point size of the font (sort of)
        @type size: int

        @param kerning: whether to subtract off the kerning
        @type kerning: boolean

        @return: xl, yb, xr, yt
        '''
        chars = map(ord, list(string))

        # order: width l b r t

        # use 'reduce' and 'map' as they're written in C

        # add up all the widths
        width = reduce(lambda x, y : x+self[y][0], chars, 0)

        # subtract the kerning
        if kerning == 1:
            if len(chars)>1:
                kk = map(lambda x, y:self[(x, y)], chars[:-1], chars[1:])
                kern = reduce(lambda x, y:x+y, kk)
                            
                width += kern
        kk = map(lambda x, y:self[(x, y)], chars[:-1], chars[1:])
        print kk

        # get rid of the end bits
        start = self[chars[0]][1]
        f = self[chars[-1]]
        width = width-start-(f[0]-f[3])

        # accumulate maximum height
        top = reduce(lambda x, y: max(x, self[y][4]), chars, 0)

        # accumulate lowest point
        bottom = reduce(lambda x, y: min(x, self[y][2]), 
                                     chars, self[chars[0]][2])

        sc = size/1000.
        xl = start*sc
        yb = bottom*sc
        xr = xl + width*sc
        yt = top*sc

        return xl, yb, xr, yt

def load(fontname):
    """
    Loads the font of the given font name

    @param fontname: the name of the font to load
    @type fontname: string
    """

    fontpath = os.path.join(FONTDIR, fontname)

    fp = open(fontpath+".font")
    font = cPickle.load(fp)
    fp.close()

    return font

if __name__ == "__main__":
    # utility for converting afm files to pyscripts
    # font modules

    for afmFname in sys.argv[1:]:

        afmObj = ConvertAFM(afmFname)

        dirname, fname = os.path.split(afmFname)

        base, ext = os.path.splitext(fname)

        base = base.lower()
        base = base.replace("-", "_")
        
        outfile = os.path.join(dirname, base+".py")

        afmObj.write(outfile)

# vim: expandtab shiftwidth=4:
