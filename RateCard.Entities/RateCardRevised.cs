using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.Entities
{
    public class RateCardRevised : BaseEntity
    {
        public string Part_Number { get; set; }
        public string Description { get; set; }
        public double List_Price { get; set; }
        public string Mou_Disc_Rate { get; set; }
        public int Years { get; set; }

    }
}
