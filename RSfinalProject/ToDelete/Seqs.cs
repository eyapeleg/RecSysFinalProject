using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    class Seqs
    {
        Dictionary<Seq,int> seqs;

        public Seqs()
        {
            this.seqs = new Dictionary<Seq,int>();
        }

        public void addSeq(Seq seq)
        {
            if (!seqs.ContainsKey(seq))
                seqs.Add(seq, 0);

            seqs[seq]++;
        }
    }
}
