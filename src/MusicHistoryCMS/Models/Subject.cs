using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Articles = new HashSet<Article>();
        }

        public int ID { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual Composer Composer { get; set; }
        public virtual Composition Composition { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual Period Period { get; set; }
    }
}
