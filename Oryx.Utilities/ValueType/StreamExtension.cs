using System.IO;
using System.Threading.Tasks;

namespace Oryx.Utilities.ValueType
{
    public static class StreamExtension
    {
        public static async Task<string> GetString(this Stream stream)
        {
            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}