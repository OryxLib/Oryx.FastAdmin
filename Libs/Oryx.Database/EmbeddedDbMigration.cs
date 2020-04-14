using FluentMigrator;
using Oryx.Database.DbSchema;
using Oryx.Utilities.ValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Database
{
    public class EmbeddedDbMigration : Migration
    {
        private readonly List<DataTable> dataTables;
        public EmbeddedDbMigration(List<DataTable> _dataTables)
        {
            dataTables = _dataTables;
            ApplicationContext = null;
        }
        public override void Down()
        {

        }

        public override void Up()
        {
            var builder = Create;
            foreach (var dataTable in dataTables)
            {
                var tableBuilder = builder.Table(dataTable.Name);

                //PrimaryKey

                tableBuilder.WithColumn("Id").AsGuid().PrimaryKey();

                foreach (var dataTableItem in dataTable.DataTableItem)
                {
                    var column = tableBuilder.WithColumn(dataTableItem.Name);
                    var columnBuilder = column.AsAnsiString();

                    if (dataTableItem.Index)
                    {
                        columnBuilder = columnBuilder.Indexed();
                    }
                    if (dataTableItem.Nullable)
                    {
                        columnBuilder = columnBuilder.Nullable();
                    }
                    else
                    {
                        columnBuilder = columnBuilder.NotNullable();
                    }

                    if (dataTableItem.DefaultValue.IsTrue())
                    {
                        columnBuilder = columnBuilder.WithDefaultValue(dataTableItem.DefaultValue);
                    }
                    if (dataTableItem.Unique)
                    {
                        columnBuilder = columnBuilder.Unique();
                    }
                    if (dataTableItem.ForeignKey.Key.IsTrue() && dataTableItem.ForeignKey.Value.IsTrue())
                    {
                        columnBuilder = columnBuilder.ForeignKey();
                        columnBuilder.ReferencedBy(dataTableItem.ForeignKey.Key, dataTableItem.ForeignKey.Value);
                    }
                }
                //SoftDelete
                if (dataTable.SoftDelete)
                {
                    var column = tableBuilder.WithColumn("softDelete");
                    column.AsBoolean();
                }
                //TimeStamps
                if (dataTable.TimeStamp)
                {
                    var column = tableBuilder.WithColumn("TimeStamp");
                    column.AsInt64();
                }
            }
           
        }
    }
}
