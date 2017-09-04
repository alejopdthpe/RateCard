using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCard.Entities
{
    public class Deal : BaseEntity
    {
        public string Location { get; set; }
        public string SLA { get; set; }
        public string RegionAOH { get; set; }
        public double TargetMargin { get; set; }
    }
}
