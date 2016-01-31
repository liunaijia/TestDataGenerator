using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Data.Fixtures {
    public class DatabaseQueryWithCache {
        private static readonly IDictionary<string, IList> Cache = new Dictionary<string, IList>();
        private readonly IDatabase database;

        public DatabaseQueryWithCache(IDatabase database) {
            this.database = database;
        }

        public IList Query(string sql, object parameters = null) {
            var result = LookupCache(database, sql, parameters);
            if (result == null) {
                result = database.ExecuteDataReader(sql, parameters, dr => dr.GetValue(0)).ToList();
                PutIntoCache(database, sql, parameters, result);
            }

            return result;
        }

        private static void PutIntoCache(IDatabase database, string sql, object parameters, IList result) {
            var key = GetCacheKey(database, sql, parameters);
            Cache.Add(key, result);
        }

        private static IList LookupCache(IDatabase database, string sql, object parameters) {
            var key = GetCacheKey(database, sql, parameters);
            if (Cache.ContainsKey(key))
                return Cache[key];

            return null;
        }

        private static string GetCacheKey(IDatabase database, string sql, object parameters) {
            return JsonConvert.SerializeObject(new {
                database = database.GetHashCode(),
                sql,
                parameters
            });
        }
    }
}
