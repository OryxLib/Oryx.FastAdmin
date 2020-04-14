# poster style for PyScript, used for the CCP2004 conference

HOME = os.path.expandvars("$HOME")
stylesDir = HOME + "/.pyscript/styles/"

# set the default style settings
self.title = ""
self.title_fg = Color("yellow")*2.0
self.title_scale = 1.4
self.title_width = 0.8  # as a fraction of the total poster width
self.title_text_style = "\large \sf"

self.authors = ""
self.authors_fg = Color("yellow")
self.authors_scale = 1
self.authors_width = 0.8  # as a fraction of the total poster width
self.authors_text_style = "\sf"

self.address = ""
self.address_fg = Color("yellow")
self.address_scale = 0.9
self.address_width = 0.8  # as a fraction of the total poster width
self.address_text_style = "\sf"

self.abstract = ""
self.abstract_fg = Color("gold")*1.1
self.abstract_scale = 0.8
self.abstract_width = 0.92  # relative to total width of poster
self.abstract_text_style = ""

self.gutter = 0.2
self.pad = 0  # should get set by add_column()
self.item_sep = 0.3

self.bg = Color("royalblue")*0.8

self.signature_fg = Color("white")

self.logo_height = 1.2

# styles for columns
self.column_item_sep = 0.3

# styles for column boxes
# the title's style...
self.column_box_title_align = "c"
self.column_box_title_tex_scale = 1.4
self.column_box_title_fixed_width = 9.4
self.column_box_title_text_style = r"\sf"
self.column_box_title_fg = Color("orangered")*0.95

# the text styles of the column box
self.column_box_text_align = "w"
self.column_box_tex_scale = 0.7
self.column_box_text_width = 9.4
self.column_box_text_style = r""
self.column_box_text_fg = Color(0)

# the column box styles
self.column_box_item_sep = 0.1
self.column_box_width = 9.9
self.column_box_bg = Color("LightGoldenRod")*1.1
self.column_box_border = 1


