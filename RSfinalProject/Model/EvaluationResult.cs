using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class EvaluationResult
    {
        public int iteration;
        public PredictionMethod method;
        public int len;
        public double score;

        public EvaluationResult(PredictionMethod method, int len, double score)
        {
            this.method = method;
            this.len = len;
            this.score = score;
        }

    }
}
