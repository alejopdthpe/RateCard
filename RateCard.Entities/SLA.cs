using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.Entities
{
    public class SLA : BaseEntity
    {
        public string Description { get; set; }
        public double Value_Amnt { get; set; }
    }
}
