using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Data.Fixtures {
    public abstract class DataFixtures {
        public abstract object Random();

        public static DataFixtures From(IDatabase database, string sql, object parameters = null) {
            var query = new DatabaseQueryWithCache(database);
            var result = query.Query(sql, parameters);
            return new DiscreteDataFixtures(result);
        }

        public static DataFixtures From(params object[] items) {
            return new DiscreteDataFixtures(items);
        }

        public static DataFixtures FromDateRange(DateTime start, DateTime end) {
            return new DateRangeDataFixtures(start, end);
        }

        public static DataFixtures FromDateRangeInDeltaDays(int deltaInDaysForStart = 0, int deltaInDaysForEnd = 0) {
            var now = DateTime.UtcNow;
            return new DateRangeDataFixtures(now.AddDays(deltaInDaysForStart), now.AddDays(deltaInDaysForEnd));
        }

        public static DataFixtures FromRegex(string pattern) {
            return new RegexDataFixtures(pattern);
        }
    }
}
