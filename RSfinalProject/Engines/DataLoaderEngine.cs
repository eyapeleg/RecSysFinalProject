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

        public Dictionary<DatasetType, List<string[]>> Load(string sFileName, double dTrainSetSize)
        {
            Dictionary<DatasetType, List<string[]>> dataset = new Dictionary<DatasetType, List<string[]>>();
            dataset.Add(DatasetType.Test, new List<string[]>());
            dataset.Add(DatasetType.Train, new List<string[]>());

            Random rnd = new Random();
            StreamReader objInput = new StreamReader(sFileName, Encoding.Default);
            Stopwatch timer = Stopwatch.StartNew();
            while (!objInput.EndOfStream)
            {
                string[] seq = readNextSequence(objInput);
                if (rnd.NextDouble() <= dTrainSetSize)
                {
                    dataset[DatasetType.Train].Add(seq);
                }
                else
                {
                    dataset[DatasetType.Test].Add(seq);
                }
            }

            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("Loading data was completed successfully\nExection Time: " + elapsed.ToString("mm':'ss':'fff"));

            return dataset;
        }

        private static string[] readNextSequence(StreamReader objInput)
        {
            string delimiter = ";";
            string line = objInput.ReadLine();
            string[] split = System.Text.RegularExpressions.Regex.Split(line,delimiter, RegexOptions.None);
            return split.Where(item => item != "").ToArray();
        }

        public int GetDataSetSize()
        {
            return datasetSize;
        }


    }
}
