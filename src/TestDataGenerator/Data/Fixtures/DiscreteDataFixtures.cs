using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Data.Fixtures {
    public class DiscreteDataFixtures : DataFixtures {
        private readonly static Random RandomGenerator = new Random();
        private readonly IList data;

        public DiscreteDataFixtures(IList data) {
            this.data = data;
        }

        public override object Random() {
            var index = RandomGenerator.Next(data.Count);
            return data[index];
        }
    }
}
