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
        private Items items = new Items();
        private List<string[]> trainSet;
        private int MIN_NUM_APPEARENCES = 15;
        private double PROBABILITY_THRESHOLD = 0.30;
        private int MAX_NUM_RESULTS = 10;

        public PredictionEngine(List<string[]> trainSet)
        {
            this.trainSet = trainSet;
        }

        public List<KeyValuePair<string, double>> getConditionalProbability(List<string> givenItems)
        {
            return getProbabilities(givenItems, cp);
        }

        public List<KeyValuePair<string, double>> getSequenceProbability(List<string> givenItems)
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
            return items.getAllCount(item) > MIN_NUM_APPEARENCES && !givenItems.Contains(item);
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

        public void TrainCpModel()
        {
            ItemPairsCount allPairs = new ItemPairsCount();
            ItemsCounts allCounts = new ItemsCounts();

            if (trainSet == null)
            {
                throw new NullReferenceException("train set must be initialize first");
            }

            foreach (var session in trainSet)
            {
                populateAllPairs(allPairs, session);
                populateAllCounts(allCounts, session);
            }

            items.addAllPairs(allPairs);
            items.addAllCount(allCounts);
        }

        public void TrainSeqModel()
        {
            ItemPairsCount seqPairs = new ItemPairsCount();
            ItemsCounts seqCounts = new ItemsCounts();

            if (trainSet == null)
            {
                throw new NullReferenceException("train set must be initialize first");
            }

            foreach (var session in trainSet)
            {
                populateSeqPairs(seqPairs, session);
                populateSeqCounts(seqCounts, session);
            }

            items.addSeqPairs(seqPairs);
            items.addSeqCounts(seqCounts);
        }

        #region private 
        private void populateAllPairs(ItemPairsCount allPairs, string[] seq)
        {
            string[] distinctSeq = seq.Distinct().ToArray();
            for (int i = 0; i < distinctSeq.Count() - 1; i++)
            {
                for (int j = i + 1; j < distinctSeq.Count(); j++)
                {
                    ItemPair pair = seq[i].CompareTo(seq[j]) < 0 ? new ItemPair(seq[i], seq[j]) : new ItemPair(seq[j], seq[i]);
                    allPairs.addPair(pair);
                }
            }
        }

        private void populateAllCounts(ItemsCounts counts, string[] seq)
        {
            string[] distinctSeq = seq.Distinct().ToArray();
            for (int i = 0; i < distinctSeq.Count(); i++)
            {
                counts.addItem(seq[i]);
            }
        }

        private void populateSeqPairs(ItemPairsCount seqPairs, string[] seq)
        {
            for (int i = 0; i < seq.Count() - 1; i++)
            {
                ItemPair pair = new ItemPair(seq[i], seq[i + 1]);
                seqPairs.addPair(pair);
            }
        }



        private void populateSeqCounts(ItemsCounts counts, string[] seq)
        {
            for (int i = 0; i < seq.Count() - 1; i++)
            {
                counts.addItem(seq[i]);
            }
        }
        #endregion

        public List<KeyValuePair<string, double>> getProbability(List<string> givenItems, PredictionMethod method)
        {
            if (method == PredictionMethod.Cp)
            {
                return getConditionalProbability(givenItems);
            }

            return getSequenceProbability(new List<string> { givenItems.Last() });
        }
    }
}
