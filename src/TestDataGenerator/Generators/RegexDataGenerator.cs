using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegExHelper;

namespace TestDataGenerator.Generators {
    public class RegexDataGenerator : DataGenerator {
        private readonly RegExpDataGenerator regexGenerator;

        public RegexDataGenerator(string pattern) {
            regexGenerator = new RegExpDataGenerator(pattern);
        }

        public override object Random() {
            return regexGenerator.Next();
        }
    }
}
