using Oryx.Wx.Pay.ViewModel;
using Oryx.Wx.Pay.WxPayAPI;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Oryx.Wx.Pay
{
    public class WeAppPayApi
    {
        public WeAppPayApi()
        {
        }

        /// <summary>
        /// 统一下下单接口, 返回发起支付的Package
        /// </summary>
        /// <param name="unifierViewModel"></param>
        /// <returns></returns>
        public static string UnifiedOrder(UnifierOrderViewModel unifierViewModel)
        {
            var wxPayData = new WxPayData();
            wxPayData.SetValue("appid", WxPayConfig.APPID);
            wxPayData.SetValue("mch_id", WxPayConfig.MCHID);
            wxPayData.SetValue("body", unifierViewModel.Body);
            wxPayData.SetValue("attach", unifierViewModel.Attach);
            wxPayData.SetValue("out_trade_no", unifierViewModel.OutTradeNo);
            wxPayData.SetValue("total_fee", unifierViewModel.TotalFee);
            wxPayData.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            wxPayData.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            wxPayData.SetValue("goods_tag", unifierViewModel.Tag);
            wxPayData.SetValue("trade_type", "JSAPI");
            wxPayData.SetValue("openid", unifierViewModel.OpenId);
            wxPayData.SetValue("notify_url", unifierViewModel.NotifyUrl);

            var result = WxPayAPI.WxPayApi.UnifiedOrder(wxPayData);

            var jsonResult = GetJsApiParameters(result);

            return jsonResult;
        }

        public static string GetJsApiParameters(WxPayData unifiedOrderResult)
        {

            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();

            return parameters;
        }

        public static string GenOutTradeNo()
        {
            return WxPayAPI.WxPayApi.GenerateOutTradeNo();
        }

        public static string ProcessNotify(Stream IntpuStream, out WxPayData notifyData)
        {
            var notifyProcessor = new ResultNotify();
            //WxPayData notifyData;
            var responseStr = notifyProcessor.ProcessNotify(IntpuStream, out notifyData);
            return responseStr;
        }

        public static Stream MakeQRCode(string str)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(30);
            //qrCodeImage.Save(AppDomain.CurrentDomain.BaseDirectory + "testqrcode1.jpg");
            //保存为PNG到内存流  
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Jpeg);
            //var bytes = new byte[ms.Length];

            //File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "testqrcode3.jpg", bytes);
            return ms;
            ////输出二维码图片
            //Response.BinaryWrite(ms.GetBuffer());
            //Response.End();
        }
    }
}
