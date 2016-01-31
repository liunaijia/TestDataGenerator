using System.Collections.Generic;
using System.Linq;
using TestDataGenerator.Data;

namespace TestDataGenerator.Schema {
    public class DatabaseSchema {
        private readonly static IDictionary<string, TableSchema> Cache = new Dictionary<string, TableSchema>();
        private readonly IDatabase database;

        public DatabaseSchema(IDatabase database) {
            this.database = database;
        }

        public TableSchema GetByTable(string tableName) {
            var cacheKey = GetCacheKey(tableName);
            if (Cache.ContainsKey(cacheKey))
                return Cache[cacheKey];

            var tableSchema = LoadTableSchema(tableName);
            Cache.Add(cacheKey, tableSchema);
            return tableSchema;
        }

        private string GetCacheKey(string tableName) {
            return database.GetHashCode().ToString() + tableName;
        }

        private TableSchema LoadTableSchema(string tableName) {
            var sql = @"select a.Name, b.name as DataType,
                COLUMNPROPERTY(a.id,a.name,'PRECISION') as Length,
                a.IsNullable,
                e.text as DefaultValue
                FROM syscolumns a
                left join systypes b on a.xusertype=b.xusertype
                inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'
                left join syscomments e on a.cdefault=e.id
                where d.name = @tableName
                order by a.colorder";

            var tableSchema = new TableSchema();
            tableSchema.Name = tableName;
            tableSchema.Columns = database.ExecuteDataReader(sql, new { tableName }, ColumnSchema.GetByDataReader).ToArray();
            return tableSchema;
        }
    }
}
