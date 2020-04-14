using Qiniu.Common;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Utilities
{
    public class QiniuTool
    {
        public static Mac macKey;
        public static string bucket = "pagetechs-scratch";
        public static string GenerateToken()
        {
            return GenerateToken(null);
        }
        public static string GenerateToken(string fileName)
        {
            string AK = "vRlRmKlkqx9qY9CTWTinD-OJowSeYw5-Vg1nktUA";
            string SK = "4gGxVhemV8DENWtUAJqs1L_xTq5QlazSnnkXfwgP";

            //string AK = "vRlRmKlkqx9qY9CTWTinD-OJowSeYw5-Vg1nktUA";
            //string SK = "4gGxVhemV8DENWtUAJqs1L_xTq5QlazSnnkXfwgP";

            // ZoneID zoneId = ZoneID.CN_East; 
            // [CN_East:华东] [CN_South:华南] [CN_North:华北] [US_North:北美]
            // USE_HTTPS = (true|false) 是否使用HTTPS
            Config.SetZone(ZoneID.CN_South, false);


            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            if (string.IsNullOrEmpty(fileName))
            {
                putPolicy.Scope = bucket;

            }
            else
            {
                putPolicy.Scope = bucket + ":" + fileName;
            }
            // putPolicy.Scope = bucket + ":" + saveKey;
            //putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;

            Mac mac = new Mac(AK, SK);
            macKey = mac;
            Auth auth = new Auth(mac);
            string token = auth.CreateUploadToken(putPolicy.ToJsonString());

            return token;
        }
        public static string GenerateToken(string fileName, string _bucket, ZoneID zoneID)
        {
            string AK = "vRlRmKlkqx9qY9CTWTinD-OJowSeYw5-Vg1nktUA";
            string SK = "4gGxVhemV8DENWtUAJqs1L_xTq5QlazSnnkXfwgP";

            //string AK = "vRlRmKlkqx9qY9CTWTinD-OJowSeYw5-Vg1nktUA";
            //string SK = "4gGxVhemV8DENWtUAJqs1L_xTq5QlazSnnkXfwgP";

            // ZoneID zoneId = ZoneID.CN_East; 
            // [CN_East:华东] [CN_South:华南] [CN_North:华北] [US_North:北美]
            // USE_HTTPS = (true|false) 是否使用HTTPS
            Config.SetZone(zoneID, false); 
            bucket = _bucket;


            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            if (string.IsNullOrEmpty(fileName))
            {
                putPolicy.Scope = bucket;

            }
            else
            {
                putPolicy.Scope = bucket + ":" + fileName;
            }
            // putPolicy.Scope = bucket + ":" + saveKey;
            //putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;

            Mac mac = new Mac(AK, SK);
            macKey = mac;
            Auth auth = new Auth(mac);
            string token = auth.CreateUploadToken(putPolicy.ToJsonString());

            return token;
        }

        public static async Task DeleteImage(string[] saveKey)
        {
            GenerateToken();
            var mgr = new Qiniu.RS.BucketManager(macKey);
            await mgr.BatchDeleteAsync(bucket, saveKey);
        }

        public static async Task UploadImage(string fileName, string saveKey)
        {
            var token = GenerateToken();
            UploadManager um = new UploadManager(putThreshold: 4 * 1024 * 1024);
            um.SetChunkUnit(ChunkUnit.U4096K);
            um.SetUploadController(new UploadController(ResumableUploader.DefaultUploadController));
            await um.UploadFileAsync(fileName, saveKey, token);
        }

        public static async Task UploadImage(Stream stream, string saveKey)
        {
            var token = GenerateToken();
            UploadManager um = new UploadManager(putThreshold: 4 * 1024 * 1024);
            um.SetChunkUnit(ChunkUnit.U4096K);
            um.SetUploadController(new UploadController(ResumableUploader.DefaultUploadController));
            stream.Position = 0;
            await um.UploadStreamAsync(stream, saveKey, token);
        }

        public static async Task UploadImage(byte[] bytes, string saveKey)
        {
            var token = GenerateToken();
            UploadManager um = new UploadManager(putThreshold: 4 * 1024 * 1024);
            um.SetChunkUnit(ChunkUnit.U4096K);
            um.SetUploadController(new UploadController(ResumableUploader.DefaultUploadController));
            await um.UploadDataAsync(bytes, saveKey, token);
        }


        public static async Task<List<string>> ListFile(string prefix = "image")
        {
            GenerateToken();
            var mgr = new Qiniu.RS.BucketManager(macKey);
            var listResult = await mgr.ListAsync(bucket, prefix, "", 20, "");
            return listResult.Result.Items.Select(x => x.Key).ToList();
        }
    }
}
