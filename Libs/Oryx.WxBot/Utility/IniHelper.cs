using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oryx.WxBot
{
    public class IniHelper
    { 
        private string sPath = null;
        public IniHelper(string path)
        {
            this.sPath = path;
        }
          
        public void WriteValue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            //WritePrivateProfileString(section, key, value, sPath);
        }

        public string ReadValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(1000);
            // section=配置节，key=键名，temp=上面，path=路径
            //GetPrivateProfileString(section, key, "", temp, 1000, sPath);
            return temp.ToString();
        }
    }

   


}
