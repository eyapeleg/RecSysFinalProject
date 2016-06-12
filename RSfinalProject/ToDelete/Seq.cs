using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    class Seq
    {
        HashSet<Pair> pairs;

        public Seq()
        {
            pairs = new HashSet<Pair>();
        }

        public void addPair(Pair pair){
                this.pairs.Add(pair);
        }
    }
}
