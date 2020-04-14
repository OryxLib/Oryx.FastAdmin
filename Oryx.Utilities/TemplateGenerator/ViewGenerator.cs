using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Oryx.Utilities.TemplateGenerator
{
    public class ViewGenerator
    {
        public List<TemplateMetaData> Generator<T>(T model)
        {
            var tmpMetaDataList = new List<TemplateMetaData>();
            var _typeRef = typeof(T);
            var props = _typeRef.GetProperties();

            foreach (var propItem in props)
            {
                TemplateMetaData metaData = GeneratorCore(propItem, model);

                tmpMetaDataList.Add(metaData);
            }

            return tmpMetaDataList;
        }

        private TemplateMetaData GeneratorCore(PropertyInfo propItem, object model)
        {
            var typeString = EnumeratPropInfos(propItem);
            var customeAttr = GetCustomeMetaData(propItem);
            TemplateMetaData metaData;
            if (typeString == "reference")
            {
                metaData = GenerateReference(propItem, model);
            }
            else if (typeString == "list")
            {
                var listValue = model == null ? null : propItem.GetValue(model);
                metaData = GenerateTable(propItem, listValue);
            }
            else
            {
                metaData = new TemplateMetaData
                {
                    Name = customeAttr?.Name ?? propItem.Name,
                    EleType = customeAttr?.EleType ?? typeString,
                    Data = model != null ? propItem.GetValue(model) : null
                };
            }

            return metaData;
        }

        private TemplateMetaData GenerateReference(PropertyInfo propItem, object model)
        {
            var tmpMetaData = new TemplateMetaData();

            var customeAttr = GetCustomeMetaData(propItem);
            tmpMetaData.Name = customeAttr?.Name ?? propItem.Name;
            tmpMetaData.EleType = customeAttr?.EleType ?? "table";
            tmpMetaData.Data = model;
            tmpMetaData.Link = customeAttr?.Link ?? "/ProcessMetaData";
            //var tempStr = listProp.DeclaringType;
            return tmpMetaData;
        }

        public TemplateMetaData GenerateTable(PropertyInfo listProp, object listValue)
        {
            var tmpMetaData = new TemplateMetaData();

            var targetProperties = listProp.PropertyType.GenericTypeArguments[0].GetProperties();

            tmpMetaData.Columns = new List<string> { };
            if (listValue != null)
            {
                IList targetObjList = listProp.GetValue(listValue, null) as IList;
                foreach (var tarObj in targetObjList)
                {
                    foreach (var targetProp in targetProperties)
                    {
                        var metaData = GeneratorCore(targetProp, tarObj);
                        if (metaData == null) continue;
                        tmpMetaData.Columns.Add(metaData.Name);
                    }
                }
            }
            var customeAttr = GetCustomeMetaData(listProp);
            tmpMetaData.Name = customeAttr?.Name ?? listProp.Name;
            tmpMetaData.EleType = customeAttr?.EleType ?? "table";
            tmpMetaData.Data = listValue;
            var tempStr = listProp.DeclaringType;
            return tmpMetaData;
        }

        private string GetPropInfoName(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.ToString();
        }

        private TemplateMetaData GetCustomeMetaData(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute<TemplateMetaDataAttribute>();
            var metaData = new TemplateMetaData();

            metaData.Name = attr?.Name;
            metaData.Name = attr?.Link;
            return metaData;
        }

        private string EnumeratPropInfos(PropertyInfo propertyInfo)
        {
            var typeStr = propertyInfo.PropertyType.ToString();
            var eleType = string.Empty;
            if (!propertyInfo.PropertyType.ToString().StartsWith("System."))
            {
                eleType = "reference";
            }
            else if (typeStr.Contains("List`1"))
            {
                eleType = "list";
            }
            else
            {
                switch (typeStr)
                {
                    case "System.Guid":
                        eleType = "text";
                        break;
                    case "System.String":
                        eleType = "text";
                        break;
                    case "System.Int32":
                        eleType = "number";
                        break;
                    case "System.Int64":
                        eleType = "number";
                        break;
                    case "System.DateTime":
                        eleType = "datetime";
                        break;
                    case "System.Boolean":
                        eleType = "bool";
                        break;
                    default:
                        eleType = "input";
                        break;
                }
            }
            return eleType;
        }
    }
}
