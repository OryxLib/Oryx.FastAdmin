using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Oryx.Utilities
{
    public class ZipTool
    {
        public static string Exact(string path)
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }
            var outputDirectory = Path.GetDirectoryName(path);
            var targetDir = outputDirectory + "\\" + Path.GetFileNameWithoutExtension(path);

            if (Directory.Exists(targetDir))
            {
                return targetDir;
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            ZipFile.ExtractToDirectory(path, outputDirectory);



            return targetDir;
        }
    }
}
