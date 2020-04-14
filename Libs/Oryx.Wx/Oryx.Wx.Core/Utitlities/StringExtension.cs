using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Wx.Core.Utitlities
{
    public static class StringExtension
    {
        public static string Wrap(string format, IEnumerable<string> strList)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in strList)
            {
                stringBuilder.AppendFormat(format, item);
            }

            return stringBuilder.ToString();
        }
    }
}
