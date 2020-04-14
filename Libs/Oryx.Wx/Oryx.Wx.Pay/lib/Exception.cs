using System;
using System.Collections.Generic;
using System.Web;

namespace Oryx.Wx.Pay.WxPayAPI
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}