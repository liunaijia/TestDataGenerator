using System;

namespace TestDataGenerator.Generators {
    public class DateRangeDataGenerator : DataGenerator {
        private readonly static Random RandomGenerator = new Random();
        private readonly DateTime start;
        private readonly DateTime end;

        public DateRangeDataGenerator(DateTime start, DateTime end) {
            this.start = start;
            this.end = end;
        }

        public override object Random() {
            var distance = (end - start).TotalSeconds;
            return start.AddSeconds(distance * RandomGenerator.NextDouble());
        }
    }
}
