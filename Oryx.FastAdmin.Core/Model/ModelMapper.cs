using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SqlSugar;
using System.Threading.Tasks;
using System.Dynamic;
using Oryx.Utilities.ObjectTool;

namespace Oryx.FastAdmin.Core.Model
{
    public class ModelMapper
    {
        SqlSugarClient SqlSugarClient;
        public ModelMapper(SqlSugarClient _SqlSugarClient)
        {
            SqlSugarClient = _SqlSugarClient;
        }
        public async Task<List<ModelInfo>> ModelToFormControl<T>()
        {
            var modelType = typeof(T);

            return await GetModelInfo(modelType);
        }

        private async Task<List<ModelInfo>> GetModelInfo(Type modelType)
        {
            var props = modelType.GetProperties();

            var modelInfoList = new List<ModelInfo>();

            foreach (var prop in props)
            {
                if (prop.CustomAttributes.Any(x => x.AttributeType == typeof(ModelType)))
                {
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        ModelType modelTypeAttr = attr as ModelType;
                        if (modelTypeAttr != null)
                        {
                            string propName = prop.Name;
                            string mtName = modelTypeAttr.Name;

                            var dataSrouce = await GetDataSourceData(modelTypeAttr.DataSourceTable, modelTypeAttr.DataSourceQuery, modelTypeAttr.DataSourceTableValue);

                            if (dataSrouce == null)
                            {
                                dataSrouce = GetDataSrouceFromMeta(attrs);
                            }

                            List<ModelInfo> subModelInfoList = null;
                            string typeName = "";
                            if (modelTypeAttr.ControlName == "dynamicgroup")
                            {
                                //subModelInfoList = await GetModelInfo(prop.PropertyType.GenericTypeArguments[0]);
                                typeName = prop.PropertyType.GenericTypeArguments[0].Name;
                            }

                            modelInfoList.Add(new ModelInfo
                            {
                                Name = modelTypeAttr.Name,
                                PropName = propName,
                                ControlType = modelTypeAttr.ControlName,
                                DataSource = dataSrouce,
                                Order = modelTypeAttr.Order,
                                ShowOnList = modelTypeAttr.ShowOnList,
                                Required = modelTypeAttr.Required,
                                TypeName = typeName,
                                SubModelInfoList = subModelInfoList
                            });
                            continue;
                        }
                    }
                }
                else
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "System.ListGeneric":
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                PropName = prop.Name,
                                ControlType = "select",
                                Order = modelInfoList.Count()
                            });
                            break;
                        case "System.Decimber":
                        case "System.Int64":
                        case "System.Int":
                        case "System.Double":
                        case "System.Float":
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                ControlType = "number",
                                PropName = prop.Name,
                                Order = modelInfoList.Count()
                            });
                            break;
                        case "System.String":
                        default:
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                PropName = prop.Name,
                                ControlType = "input",
                                Order = modelInfoList.Count()
                            });
                            break;
                    }
                }
            }

            return modelInfoList;
        }

        public async Task<ModelData> ModelToData<T>(T model)
        {
            var modelType = typeof(T);

            var props = modelType.GetProperties();

            var modelData = new ModelData();

            foreach (var prop in props)
            {
                //if (prop.CustomAttributes.Any(x => x.AttributeType == typeof(ModelType)))
                //{
                //    object[] attrs = prop.GetCustomAttributes(true);
                //    List<ModelData> ModelDataList = null;
                //    foreach (object attr in attrs)
                //    {
                //        ModelType modelTypeAttr = attr as ModelType;
                //        if (modelTypeAttr != null)
                //        {
                //            string propName = prop.Name;
                //            string mtName = modelTypeAttr.Name;

                //            if (modelTypeAttr.ControlName == "dynamicgroup")
                //            {
                //                ModelDataList = await GetSubDataList(modelTypeAttr.DataSourceTable, modelTypeAttr.DataSourceQuery, prop.PropertyType.GenericTypeArguments[0]);
                //            }
                //        }
                //    }
                //    modelData.Add(prop.Name, ModelDataList);
                //}
                //else
                //{
                //    modelData.Add(prop.Name, prop.GetValue(model));
                //}
                modelData.Add(prop.Name, prop.GetValue(model)); 
            }

            return modelData;
        }

        private async Task<List<ModelData>> GetSubDataList(string table, string query, Type dataType)
        {
            var resultData = new List<ModelData>();
            var result = await SqlSugarClient.Queryable(table, table).Where(query).ToListAsync();
            var subProps = dataType.GetProperties();
            foreach (var item in result)
            {
                var modelData = new ModelData();
                foreach (var subprop in subProps)
                {
                    modelData.Add(subprop.Name, subprop.GetValue(subprop)?.ToString() ?? "");
                }
                resultData.Add(modelData);
            }
            return resultData;
        }

        public async Task<ModelTable> ModelToTable<T>()
        {
            var modelType = typeof(T);

            var props = modelType.GetProperties();

            var modelInfoList = await ModelToFormControl<T>();

            var modelTable = new ModelTable();
            modelTable.TableInfoList = new List<TableInfo>();

            foreach (var modelInfo in modelInfoList.Where(x => x.ShowOnList))
            {
                modelTable.TableInfoList.Add(new TableInfo
                {
                    Name = modelInfo.Name,
                    PropName = modelInfo.PropName
                });
            }
            return modelTable;
        }

        private Dictionary<string, string> GetDataSrouceFromMeta(object[] attrs)
        {
            var resultData = new Dictionary<string, string>();
            foreach (var attr in attrs)
            {
                ModelBindData bindDataAttr = attr as ModelBindData;
                if (bindDataAttr != null)
                {
                    resultData.Add(bindDataAttr.Key, bindDataAttr.Value);
                }
            }

            return resultData;
        }

        private async Task<Dictionary<string, string>> GetDataSourceData(string table, string query, string value)
        {
            List<ExpandoObject> dataResult;
            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (!string.IsNullOrEmpty(query))
            {
                dataResult = await SqlSugarClient.Queryable(table, table).Where(query).ToListAsync();
            }
            else
            {
                dataResult = await SqlSugarClient.Queryable(table, table).ToListAsync();
            }
            var result = new Dictionary<string, string>();
            foreach (var item in dataResult)
            {
                var idObj = item.Where(x => x.Key == "Id").FirstOrDefault();
                var valueObj = item.Where(x => x.Key == value).FirstOrDefault();
                result.Add(idObj.Value?.ToString() ?? "", valueObj.Value?.ToString() ?? "");
            }
            return result;
        }
    }
}
