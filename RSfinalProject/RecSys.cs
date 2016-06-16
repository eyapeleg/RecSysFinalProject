using System;
using System.Collections.Generic;
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
        private List<string[]> trainSet;
        private List<string[]> testSet;

        public RecSys() {
            dataLoaderEngine = new DataLoaderEngine();
        }

        public void Load(string sFileName, double dTrainSetSize)
        {
            Dictionary<DatasetType, List<string[]>> data = dataLoaderEngine.Load(sFileName, dTrainSetSize);
            trainSet = data[DatasetType.Train];
            testSet = data[DatasetType.Test];

            this.predictionEngine = new PredictionEngine(trainSet);
            this.evaluationEngine = new EvaluationEngine(testSet, predictionEngine);
        }

        public void TrainCpModel()
        {
            predictionEngine.TrainCpModel();
        }

        public void TrainSeqModel()
        {
            predictionEngine.TrainSeqModel();
        }

        public void ComputeHitRatio()
        {
            evaluationEngine.ComputeHitRatio();
        }
    }
}
