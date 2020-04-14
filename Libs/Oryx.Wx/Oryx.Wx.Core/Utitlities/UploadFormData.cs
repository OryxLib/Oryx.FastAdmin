using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Wx.Core.Utitlities
{

    public static class UploadFormData
    {
        public static string UploadMaterialImage(string url, string uploadKey, Stream fileStream)
        {
            //var apitype = "material";
            //var url = string.Concat(BaseUrl, apitype, "/add_material", "?access_token=", token, "&type=", "thumb");
            var boundary = "fbce142e-4e8e-4bf3-826d-cc3cf506cccc";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "KnowledgeCenter");
            client.DefaultRequestHeaders.Remove("Expect");
            client.DefaultRequestHeaders.Remove("Connection");
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.DefaultRequestHeaders.ConnectionClose = true;
            var content = new MultipartFormDataContent(boundary);
            content.Headers.Remove("Content-Type");
            content.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
            var contentByte = new StreamContent(fileStream);
            content.Add(contentByte);
            contentByte.Headers.Remove("Content-Disposition");
            contentByte.Headers.TryAddWithoutValidation("Content-Disposition", "form-data; name=\"media\";filename=\"" + uploadKey + ".png\"" + "");
            contentByte.Headers.Remove("Content-Type");
            contentByte.Headers.TryAddWithoutValidation("Content-Type", "image/png");
            try
            {
                var result = client.PostAsync(url, content).Result;
                //if (result.Result.StatusCode != HttpStatusCode.OK)
                //    throw new Exception(result.Result.Content.ReadAsStringAsync().Result);

                return result.Content.ReadAsStringAsync().Result;
                //if (result.Result.Content.ReadAsStringAsync().Result.Contains("media_id"))
                //{
                //    var resultContent = result.Result.Content.ReadAsStringAsync().Result;
                //    var materialEntity = JsonConvert.DeserializeObject<MaterialImageReturn>(resultContent);
                //    return materialEntity;
                //}
                //throw new Exception(result.Result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message + ex.InnerException.Message);
                return ex.InnerException.Message;
            }
        }


        public static string PostFormFile(this HttpWebRequest _httpRequest, string uploadKey, Stream fileStream)
        {
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");

            var beginBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--\n");

            var endBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");

            _httpRequest.Method = "POST";
            _httpRequest.Timeout = 300;
            _httpRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            string fileHeader =
                $"Content-Disposition: form-data; name=\"{uploadKey}\"; filename=\"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}\"\n" +
                "Content-Type: application/octet-stream\n"; ;
            var fileHeaderBytes = Encoding.UTF8.GetBytes(fileHeader);

            var reqStream = _httpRequest.GetRequestStream();
            var reqStreamWriter = new StreamWriter(reqStream);
            //reqStream.Write(beginBoundary, 0, beginBoundary.Length);
            reqStreamWriter.Write(beginBoundary);
            reqStreamWriter.Write(fileStream);
            reqStreamWriter.Write(endBoundary);
            //reqStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);
            //fileStream.Position = 0;
            //fileStream.CopyTo(reqStream);
            //reqStream.Write(endBoundary, 0, endBoundary.Length);
            reqStream.Close();

            var response = _httpRequest.GetResponse();
            var resReader = new StreamReader(response.GetResponseStream());
            return resReader.ReadToEnd();
        }
    }

}
