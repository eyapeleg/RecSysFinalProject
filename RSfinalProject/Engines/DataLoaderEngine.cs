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
        DataUtils dataUtils = new DataUtils();

        public DataLoaderEngine()
        {
        }

        public Tuple<Pairs,Pairs> Load(string sFileName)
        {
            Pairs seqPairs = new Pairs();
            Pairs allPairs = new Pairs();

            Stopwatch timer = Stopwatch.StartNew();
            StreamReader objInput = new StreamReader(sFileName, Encoding.Default);
            while (!objInput.EndOfStream)
            {
                string[] seq = readNextSequence(objInput);
                populateSeqPairs(seqPairs, seq);
                populateAllPairs(allPairs, seq);    
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

            return new Tuple<Pairs,Pairs>(allPairs,seqPairs);
        }

        private static string[] readNextSequence(StreamReader objInput)
        {
            string delimiter = " ";
            string line = objInput.ReadLine();
            string[] split = System.Text.RegularExpressions.Regex.Split(line,delimiter, RegexOptions.None);
            return split.Where(item => item != "").ToArray();
        }

        private static void populateAllPairs(Pairs allPairs, string[] split)
        {
            for (int i = 0; i < split.Count() - 1; i++)
            {
                for (int j = i + 1; j < split.Count(); j++)
                {
                    Pair pair = split[i].CompareTo(split[j]) < 0 ? new Pair(split[i], split[j]) : new Pair(split[j], split[i]);
                    allPairs.addPair(pair);
                }
            }
        }

        private static void populateSeqPairs(Pairs seqPairs, string[] split)
        {
            for (int i = 0; i < split.Count() - 1; i++)
            {
                Pair pair = new Pair(split[i], split[i + 1]);
                seqPairs.addPair(pair);
            }
        }

        public int GetDataSetSize()
        {
            return datasetSize;
        }


    }
}
