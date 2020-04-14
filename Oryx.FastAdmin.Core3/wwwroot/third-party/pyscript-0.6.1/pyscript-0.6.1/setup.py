#!/usr/bin/env python

from distutils.core import setup

a=setup(name="pyscript",
      version="0.6.1",
      description="Postscript Graphics with Python",
      author="Alexei Gilchrist and Paul Cochrane",
      author_email="aalexei@users.sourceforge.net, paultcochrane@users.sourceforge.net",
      maintainer="Alexei Gilchrist and Paul Cochrane",
      maintainer_email="aalexei@users.sourceforge.net, paultcochrane@users.sourceforge.net",
      url="http://pyscript.sourceforge.net",
      license="GPL",
      keywords="presentation scientific/engineering graphics drawing",
      platforms="OS Independent",
      packages=['pyscript','pyscript.lib','pyscript.fonts'],
      scripts=['bin/pyscript'],
)


