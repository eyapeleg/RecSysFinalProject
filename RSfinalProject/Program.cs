using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    class Program
    {

        static void Main(string[] args)
        {
            RecSys recSys = new RecSys();
            recSys.Load("data.csv", 0.99);
            recSys.TrainCpModel();
            recSys.TrainSeqModel();
            recSys.ComputeHitRatio();
            Console.ReadLine();
        }
    }
}
