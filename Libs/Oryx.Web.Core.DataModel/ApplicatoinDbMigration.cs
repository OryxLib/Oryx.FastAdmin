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
    public class ApplicatoinDbMigration : Migration
    {
        private dynamic json;
        private dynamic srcJson;
        public ApplicatoinDbMigration(dynamic _json, dynamic _srcJson)
        {
            json = _json;
            srcJson = _srcJson;
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

        public override void Up()
        {
            var tool = new DataJsonDiff();

            var inTable = tool.InCommingTables(json, srcJson);
            var outTable = tool.OutCommingTables(json, srcJson);

            var inCol = tool.InCommingColumns(json, srcJson);
            var outCol = tool.OutCommingColumn(json, srcJson);

            var inRel = tool.InCommingRelation(json, srcJson);
            var outRel = tool.OutCommingRelation(json, srcJson);

            var builder = Create;

            foreach (var item in inTable)
            {
                ICreateTableWithColumnOrSchemaOrDescriptionSyntax tableBuilder = builder.Table(item);
                tableBuilder.WithColumn("Id").AsGuid();
            }

            foreach (var item in inCol)
            {
                var colOnTableName = FindTableName(item.Key, json.tables);
                foreach (var colItem in item.Value)
                {
                    ICreateColumnOnTableSyntax colBuilder = Create.Column(colItem.name.Value);
                    ICreateColumnAsTypeSyntax coOptionlBuilder = colBuilder.OnTable(colOnTableName);
                    //coOptionlBuilder.AsString();
                    ProcessType(colItem.type.Value, coOptionlBuilder);
                }
            }

            foreach (var item in inRel)
            {
                ICreateForeignKeyFromTableSyntax relationBuilder = Create.ForeignKey(item);
                string tableName = json.tables[item.tableId].Value;
                relationBuilder.FromTable(tableName).ForeignColumn("").ToTable("").PrimaryColumn("");
            }

            var DelBuilder = Delete;

            foreach (var item in outTable)
            {
                DelBuilder.Table(item);
            }

            foreach (var item in outCol)
            {
                DelBuilder.Column("").FromTable("");
            }

            foreach (var item in outRel)
            {
                DelBuilder.ForeignKey("").OnTable("");
            }
        }

        public void Up12()
        {
            var tableList = json.table;
            var col = json.col;
            var reference = json.reference;

            var builder = Create;
            foreach (var tableItem in tableList)
            {
                ICreateTableWithColumnOrSchemaOrDescriptionSyntax tableBuilder = builder.Table(tableItem["name"].Value);

                //tableBuilder.WithColumn("Id").AsGuid().PrimaryKey();

                foreach (var colItem in col[tableItem["name"].Value])
                {
                    ICreateTableColumnAsTypeSyntax column = tableBuilder.WithColumn(colItem.Name);
                    //ProcessType(colItem.Value.ToString().ToLower(), column);

                }
            }
        }

        private string FindTableName(string key, dynamic tables)
        {
            var tableList = tables as IEnumerable<dynamic>;
            return tableList.FirstOrDefault(x => x.id.Value == key).name.Value;
        }

        private static void ProcessType(string type, ICreateColumnAsTypeSyntax column)
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
