using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Instrument
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PeriodID { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Period Period { get; set; }
    }
}
