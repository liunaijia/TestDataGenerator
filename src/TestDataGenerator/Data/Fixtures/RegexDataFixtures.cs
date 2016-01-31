using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegExHelper;

namespace DataProducer.Data.Fixtures {
    public class RegexDataFixtures : DataFixtures {
        private readonly RegExpDataGenerator regexGenerator;

        public RegexDataFixtures(string pattern) {
            regexGenerator = new RegExpDataGenerator(pattern);
        }

        public override object Random() {
            return regexGenerator.Next();
        }
    }
}
