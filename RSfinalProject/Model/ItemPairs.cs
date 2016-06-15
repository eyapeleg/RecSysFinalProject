using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class ItemPairs
    {
        private Dictionary<ItemPair, int> pairs;

        public ItemPairs()
        {
            pairs = new Dictionary<ItemPair, int>();
        }

        public void addPair(ItemPair pair)
        {
            if (!pairs.ContainsKey(pair))
            {
                pairs.Add(pair, 0);
            }
            pairs[pair]++;
        }

        public int getPairCount(ItemPair pair)
        {
            if (pairs.ContainsKey(pair))
                return pairs[pair];
            else
                return 0;
        }
    }
}
