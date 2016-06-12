using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class Pairs
    {
        private Dictionary<Pair, int> pairs;

        public Pairs()
        {
            pairs = new Dictionary<Pair, int>();
        }

        public void addPair(Pair pair)
        {
            if (!pairs.ContainsKey(pair))
            {
                pairs.Add(pair, 0);
            }
            pairs[pair]++;
        }
    }
}
