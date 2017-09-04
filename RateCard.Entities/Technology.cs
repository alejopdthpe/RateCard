using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.Entities
{
    public class Technology : BaseTechnology
    {
    
        public double Monitoring { get; set; }
        public double Incidents { get; set; }
        public double Services { get; set; }
        public string Type { get; set; }
        public string Unit_Of_Measure { get; set; }

        public double Avg_TBs { get; set; }


    }
}
