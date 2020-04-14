#!/usr/bin/python
# -*- coding: UTF-8 -*-

from django.shortcuts import render
from django.http import HttpResponse


def accountProfile(request):
    context ={}
    return render(request,'registration/profile.html',context)