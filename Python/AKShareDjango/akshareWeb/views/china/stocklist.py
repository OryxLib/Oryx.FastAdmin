#!/usr/bin/python
# -*- coding: UTF-8 -*-

from django.shortcuts import render
from django.http import HttpResponse
import akshare

#行业
def stock_hy_list(request): 
    context ={}
    datalist = akshare.get_zh_a_stock_hy()
    context['stocklist'] = datalist
    return render(request,'stock/china/stock.html',context)

def stock_hy_list_api(request):
    datalist = akshare.get_zh_a_stock_hy() 
    return HttpResponse.write(datalist)

#概念
def stock_hy_list(request): 
    context ={}
    datalist = akshare.get_zh_a_gainian()
    context['stocklist'] = datalist
    return render(request,'index.html',context)

#热门概念
def stock_hy_list(request): 
    context ={}
    datalist = akshare.get_zh_a_hot_gainian()
    context['stocklist'] = datalist
    return render(request,'index.html',context)

#地域
def stock_hy_list(request): 
    context ={}
    datalist = akshare.get_zh_a_diyu()
    context['stocklist'] = datalist
    return render(request,'indedatalistx.html',context)

 

