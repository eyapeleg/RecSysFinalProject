using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RSfinalProject
{
    public class DataLoaderEngine
    {
        private int datasetSize;

        public DataLoaderEngine()
        {
        }

        public Items Load(string sFileName)
        {
            ItemPairsCount seqPairs = new ItemPairsCount();
            ItemPairsCount allPairs = new ItemPairsCount();
            ItemsCounts seqCounts = new ItemsCounts();
            ItemsCounts allCounts = new ItemsCounts();

            Stopwatch timer = Stopwatch.StartNew();
            StreamReader objInput = new StreamReader(sFileName, Encoding.Default);
            while (!objInput.EndOfStream)
            {
                string[] seq = readNextSequence(objInput);
                populateSeqPairs(seqPairs, seq);
                populateAllPairs(allPairs, seq);
                populateSeqCounts(seqCounts, seq);
                populateAllCounts(allCounts, seq);
            }

            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            System.Console.WriteLine("Loading data was completed successfully\nExection Time: " + elapsed.ToString("mm':'ss':'fff"));

            //var blacklistedUsers = users.Where(user => user.GetRatedItems().Count < 5).Select(user => user.GetId()).ToList();
            //Console.WriteLine("blacklisted users size: {0}", blacklistedUsers.Count());
            //Console.WriteLine("dataset size before removing blacklisted users: {0}", users.Sum(user => user.GetRatedItems().Count()));
            //users.removeUsers(blacklistedUsers);

            //datasetSize = (int)users.Sum(user => user.GetRatedItems().Count());
            //Console.WriteLine("dataset size after removing blacklisted users: {0}", datasetSize);

            return new Items(allPairs,seqPairs,seqCounts,allCounts);
        }

        private static string[] readNextSequence(StreamReader objInput)
        {
            string delimiter = " ";
            string line = objInput.ReadLine();
            string[] split = System.Text.RegularExpressions.Regex.Split(line,delimiter, RegexOptions.None);
            return split.Where(item => item != "").ToArray();
        }

        private static void populateAllPairs(ItemPairsCount allPairs, string[] seq)
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

        private static void populateSeqPairs(ItemPairsCount seqPairs, string[] seq)
        {
            for (int i = 0; i < seq.Count() - 1; i++)
            {
                ItemPair pair = new ItemPair(seq[i], seq[i + 1]);
                seqPairs.addPair(pair);
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

        private void populateSeqCounts(ItemsCounts counts, string[] seq)
        {
            for (int i = 0; i < seq.Count()-1; i++)
            {
                counts.addItem(seq[i]);
            }
        }

        public int GetDataSetSize()
        {
            return datasetSize;
        }


    }
}
