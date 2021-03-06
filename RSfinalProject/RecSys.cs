﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    class RecSys
    {

        private DataLoaderEngine dataLoaderEngine;
        private PredictionEngine predictionEngine;
        private EvaluationEngine evaluationEngine;
        private DataUtils dataPreparationEngine;

        //private List<string[]> trainSet;
        //private List<string[]> testSet;
        //private List<string[]> data;

        public RecSys() {
            
            dataPreparationEngine = new DataUtils();
        }

        public List<string[]> Load(string sFileName)
        {
            DataLoaderEngine dataLoaderEngine = new DataLoaderEngine();
            return dataLoaderEngine.Load(sFileName);
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

        public void ComputeHitRatio(int iterationNum, int cvNum)
        {
            Stopwatch timer = Stopwatch.StartNew();

            evaluationEngine.ComputeHitRatio(iterationNum, cvNum);

            timer.Stop();
            TimeSpan elapsed = timer.Elapsed;
            Console.WriteLine("Compute Hit Ratio was completed successfully\nExection Time: " + elapsed.ToString("mm':'ss':'fff"));
        }

        public void runExperiment(string fileName, int numOfIterations, int numCrossValidation)
        {
            List<string[]> data = this.Load(fileName);
            for(int i=0; i< numOfIterations;i++)
            {
                List<string[]> allData = dataPreparationEngine.randomizeData(data);
                for (int j = 0; j < numCrossValidation; j++)
                {
                    Dictionary<DatasetType, List<string[]>> trainTestData = dataPreparationEngine.devideDataToTrainTestCV(allData,j,numCrossValidation);
                    predictionEngine = new PredictionEngine(trainTestData[DatasetType.Train]);
                    evaluationEngine = new EvaluationEngine(trainTestData[DatasetType.Test], predictionEngine);
                    TrainCpModel();
                    TrainSeqModel();
                    ComputeHitRatio(i , j);
                }
            }
        }
    }
}
