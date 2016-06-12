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
        private ItemBasedEngine itemBasedEngine;
        private DataUtils dataUtils;

        private Users users;
        private Items items;
        private Users trainUsers;
        private Items trainItems;
        private Users validationUsers;
        private Items validationItems;
        private Users testUsers;
        private Items testItems;

        private int dsSize;

        public RecSys() {
            dataLoaderEngine = new DataLoaderEngine();
            dataUtils = new DataUtils();
        }

        public void Load(string sFileName, double dTrainSetSize)
        {
            Tuple<Pairs, Pairs> data = dataLoaderEngine.Load(sFileName);

            this.dsSize = dataLoaderEngine.GetDataSetSize();
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
