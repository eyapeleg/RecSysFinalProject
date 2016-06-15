using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class Items
    {
        private ItemPairs allPairs;
        private ItemPairs seqPairs;
        private ItemCounts allCounts;
        private ItemCounts seqCounts;

        public Items(ItemPairs allPairs, ItemPairs seqPairs, ItemCounts seqCounts, ItemCounts allCounts)
        {
            this.allPairs = allPairs;
            this.seqPairs = seqPairs;
            this.allCounts = allCounts;
            this.seqCounts = seqCounts;
        }

        public double getCp(string expectedItem, string givenItem)
        {
            ItemPair pair = givenItem.CompareTo(expectedItem) < 0 ? new ItemPair(givenItem, expectedItem) : new ItemPair(expectedItem, givenItem);
            double pairCount = allPairs.getPairCount(pair);
            double givenItemCount = allCounts.getItemCount(givenItem);
            return (pairCount / givenItemCount) ;
        }

        public double getSeq(string expectedItem, string givenItem)
        {
            ItemPair pair = new ItemPair(givenItem, expectedItem);
            double seqCount = seqPairs.getPairCount(pair);
            double givenItemCount = seqCounts.getItemCount(givenItem);
            return (seqCount / givenItemCount);
        }

    }
}
