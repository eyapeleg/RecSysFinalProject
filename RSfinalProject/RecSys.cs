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

        public RecSys() {
            dataLoaderEngine = new DataLoaderEngine();
        }

        public void Load(string sFileName, double dTrainSetSize)
        {
            Items data = dataLoaderEngine.Load(sFileName);
            Console.WriteLine("Cp: {0}", data.getCp("1", "1"));
            Console.WriteLine("Cp: {0}", data.getSeq("1", "1"));
    
            
            //splittedData = dataUtils.Split(dTrainSetSize, dsSize, data, DatasetType.Test, DatasetType.Train);
            //
            //trainUsers = splittedData[DatasetType.Train].Item1;
            //trainItems = splittedData[DatasetType.Train].Item2;
            //testUsers = splittedData[DatasetType.Test].Item1;
            //testItems = splittedData[DatasetType.Test].Item2;
            //
            //double trainSize = Math.Round(dsSize * dTrainSetSize);
            //splittedData = dataUtils.Split(dTrainSetSize, trainSize, new Tuple<Users, Items>(this.trainUsers, this.trainItems), DatasetType.Validation, DatasetType.Train);
            //
            //trainUsers = splittedData[DatasetType.Train].Item1;
            //trainItems = splittedData[DatasetType.Train].Item2;
            //validationUsers = splittedData[DatasetType.Validation].Item1;
            //validationItems = splittedData[DatasetType.Validation].Item2;

            //calculate the overall average rating 
            //CalculateAverageRatingForTrainingSet();
        }

    }
}
