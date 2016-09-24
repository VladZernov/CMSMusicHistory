using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Keyword
    {
        public Keyword()
        {
            Compositions = new HashSet<Composition>();
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Composition> Compositions { get; set; }
    }
}
