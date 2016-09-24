using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Compositions = new HashSet<Composition>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Composition> Compositions { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
