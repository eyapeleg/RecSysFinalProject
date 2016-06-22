using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RSfinalProject
{
    public class DataUtils
    {
        public Dictionary<DatasetType, List<string[]>> devideDataToTrainTestRandomly(List<string[]> data, double dTrainSetSize)
        {
            Dictionary<DatasetType, List<string[]>> dataset = new Dictionary<DatasetType, List<string[]>>();
            dataset.Add(DatasetType.Test, new List<string[]>());
            dataset.Add(DatasetType.Train, new List<string[]>());

            Random rnd = new Random();
            Stopwatch timer = Stopwatch.StartNew();

            foreach (string[] seq in data)
            {
                if (rnd.NextDouble() <= dTrainSetSize)
                {
                    dataset[DatasetType.Train].Add(seq);
                }
                else
                {
                    dataset[DatasetType.Test].Add(seq);
                }
            }
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("Data Preparation was finished successfully \nExection Time: " + elapsed.ToString("mm':'ss':'fff"));

            return dataset;
        }

        public Dictionary<DatasetType, List<string[]>> devideDataToTrainTestCV(List<string[]> data, int foldNumber,int  foldTotal)
        {
            if (foldNumber >= foldTotal)
                throw new ArgumentException();

            Dictionary<DatasetType, List<string[]>> dataset = new Dictionary<DatasetType, List<string[]>>();
            dataset.Add(DatasetType.Test, new List<string[]>());
            dataset.Add(DatasetType.Train, new List<string[]>());

            int counter = 0;
            int numOfInstances = data.Count();
            double startIndex = ((double)foldNumber/(double)foldTotal)*numOfInstances;
            double endIndex = ((double)(foldNumber + 1) / (double)foldTotal) * numOfInstances;

            Stopwatch timer = Stopwatch.StartNew();

            foreach (string[] seq in data)
            {
                if (counter>=startIndex && counter<endIndex)
                {
                    dataset[DatasetType.Test].Add(seq);
                }
                else
                {
                    dataset[DatasetType.Train].Add(seq);
                }
                counter++;
            }
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("devideDataToTrainTestCV was finished successfully \nExection Time: " + elapsed.ToString("mm':'ss':'fff"));

            return dataset;
        }

        public List<string[]> randomizeData(List<string[]> data)
        {
            Random rnd = new Random();
            return data.OrderBy(item => rnd.NextDouble()).ToList();
        }
    }
}

