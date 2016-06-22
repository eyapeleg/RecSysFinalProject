using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace RSfinalProject
{
    public class EvaluationEngine
    {
        private List<string[]> testSet;
        private PredictionEngine predictionEngine;
        private const string filePath = "results.csv";

        public EvaluationEngine(List<string[]> testSet, PredictionEngine predictionEngine)
        {
            this.testSet = testSet;
            this.predictionEngine = predictionEngine;
        }

        public void ComputeHitRatio(int iterationNum, int cvNum)
        {
            HashSet<EvaluationResult> evaluationResults = new HashSet<EvaluationResult>();
            Random rnd = new Random();
            ConcurrentDictionary<int, ConcurrentDictionary<PredictionMethod, double>> ans = new ConcurrentDictionary<int, ConcurrentDictionary<PredictionMethod, double>>();
            double count = 0;

            ans.TryAdd(1, new ConcurrentDictionary<PredictionMethod, double>());
            ans.TryAdd(5, new ConcurrentDictionary<PredictionMethod, double>());
            ans.TryAdd(10, new ConcurrentDictionary<PredictionMethod, double>());
            foreach (var len in ans.Keys)
            {
                ans[len].TryAdd(PredictionMethod.Cp, 0);
                ans[len].TryAdd(PredictionMethod.Seq, 0);
            }

            foreach (var session in testSet)
            {
                var givenItems = new List<string>();
                var size = session.Length;

                var rndIndex = rnd.Next(0, size - 2);
                string itemToPredict = session[rndIndex+1];

                for (int i = 0; i <= rndIndex; i++)
                {
                    givenItems.Add(session[i]);
                }

                foreach (var len in ans.Keys)
                {
                    foreach (var method in ans[len].Keys)
                    {
                        var results = predictionEngine.getProbability(givenItems, method);

                        List<string> predictedItems = results.Take(len).Select(c => c.Key).ToList();
                        if (predictedItems.Contains(itemToPredict))
                        {
                            ans[len][method] += 1;
                        }
                    }
                }
                count++;
            }

            var stringBuilder = new StringBuilder();
            foreach (var len in ans.Keys)
            {
                foreach (var method in ans[len].Keys)
                {
                    double result = ((double)ans[len][method]) / (double)count;
                    var newLine = string.Format("{0},{1},{2},{3},{4}", iterationNum,cvNum,method,len,result);
                    stringBuilder.AppendLine(newLine);
                }
            }
            File.AppendAllText(filePath, stringBuilder.ToString());
        }
    }
}
