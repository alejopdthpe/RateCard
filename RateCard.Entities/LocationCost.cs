using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.Entities
{
    public class LocationCost : BaseEntity
    {
        public string Country { get; set; }
        public double L1 { get; set; }
        public double L2 { get; set; }
        public double L3 { get; set; }
        public double L4 { get; set; }
        public double Inflation_Rate { get; set; }
        public double Efficiency { get; set; }
        public double Incremental_Rate { get; set; }
        public double Decrease_Rate { get; set; }

      
        public LocationCost() { }

        public LocationCost(string country, double l1, double l2, double l3, double l4, double inflation_Rate, double efficiency, double incremental_Rate, double decrease_Rate)
        {
            Country = country;
            L1 = l1;
            L2 = l2;
            L3 = l3;
            L4 = l4;
            Inflation_Rate = inflation_Rate;
            Efficiency = efficiency;
            Incremental_Rate = incremental_Rate;
            Decrease_Rate = decrease_Rate;
        }
    }
}
