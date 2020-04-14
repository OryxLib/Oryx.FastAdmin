using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Utilities
{
    public class PDFTool
    {
        public static string LicenceKey = "76FFY-UB95C-9LT94-Y3L9B-IYIIZ";
        public static async Task<List<Stream>> PDFToImage(string path)
        {
            // replace string.Empty with your license key
            LicenseManager.AddLicenseData(LicenceKey);

            return await Task.Run(() =>
            {
                var streamList = new List<Stream>();
                using (PdfDocument pdf = new PdfDocument(path))
                {
                    PdfDrawOptions options = PdfDrawOptions.Create();
                    options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                    options.Compression = ImageCompressionOptions.CreateJpeg();
                    foreach (var page in pdf.Pages)
                    {
                        var outputStream = new MemoryStream();
                        page.Save(outputStream, options);
                        streamList.Add(outputStream);
                    }
                }
                return streamList;
            });
        }

        public static void PDFToImage(string path, string savePath, string extension)
        {
            // replace string.Empty with your license key
            LicenseManager.AddLicenseData(LicenceKey);

            using (PdfDocument pdf = new PdfDocument(path))
            {
                PdfDrawOptions options = PdfDrawOptions.Create();
                options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                options.Compression = ImageCompressionOptions.CreateJpeg();
                var index = 1;
                foreach (var pdfPage in pdf.Pages)
                {
                    var fileName = savePath + "-" + index++ + extension;
                    pdfPage.Save(fileName, options);
                }
            }
        }
    }
}
