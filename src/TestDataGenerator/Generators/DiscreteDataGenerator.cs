using System;
using System.Collections;

namespace TestDataGenerator.Generators {
    public class DiscreteDataGenerator : DataGenerator {
        private readonly static Random RandomGenerator = new Random();
        private readonly IList data;

        public DiscreteDataGenerator(IList data) {
            this.data = data;
        }

        public override object Random() {
            var index = RandomGenerator.Next(data.Count);
            return data[index];
        }
    }
}
