using System.Data;

namespace TestDataGenerator.Schema {
    public class TableSchema {
        public string Name { get; set; }

        public ColumnSchema[] Columns { get; set; }
        
        public DataTable CreateDataTableStructure() {
            var table = new DataTable(Name);

            foreach (var column in Columns) {
                table.Columns.Add(column.Name, column.GetDataType());
            }

            return table;
        }
    }
}
