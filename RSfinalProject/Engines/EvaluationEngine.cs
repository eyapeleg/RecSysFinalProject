using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject.Engines
{
    public class EvaluationEngine
    {
        private List<string[]> testSet;
        private PredictionEngine predictionEngine;
        public enum Method
        {
            Cp, Seq
        }

        public EvaluationEngine(List<string[]> testSet, PredictionEngine predictionEngine)
        {
            this.testSet = testSet;
            this.predictionEngine = predictionEngine;
        }

        public void ComputeHitRatio()
        {
            Random rnd = new Random();
            ConcurrentDictionary<int, ConcurrentDictionary<Method, double>> ans = new ConcurrentDictionary<int, ConcurrentDictionary<Method, double>>();
            double count = 0;

            ans.TryAdd(1, new ConcurrentDictionary<Method, double>());
            ans.TryAdd(5, new ConcurrentDictionary<Method, double>());
            ans.TryAdd(10, new ConcurrentDictionary<Method, double>());
            foreach (var len in ans.Keys)
            {
                ans[len].TryAdd(Method.Cp, 0);
                ans[len].TryAdd(Method.Seq, 0);
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

            
            foreach (var len in ans.Keys)
            {
                foreach (var method in ans[len].Keys)
                {
                    ans[len][method] = ans[len][method]/count;
                    Console.WriteLine("Method: {0}, @Hit{1}: {2}", method, len, ans[len][method]);
                }
            }
        }
    }
}
