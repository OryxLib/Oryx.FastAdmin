
from django.urls import path
from akshareWeb.views import homePage,getZhASpot,getzhastock
urlpatterns = [
    path('',homePage),
    path('index/',homePage),
    path('getastockspot/', getZhASpot),
    path('getastocklist/', getzhastock),
]
