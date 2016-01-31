using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataProducer.Data;
using DataProducer.Data.Schema;

namespace DataProducer
{
    public class BuildResult<T>
    {
        private readonly Func<T> builder;
        private readonly int numbers;

        public BuildResult(Func<T> builder, int numbers)
        {
            this.builder = builder;
            this.numbers = numbers;
        }

        public void InTable(IDatabase database, string tableName)
        {
            var table = CreateDataTableStructure(database, tableName);

            CreateRows(table);

            database.BulkCopy(table);
            //foreach (var batch in Chunkify(data, 1000))
            //{
            //    AddRows(table, batch);
            //    database.BulkCopy(table);
            //}
        }

        private void CreateRows(DataTable table)
        {
            foreach (var i in Enumerable.Range(0, numbers))
            {
                var item = builder();
                var row = table.NewRow();
                foreach (var prop in item.GetType().GetProperties())
                {
                    row.SetField(prop.Name, prop.GetValue(item));
                }
                table.Rows.Add(row);
            }
        }

        private DataTable CreateDataTableStructure(IDatabase database, string tableName)
        {
            var schema = new DatabaseSchema(database);
            var table = schema.GetByTable(tableName).CreateDataTableStructure();
            return table;
        }

        //public IEnumerable<IList<T>> Chunkify(IEnumerable<T> source, int size)
        //{
        //    if (source == null)
        //        throw new ArgumentNullException("source");
        //    if (size < 1)
        //        throw new ArgumentOutOfRangeException("size");

        //    using (var iter = source.GetEnumerator())
        //    {
        //        while (iter.MoveNext())
        //        {
        //            var chunk = new List<T>();
        //            chunk.Add(iter.Current);
        //            for (int i = 1; i < size && iter.MoveNext(); i++)
        //            {
        //                chunk.Add(iter.Current);
        //            }
        //            yield return chunk;
        //        }
        //    }
        //}
    }

    public static class FuncExtensions
    {
        public static BuildResult<T> Build<T>(this Func<T> builder, int numbers)
        {
            return new BuildResult<T>(builder, numbers);
        }
    }
}
