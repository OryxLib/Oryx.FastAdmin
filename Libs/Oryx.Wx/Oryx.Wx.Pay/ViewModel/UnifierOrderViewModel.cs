using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Wx.Pay.ViewModel
{
    public class UnifierOrderViewModel
    {
        public string appId { get; set; }

        public string mch_Id { get; set; }

        public string device_info { get; set; }
        public int TotalFee { get; set; }
        public string OpenId { get; set; }
        public string Body { get; set; }
        public string Attach { get; set; }
        public string Tag { get; set; }
        public string OutTradeNo { get; set; }
        public string NotifyUrl { get; set; }

        //public string 
    }
}
