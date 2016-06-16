using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace RSfinalProject
{
    public class PredictionEngine
    {
        private Items items;
        private int MIN_NUMBER_OF_USERS = 25;
        private double PROBABILITY_THRESHOLD = 0.30;
        private int MAX_NUM_RESULTS = 50;

        public PredictionEngine(Items items)
        {
            this.items = items;
        }

        public List<KeyValuePair<string, double>> getConditionalProbability(List<string> givenItems)
        {
            return getProbabilities(givenItems, cp);
        }

        public List<KeyValuePair<string, double>> getJaccardProbability(List<string> givenItems)
        {
            return getProbabilities(givenItems, seq);
        }

        private List<KeyValuePair<string, double>> getProbabilities(List<string> givenItems, Func<string, string, double> probabilityFunction)
        {

            ConcurrentDictionary<string, double> probabilities = new ConcurrentDictionary<string, double>(); ;
            int positiveProbabilitiesCount = 0;

            Parallel.ForEach(items.getItemsList(), (item, loopState) =>
            {
                if (positiveProbabilitiesCount > MAX_NUM_RESULTS)
                {
                    loopState.Break();
                }
                if (itemValid(givenItems, item))
                {
                    double itemProbability = calculateProbability(givenItems, probabilityFunction, item);
                    if (itemProbability > PROBABILITY_THRESHOLD)
                    {
                        Interlocked.Increment(ref positiveProbabilitiesCount);
                    }
                    probabilities.TryAdd(item, itemProbability);
                }
            });

            return probabilities
                .OrderByDescending(kv => kv.Value)
                .ToList();
        }

        private double calculateProbability(List<string> givenItems, Func<string, string, double> probabilityFunction, string item)
        {
            return givenItems
                .Select(givenItem => probabilityFunction(item, givenItem))
                .Max();
        }

        private bool itemValid(List<string> givenItems, string item)
        {
            return items.getAllCount(item) > MIN_NUMBER_OF_USERS && !givenItems.Contains(item);
        }

        private double cp(string expectedItem, string givenItem)
        {
            ItemPair pair = givenItem.CompareTo(expectedItem) < 0 ? new ItemPair(givenItem, expectedItem) : new ItemPair(expectedItem, givenItem);
            double pairCount = items.getAllPairsCount(pair);
            double givenItemCount = items.getAllCount(givenItem);
            return (pairCount / givenItemCount);
        }

        public double seq(string expectedItem, string givenItem)
        {
            ItemPair pair = new ItemPair(givenItem, expectedItem);
            double seqCount = items.getSeqPairsCount(pair);
            double givenItemCount = items.getSeqCount(givenItem);
            return (seqCount / givenItemCount);
        }
    }
}
