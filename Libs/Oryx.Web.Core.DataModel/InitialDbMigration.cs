using FluentMigrator;
using FluentMigrator.Builders.Create.Column;
using FluentMigrator.Builders.Create.ForeignKey;
using FluentMigrator.Builders.Create.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oryx.Database.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Oryx.Database
{
    public class InitialDbMigration : Migration
    {
        private dynamic json;
        public InitialDbMigration(dynamic _json)
        {
            json = _json;
        }
        /// <summary>
        /// DbJson:{
        ///     table:[],
        ///     col:{},
        ///     reference:[]
        /// }
        /// </summary>
        public override void Down()
        {

        }

        //public override void Up()
        //{  
        //    var builder = Create;

        //    foreach (var item in inTable)
        //    {
        //        ICreateTableWithColumnOrSchemaOrDescriptionSyntax tableBuilder = builder.Table(item);
        //        tableBuilder.WithColumn("Id").AsGuid();
        //    }

        //    foreach (var item in inCol)
        //    {
        //        var colOnTableName = FindTableName(item.Key, json.tables);
        //        foreach (var colItem in item.Value)
        //        {
        //            ICreateColumnOnTableSyntax colBuilder = Create.Column(colItem.name.Value);
        //            ICreateColumnAsTypeSyntax coOptionlBuilder = colBuilder.OnTable(colOnTableName);
        //            //coOptionlBuilder.AsString();
        //            ProcessType(colItem.type.Value, coOptionlBuilder);
        //        }
        //    }

        //    foreach (var item in inRel)
        //    {
        //        ICreateForeignKeyFromTableSyntax relationBuilder = Create.ForeignKey(item);
        //        string tableName = json.tables[item.tableId].Value;
        //        relationBuilder.FromTable(tableName).ForeignColumn("").ToTable("").PrimaryColumn("");
        //    }

        //}

        public override void Up()
        {
            var tableList = json.table;
            var col = json.col;
            var reference = json.reference;

            var builder = Create;
            foreach (var tableItem in tableList)
            { 
                ICreateTableWithColumnOrSchemaOrDescriptionSyntax tableBuilder = builder.Table(tableItem["name"].Value);

                foreach (var colItem in col[tableItem["name"].Value])
                {
                    ICreateTableColumnAsTypeSyntax column = tableBuilder.WithColumn(colItem.Name);
                    ProcessType(colItem.Value.Value, column);
                }
            }
        }

        private string FindTableName(string key, dynamic tables)
        {
            var tableList = tables as IEnumerable<dynamic>;
            return tableList.FirstOrDefault(x => x.id.Value == key).name.Value;
        }

        private static void ProcessType(string type, ICreateTableColumnAsTypeSyntax column)
        {
            switch (type)
            {
                case "short":
                case "int16":
                    column.AsInt16();
                    break;
                case "int":
                case "int32":
                    column.AsInt32();
                    break;
                case "datetime":
                    column.AsDateTime();
                    break;
                case "guid":
                    column.AsGuid();
                    break;
                case "string":
                default:
                    column.AsString();
                    break;
            }
        }
    }
}
