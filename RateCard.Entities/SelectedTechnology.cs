using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.Entities
{
    public class SelectedTechnology : BaseTechnology
    {
        public int Quantity { get; set; }
        public bool HasMonitoring { get; set; }
        public bool HasOperAndAdmin { get; set; }
    }
}
