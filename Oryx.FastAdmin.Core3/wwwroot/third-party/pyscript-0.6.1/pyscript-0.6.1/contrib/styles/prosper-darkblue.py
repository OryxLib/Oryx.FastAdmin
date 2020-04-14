# talk style for PyScript, following the Darkblue design of prosper

HOME = os.path.expandvars("$HOME")
stylesDir = HOME + "/.pyscript/styles/"

# set the foreground and background colour of the title text of the talk
self.title_fg = Color('white')
self.title_bg = Color('white')

# set the talk title's text style
self.title_textstyle = r"\bf\sf"

# set the text style for the text of who is giving the talk
self.speaker_textstyle = r"\sf"

# set the colour and text style of the address of the speaker of the talk
self.address_fg = Color('white')
self.address_textstyle = r"\sf"

# set the colour and text style of the authors of the talk (not necessarily
# the speaker of the talk)
self.authors_fg = Color('white')
self.authors_textstyle = r"\sf"

# set the colour and text style of the title of the *slide*
self.slide_title_fg = Color('lightgray')
self.slide_title_textstyle = r"\bf"

# set the colour, scale, textstyle, bullet and indent type for a level 1 heading
self.headings_fgs[1] = Color('white')
self.headings_scales[1] = 3
self.headings_textstyle[1] = r"\sf"
self.headings_bullets[1] = Epsf(file=stylesDir+"redbullet.eps").scale(0.2,0.2)
self.headings_indent[1] = 0

# set the colour, scale, textstyle, bullet and indent type for a level 2 heading
self.headings_fgs[2] = Color('white')
self.headings_scales[2] = 2.5
self.headings_textstyle[2] = r"\sf"
self.headings_bullets[2] = Epsf(file=stylesDir+"greenbullet.eps").scale(0.15,0.15)
self.headings_indent[2] = 0.5

# set the colour, scale, textstyle, bullet and indent type for a level 3 heading
self.headings_fgs[3] = Color('white')
self.headings_scales[3] = 2.2
self.headings_textstyle[3] = r"\sf"
self.headings_bullets[3] = Epsf(file=stylesDir+"yellowbullet.eps").scale(0.1,0.1)
self.headings_indent[3] = 1 

# set the colour, textstyle and scale for placed text
self.text_scale = 3.0
self.text_fg = Color('white')
self.text_textstyle = r"\sf"
