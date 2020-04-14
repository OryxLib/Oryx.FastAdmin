#!/usr/bin/python
# -*- coding: UTF-8 -*-

from django.shortcuts import render
from django.http import HttpResponse

# Create your views here.
import akshare

def homePage(request):
    context ={}
    context['hello'] = 'Hello World!'
    print(context)
    return render(request,'/',context)

def getZhASpot(request):
    zh_a_spot = akshare.stock_zh_a_spot()
    return HttpResponse(zh_a_spot)

def getzhastock(request):
    zhastocklist = akshare.get_zh_a_stock_hy()
    return HttpResponse(zhastocklist)