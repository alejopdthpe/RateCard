using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCard.Entities
{
    public class TableRow : BaseEntity
    {
        public string Detail { get; set; }
        public double[] ValueAmounts { get; set; }
    }
}
