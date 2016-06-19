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
            recSys.runExperiment("data.csv", 100,10);
        }
    }
}
