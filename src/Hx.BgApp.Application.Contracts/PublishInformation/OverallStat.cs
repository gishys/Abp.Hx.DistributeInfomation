using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.BgApp.PublishInformation
{
    public class OverallStat
    {
        public long HouseholdCount { get; set; }
        public long EvaluationsTotalTimes { get; set; }
        public double EvaluationsAverage { get; set; }
        public long InitiateEvaluations { get; set; }
    }
}
