using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Oryx.Utilities.ValueType;
using Newtonsoft.Json.Linq;

namespace Oryx.Database.Tools
{
    public class DataJsonDiff
    {
        /// <summary>
        /// will append data table item
        /// </summary>
        /// <param name="tarJson"></param>
        /// <param name="srcJson"></param>
        /// <returns></returns>
        public List<string> InCommingTables(dynamic tarJson, dynamic srcJson)
        {
            try
            {
                var tarTables = tarJson.tables as IEnumerable<dynamic>;
                var srcTables = srcJson.tables as IEnumerable<dynamic>;
                var incommingTables = new List<string>();

                foreach (var tarTableItem in tarTables)
                {
                    //不存在则加进入待增序列
                    srcTables.Any(x => x.name.Value == tarTableItem.name.Value)
                        .FalseAction(() =>
                        {
                            incommingTables.Add(tarTableItem.name.Value);
                        });
                }
                return incommingTables;
            }
            catch (Exception exc)
            {
                throw;
            }

        }

        /// <summary>
        /// Will remove data table item
        /// </summary>
        /// <param name="tarJson"></param>
        /// <param name="srcJson"></param>
        /// <returns></returns>
        public List<string> OutCommingTables(dynamic tarJson, dynamic srcJson)
        {
            try
            {
                var tarTables = tarJson.tables as IEnumerable<dynamic>;
                var srcTables = srcJson.tables as IEnumerable<dynamic>;
                var outcommingTables = new List<string>();

                foreach (var srcTableItem in srcTables)
                {
                    //不存在则进入待减序列
                    tarTables.Any(x => x.name.Value == srcTableItem.name.Value)
                        .FalseAction(() =>
                        {
                            outcommingTables.Add(srcTableItem.name.Value);
                        });
                }
                return outcommingTables;
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        /// <summary>
        /// Will append Column item
        /// </summary>
        /// <param name="tarJson"></param>
        /// <param name="srcJson"></param>
        /// <returns></returns>
        public Dictionary<string, List<dynamic>> InCommingColumns(dynamic tarJson, dynamic srcJson)
        {
            try
            {
                var tarColumns = tarJson.columns as JObject;
                var srcColumns = srcJson.columns as JObject;
                var incommingCol = new Dictionary<string, List<dynamic>>();
                //JObject
                foreach (var tarColumnItem in tarColumns)
                {
                    var columnOfTableName = tarColumnItem.Key;
                    var incColList = new List<dynamic>();
                    var tarColumnList = tarColumns[columnOfTableName] as IEnumerable<dynamic>;
                    var srcColumnList = srcColumns[columnOfTableName] as IEnumerable<dynamic>;

                    foreach (var tarCol in tarColumnList)
                    {
                        if (srcColumnList != null)
                        {
                            srcColumnList.Any(x => x.name.Value == tarCol.name.Value)
                             .FalseAction(() =>
                             {
                                 incColList.Add(tarCol);
                             });
                        }
                        else
                        {
                            incColList.Add(tarCol);
                        }
                    }
                    if (incColList.Count > 0)
                    {
                        incommingCol.Add(columnOfTableName, incColList);
                    }
                }
                return incommingCol;
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        public Dictionary<string, List<dynamic>> OutCommingColumn(dynamic tarJson, dynamic srcJson)
        {
            try
            {
                var tarColumns = tarJson.columns as JObject;
                var srcColumns = srcJson.columns as JObject;
                var incommingCol = new Dictionary<string, List<dynamic>>();

                foreach (var tarColumnItem in tarColumns)
                {
                    var columnOfTableName = tarColumnItem.Key;
                    var incColList = new List<dynamic>();
                    var tarColumnList = tarColumns[columnOfTableName] as IEnumerable<dynamic>;
                    var srcColumnList = srcColumns[columnOfTableName] as IEnumerable<dynamic>;

                    if (srcColumnList == null)
                    {
                        continue;
                    }

                    foreach (var srcCol in srcColumnList)
                    {
                        tarColumnList.Any(x => x.name.Value == srcCol.name.Value)
                                .FalseAction(() =>
                                {
                                    incColList.Add(srcCol);
                                });
                    }
                    if (incColList.Count > 0)
                    {
                        incommingCol.Add(columnOfTableName, incColList);
                    }
                }
                return incommingCol;
            }
            catch (Exception exc)
            {
                throw;
            }
        }


        public List<dynamic> InCommingRelation(dynamic tarJson, dynamic srcJson)
        {
            try
            {
                var tarRelations = tarJson.relations as IEnumerable<dynamic>;
                var srcRelations = srcJson.relations as IEnumerable<dynamic>;
                var incommingCol = new List<dynamic>();
                var incColList = new List<dynamic>();

                foreach (var tarRel in tarRelations)
                {
                    srcRelations.Any(x =>
                    x.source.tableId.Value == tarRel.source.tableId.Value &&
                    x.source.columnId.Value == tarRel.source.columnId.Value &&
                    x.target.tableId.Value == tarRel.target.tableId.Value &&
                    x.target.columnId.Value == tarRel.target.columnId.Value)
                       .FalseAction(() =>
                       {
                           incommingCol.Add(tarRel);
                       });
                }
                return incommingCol;
            }
            catch (Exception exc)
            {

                throw;
            }
        }


        public List<dynamic> OutCommingRelation(dynamic tarJson, dynamic srcJson)
        {
            try
            {
                var tarRelations = tarJson.relations as IEnumerable<dynamic>;
                var srcRelations = srcJson.relations as IEnumerable<dynamic>;
                var incommingCol = new List<dynamic>();
                var incColList = new List<dynamic>();

                foreach (var tarRel in tarRelations)
                {
                    srcRelations.Any(x => x.source.tableId.Value == tarRel.source.tableId.Value &&
                        x.source.columnId.Value == tarRel.source.columnId.Value &&
                        x.target.tableId.Value == tarRel.target.tableId.Value &&
                        x.target.columnId.Value == tarRel.target.columnId.Value)
                        .FalseAction(() =>
                        {
                            incommingCol.Add(tarRel);
                        });
                }
                return incommingCol;
            }
            catch (Exception exc)
            {
                throw;
            }
        }

    }
}
