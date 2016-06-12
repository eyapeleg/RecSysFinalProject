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
            recSys.Load("data.seq", 0.95);
        }
    }
}
