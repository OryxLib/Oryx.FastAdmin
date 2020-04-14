
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Oryx.Utilities.ExcelTool
{
    public class ExcelHelper
    {
        public static ExcelWorkbook LoadExcel(Stream stream)
        {
            var xls = new ExcelPackage(stream);
            return xls.Workbook;
        }

        public static List<T> LoadModel<T>(Stream stream)
            where T : class, new()
        {
            var xlsPkg = LoadExcel(stream);
            var sheet = xlsPkg.Worksheets[1];

            var type = typeof(T);
            var props = type.GetProperties();

            var modelList = new List<T>();
            //sheet.Cells.Table
            var array = sheet.Cells.Value as Array;
            var rowNum = array.GetUpperBound(0) + 1;
            var colNum = array.GetUpperBound(1) + 1;

            for (int rowIndex = 1; rowIndex <= rowNum; rowIndex++)
            {
                if (rowIndex == 1)//第一列为Header列
                {
                    continue;
                }
                var tModel = new T();
                foreach (var prop in props)
                {
                    var targetValue = GetValueBySheetHead(prop.Name, sheet, rowIndex, colNum);
                    prop.SetValue(tModel, targetValue);
                }
                modelList.Add(tModel);
            }

            return modelList;
        }

        private static string GetValueBySheetHead(string name, ExcelWorksheet sheet, int rowIndex, int totalCol)
        {
            var targetColIndex = -1;
            //var array = cells.Value as Array;
            //var colNum = array.GetUpperBound(1) + 1;
            for (int colIndex = 1; colIndex <= totalCol; colIndex++)
            {
                var currValue = sheet.GetValue<string>(1, colIndex).Trim().Replace(" ", "");
                if (currValue == name)
                {
                    targetColIndex = colIndex;
                    break;
                }
            }
            if (targetColIndex == -1)
            {
                return null;
            }
            return sheet.GetValue<string>(rowIndex, targetColIndex);
        }

        public static Stream SaveExcel<T>(List<T> dataList)
        {
            var dataMatrix = ModelToDataMatrix(dataList);
            return SaveExcel(dataMatrix);
        }
        public static Stream SaveExcel<T>(List<T> dataList, bool showSerial)
        {
            var dataMatrix = ModelToDataMatrix(dataList);
            return SaveExcel(dataMatrix, showSerial);
        }

        public static Stream SaveExcel(string[][] dataMatrix, bool showSerial = false)
        {
            var outputStream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(outputStream))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                var serialOffset = 0;
                if (showSerial)
                {
                    serialOffset = 1;
                }
                for (int rowIndex = 0; rowIndex < dataMatrix.Length; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < dataMatrix[rowIndex].Length + serialOffset; colIndex++)
                    {
                        if (showSerial)
                        {

                            if (colIndex == 0)
                            {
                                worksheet.SetValue(rowIndex + 1, colIndex + 1, rowIndex + 1);
                            }
                            else
                            {
                                worksheet.SetValue(rowIndex + 1, colIndex + 1, dataMatrix[rowIndex][colIndex - serialOffset]);
                            }
                        }
                        else
                        {
                            worksheet.SetValue(rowIndex + 1, colIndex + 1, dataMatrix[rowIndex][colIndex]);
                        }
                    }
                }
                package.Save();
            }
            outputStream.Position = 0;
            return outputStream;
        }

        public static string[][] ModelToDataMatrix<T>(List<T> dataList)
        {
            var typeInfo = typeof(T);
            var props = typeInfo.GetProperties();
            var headRow = 1;
            var dataMatrix = new string[dataList.Count + headRow][];
            for (int rowIndex = 0; rowIndex < dataList.Count + headRow; rowIndex++)
            {
                dataMatrix[rowIndex] = new string[props.Length];
                if (rowIndex == 0)
                {
                    for (int colIndex = 0; colIndex < props.Length; colIndex++)
                    {
                        var prop = props[colIndex];
                        dataMatrix[rowIndex][colIndex] = prop.Name;
                    }
                }
                else
                {
                    for (int colIndex = 0; colIndex < props.Length; colIndex++)
                    {
                        var prop = props[colIndex];
                        if (dataMatrix[rowIndex] == null)
                        {
                            dataMatrix[rowIndex] = new string[props.Length];
                        }
                        dataMatrix[rowIndex][colIndex] = prop.GetValue(dataList[rowIndex - 1])?.ToString() ?? string.Empty;
                    }
                }
            }
            return dataMatrix;
        }


    }
}
