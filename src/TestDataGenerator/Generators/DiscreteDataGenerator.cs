using System;
using System.Collections;
using System.Collections.Generic;

namespace TestDataGenerator.Generators {
    public class DiscreteDataGenerator : DataGenerator {
        private readonly static Random RandomGenerator = new Random();
        private static IDictionary<int, int> Indexes = new Dictionary<int, int>();
        private readonly IList data;

        public DiscreteDataGenerator(IList data) {
            this.data = data;

            var indexKey = GetIndexKey();
            if (!Indexes.ContainsKey(indexKey))
                Indexes.Add(indexKey, 0);
        }

        public override object Random() {
            var index = RandomGenerator.Next(data.Count);
            return data[index];
        }

        public override object Sequence() {
            var index = Indexes[GetIndexKey()];
            Indexes[GetIndexKey()] = index + 1;

            return data[index];
        }

        private int GetIndexKey() {
            return data.GetHashCode();
        }
    }
}
