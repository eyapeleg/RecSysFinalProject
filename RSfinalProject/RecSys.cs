using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSfinalProject.Engines;

namespace RSfinalProject
{
    class RecSys
    {

        private DataLoaderEngine dataLoaderEngine;
        private PredictionEngine predictionEngine;
        private EvaluationEngine evaluationEngine;
        private List<string[]> data;
        private List<string[]> trainSet;
        private List<string[]> testSet;

        public RecSys() {
            dataLoaderEngine = new DataLoaderEngine();
        }

        public List<string[]> Load(string sFileName, double dTrainSetSize)
        {
            return dataLoaderEngine.Load(sFileName);
            this.predictionEngine = new PredictionEngine(trainSet);
            this.evaluationEngine = new EvaluationEngine(testSet, predictionEngine);
        }


        public void TrainCpModel()
        {
            Stopwatch timer = Stopwatch.StartNew();

            predictionEngine.TrainCpModel();

            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("Train Cp Model was completed successfully\nExection Time: " + elapsed.ToString("mm':'ss':'fff"));
        }

        public void TrainSeqModel()
        {
            Stopwatch timer = Stopwatch.StartNew();

            predictionEngine.TrainSeqModel();

            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("Train Seq Model was completed successfully\nExection Time: " + elapsed.ToString("mm':'ss':'fff"));
        }

        public void ComputeHitRatio()
        {
            Stopwatch timer = Stopwatch.StartNew();

            evaluationEngine.ComputeHitRatio();

            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("Compute Hit Ratio was completed successfully\nExection Time: " + elapsed.ToString("mm':'ss':'fff"));
        }
    }
}
