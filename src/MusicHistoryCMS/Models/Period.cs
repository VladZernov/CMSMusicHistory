using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Period
    {
        public Period()
        {
            Composers = new HashSet<Composer>();
            Instruments = new HashSet<Instrument>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Composer> Composers { get; set; }
        public virtual ICollection<Instrument> Instruments { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
