using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class Items
    {
        private ItemPairsCount allPairs;
        private ItemPairsCount seqPairs;
        private ItemsCounts allCounts;
        private ItemsCounts seqCounts;

        public Items(ItemPairsCount allPairs, ItemPairsCount seqPairs, ItemsCounts seqCounts, ItemsCounts allCounts)
        {
            this.allPairs = allPairs;
            this.seqPairs = seqPairs;
            this.allCounts = allCounts;
            this.seqCounts = seqCounts;
        }

        public int getAllPairsCount(ItemPair itemPair)
        {
            return allPairs.getPairCount(itemPair);
        }

        public int getSeqPairsCount(ItemPair itemPair)
        {
            return seqPairs.getPairCount(itemPair);
        }

        public int getAllCount(string item)
        {
            return allCounts.getItemCount(item);
        }

        public int getSeqCount(string item)
        {
            return seqCounts.getItemCount(item);
        }

        public List<string> getItemsList()
        {
            return allCounts.getItemsList();
        }
    }
}
