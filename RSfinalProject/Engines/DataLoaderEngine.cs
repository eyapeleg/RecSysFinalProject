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

        public List<string[]> Load(string sFileName)
        {
            List<string[]> dataset = new List<string[]>();
           
            StreamReader objInput = new StreamReader(sFileName, Encoding.Default);
            Stopwatch timer = Stopwatch.StartNew();
            while (!objInput.EndOfStream)
            {
                string[] seq = readNextSequence(objInput);
                dataset.Add(seq);
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
