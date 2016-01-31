using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Data.Fixtures {
    public class DateRangeDataFixtures : DataFixtures {
        private readonly static Random RandomGenerator = new Random();
        private readonly DateTime start;
        private readonly DateTime end;

        public DateRangeDataFixtures(DateTime start, DateTime end) {
            this.start = start;
            this.end = end;
        }

        public override object Random() {
            var distance = (end - start).TotalSeconds;
            return start.AddSeconds(distance * RandomGenerator.NextDouble());
        }
    }
}
