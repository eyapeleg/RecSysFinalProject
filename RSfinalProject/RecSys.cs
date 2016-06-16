using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    class RecSys
    {

        private DataLoaderEngine dataLoaderEngine;
        private PredictionEngine predictionEngine;

        public RecSys() {
            dataLoaderEngine = new DataLoaderEngine();
        }

        public void Load(string sFileName, double dTrainSetSize)
        {
            Items data = dataLoaderEngine.Load(sFileName);
            predictionEngine = new PredictionEngine(data);   
        }

    }
}
