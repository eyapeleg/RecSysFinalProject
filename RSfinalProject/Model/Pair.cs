using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class Pair 
    {
        private Tuple<string, string> pair;

        public Pair(string item1, string item2)
        {
            this.pair = Tuple.Create(item1, item2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Name.Equals(obj.GetType().Name))
                return false;

            if (obj == this)
                return true;

            if (this.pair.Equals(((Pair)obj).pair))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return this.pair.GetHashCode();
        }
    }
}
