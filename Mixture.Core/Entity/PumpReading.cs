using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Core.Entity
{
    public class PumpReading
    {
        public int Id { get; set; }
        public string PumpId { get; set; } // رقم المضخة
        public double Weight { get; set; } // الوزن
        public int FeedTypeId { get; set; } // رقم العلف
    }
}
