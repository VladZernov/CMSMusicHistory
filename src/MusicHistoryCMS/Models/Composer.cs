using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Composer
    {
        public Composer()
        {
            Compositions = new HashSet<Composition>();
        }

        public int ID { get; set; }
        public int? BornYear { get; set; }
        public int? DieYear { get; set; }
        public string Name { get; set; }
        public int PeriodID { get; set; }

        public virtual ICollection<Composition> Compositions { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Period Period { get; set; }
    }
}
