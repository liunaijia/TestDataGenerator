using System;
using TestDataGenerator.Data;

namespace TestDataGenerator.Generators {
    public abstract class DataGenerator {
        public abstract object Random();

        public static DataGenerator From(IDatabase database, string sql, object parameters = null) {
            var query = new DatabaseQueryWithCache(database);
            var result = query.Query(sql, parameters);
            return new DiscreteDataGenerator(result);
        }

        public static DataGenerator From(params object[] items) {
            return new DiscreteDataGenerator(items);
        }

        public static DataGenerator FromDateRange(DateTime start, DateTime end) {
            return new DateRangeDataGenerator(start, end);
        }

        public static DataGenerator FromDateRangeInDeltaDays(int deltaInDaysForStart = 0, int deltaInDaysForEnd = 0) {
            var now = DateTime.UtcNow;
            return new DateRangeDataGenerator(now.AddDays(deltaInDaysForStart), now.AddDays(deltaInDaysForEnd));
        }

        public static DataGenerator FromRegex(string pattern) {
            return new RegexDataGenerator(pattern);
        }
    }
}
