using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.Entities
{
    public class TSSOfferPrice : BaseEntity
    {
        public double ANNUAL_TOTAL_LIST_COST_MIN { get; set; }
        public double SAW { get; set; }
        public double SAAS { get; set; }
    }
}
