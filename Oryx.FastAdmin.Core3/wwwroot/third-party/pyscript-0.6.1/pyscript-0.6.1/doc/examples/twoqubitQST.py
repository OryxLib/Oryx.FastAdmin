from pyscript import *

defaults.units=UNITS['mm']
defaults.linewidth=1

black = Color("Black")
grey = Color("LightGray")
laserCol = Color("OrangeRed")
pumpCol = Color("RoyalBlue")

def ScaledText(text,**dict):
    t=TeX(text).scale(2,2)
    apply(t,(),dict)
    return t

def arrowhead(n,wid,len,angle,col):
    
    L = P(0,len)
    W = P(wid,0)
    
    arrow = Path(n,n-L+W,n-L-W,n,bg=col).rotate(angle,p=n)
    
    return arrow

def loop(c,radius):

    return Circle(r=radius,c=c)

def fibre(c):

    loops = Group(
        loop(c+P(0,1),10),
        loop(c,10),
        loop(c-P(0,1),10)
        )

    l = 10
    x1 = P(0,l)
    x2 = P(0,1.5*l)
    x3 = P(-l/2,2*l)
    x4 = P(-l,2.5*l)

    return Group(
        Path(loops.e,
             loops.e+x1,
             C(loops.e+x2, loops.e+x2),
             loops.e+x3
             #loops.e+x4
             ),
        Path(loops.w,
             loops.w-x1,
             C(loops.w-x2, loops.w-x2),
             loops.w-x3
             #loops.w-x4
             ),
        loops
        ).rotate(-45,p=c)

def fibrecoupler(c,angle):

    boxW = 15
    boxH = 12
    nozzle = 3
    box = Rectangle(c=c+P(boxW/6,0),width=boxW,height=boxH)
    fibrepos = c
    lenspos = box.e-P(box.width/3,0)
    irispos = box.e-P(box.width/12,0)
    irisInside = P(0,1)
    irisOutside = P(0,5)
    iris = Group(
        Path(irispos+irisInside,
             irispos+irisOutside
             ),
        Path(irispos-irisInside,
             irispos-irisOutside
             )
        )

    lens = Circle(r=4)
    lens.scale(0.3,1)
    lens.c = lenspos
    coupler = Group(
        
        box,
        Path(box.w,fibrepos),
        Path(fibrepos,
             fibrepos-P(0,nozzle/2),
             fibrepos-P(nozzle,0),
             fibrepos+P(0,nozzle/2),
             fibrepos,
             bg=black
             ),
	lens,
        iris
        ).rotate(angle,p=c)

    return coupler

def fibrecollimator(c,angle):

    rectW = c
    
    collimator = Group(
        Path(c,
             c+P(0,1.5),
             c+P(-3,0),
             c+P(0,-1.5),
             c,
             bg=black
             ),
        Rectangle(w=rectW,width=2,height=8,bg=black)
        ).rotate(angle,p=c)

    return collimator

def hologram(c,angle):

    platesize = P(0,6)
    plate = Path(c-platesize,c+platesize,linewidth=3)

    stageStart = 1.25*platesize
    stageFinish = 2.25*platesize
    arrowL = 2
    arrowW = 1
    offset = arrowL/4

    hologram = Group(
        plate,
        Path(c+stageStart,c+stageFinish-P(0,offset)),
        Circle(r=0.75,c=c+stageStart,bg=black),
        arrowhead(c+stageFinish,arrowW,arrowL,0,black),
        ScaledText(r"$x$",c=c+stageFinish-P(4,0)),
        ScaledText(r"$y$",c=c+stageStart-P(4,0))
        )

    return hologram

def laser(e):

    box = Rectangle(e=e,width=20,height=8,bg=grey)
    lasername = ScaledText(r'Laser',c=box.c,fg=Color("DarkBlue"));

    #I want to do a squiggle with an arrow (the light symbol)

    return Group(
        box,
        lasername,
        )

coupler1 = fibrecoupler(P(180,10),180)
coupler2 = fibrecoupler(P(180,10),180)

analyser1 = Group(
    
    Path(P(110,10),P(150,10),linewidth=2,fg=laserCol),
    arrowhead(P(140,10),1,3,90,laserCol),

    Path(P(150,10),P(180,10),linewidth=2,fg=laserCol).rotate(-22.5,p=P(150,10)),

    hologram(P(150,10),0),

    coupler1.rotate(-22.5,p=P(150,10))

    )

analyser2 = Group(
    
    Path(P(110,10),P(150,10),linewidth=2,fg=laserCol),
    arrowhead(P(140,10),1,3,90,laserCol),

    Path(P(150,10),P(180,10),linewidth=2,fg=laserCol).rotate(22.5,p=P(150,10)),

    hologram(P(150,10),0),

    coupler2.rotate(22.5,p=P(150,10))

    )

analyser1.rotate(40,p=P(110,10))
point1 = analyser1.itoe(analyser1[4].w)
analyser2.rotate(-40,p=P(110,10))
point2 = analyser2.itoe(analyser2[4].w)

fibre1 = fibre(P(220,10))
fibre1.scale(1,-1,p=fibre1.c)
#fibre1.move(point1+P(10,-5)-fibre1.itoe(fibre1[0].path[-1]))
fibre1.move(point1+P(10,-5)-fibre1.itoe(fibre1[0].P(1)))

fibre2 = fibre(P(220,10))
#fibre2.move(point2+P(10,5)-fibre2.itoe(fibre2[0].path[-1]))
fibre2.move(point2+P(10,5)-fibre2.itoe(fibre2[0].P(1)))

detector1 = Group(
    Rectangle(c=P(240,10),width=10,height=10,bg=black),
    Circle(r=5,c=P(245,10),bg=black),
)
#detector1.move(fibre1.itoe(fibre1[1].path[-1])-detector1[1].w)
detector1.move(fibre1.itoe(fibre1[1].P(1))-detector1[1].w)
detector2 = Group(
    Rectangle(c=P(240,10),width=10,height=10,bg=black),
    Circle(r=5,c=P(245,10),bg=black),
)
#detector2.move(fibre2.itoe(fibre2[1].path[-1])-detector2[1].w)
detector2.move(fibre2.itoe(fibre2[1].P(1))-detector2[1].w)

render(
    laser(P(70,10)),

    Path(P(70,10),P(110,10),linewidth=2,fg=pumpCol),
    arrowhead(P(100,10),1,3,90,pumpCol),

    Path(P(110,10),P(140,10),linewidth=2,fg=pumpCol),
    arrowhead(P(130,10),1,3,90,pumpCol),
    Rectangle(c=P(140,10),width=4,height=4,fg=None,bg=black),

    analyser1,
    analyser2,
    # label analyser hologram
    # label coupler2

    Rectangle(c=P(110,10),height=12,width=3,bg=grey),
    ScaledText(r'BBO',n=P(110,2)),

    ScaledText(r'coupler 2',s=point2+P(-5,5)),

    Path(
    point1,
    C(point1+P(5,-5), point1+P(5,-5)),
    point1+P(10,-5),
    ),
    Path(
    point2,
    C(point2+P(5,5), point2+P(5,5)),
    point2+P(10,5),
    ),

    ScaledText(r'holo 1',s=P(140,-28)),
    ScaledText(r'holo 2',s=P(135,48)),
    # label preparation hologram

    # insert fibre between coupler and fibre loop
    fibre1,
    Rectangle(width=20,height=7,c=fibre1.c,bg=Color(1)),
    ScaledText(r'SMF',c=fibre1.c),
    fibre2,
    Rectangle(width=20,height=7,c=fibre2.c,bg=Color(1)),
    ScaledText(r'SMF',c=fibre2.c),

    # insert fibre between coupler and detector
    detector1,
    ScaledText(r'detector 1',nw=detector1.itoe(detector1[1].s)+P(-3,-3)),
    # label detector
    detector2,
    ScaledText(r'detector 2',sw=detector2.itoe(detector2[1].n)+P(-3,3)),
    # label detector

    Path(detector1.itoe(detector1[1].e),
         detector1.itoe(detector1[1].e)+P(20,0)
         ),
    Path(detector2.itoe(detector2[1].e),
         detector2.itoe(detector2[1].e)+P(20,0)
         ),
    arrowhead(detector1.itoe(detector1[1].e)+P(20,0),1,3,90,black),
    arrowhead(detector2.itoe(detector2[1].e)+P(20,0),1,3,90,black),

    ScaledText(r'''\begin{center}
    Coincidence Counting\\Electronics\end{center}''',
               w=P(240,10)),
    
    file="twoqubitQST.eps"

    )
