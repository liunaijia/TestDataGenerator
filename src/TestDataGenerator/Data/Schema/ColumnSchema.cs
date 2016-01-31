using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Data.Schema {
    public class ColumnSchema {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public bool IsNullable { get; set; }
        public string DefaultValue { get; set; }

        public static ColumnSchema GetByDataReader(DbDataReader dr) {
            return new ColumnSchema {
                Name = (string)dr["Name"],
                DataType = (string)dr["DataType"],
                Length = (int)dr["Length"],
                IsNullable = (int)dr["IsNullable"] == 1,
                DefaultValue = dr["DefaultValue"] != DBNull.Value ? (string)dr["DefaultValue"] : null
            };
        }

        public Type GetDataType() {
            switch (DataType) {
                case "uniqueidentifier":
                    return typeof(Guid);
                case "nvarchar":
                case "varchar":
                    return typeof(string);
                case "int":
                    return typeof(int);
                case "bigint":
                    return typeof(long);
                case "datetime":
                    return typeof(DateTime);
                default:
                    throw new ArgumentException(string.Format("unknow column data type '{0}'", DataType));
            }
        }
    }
}
