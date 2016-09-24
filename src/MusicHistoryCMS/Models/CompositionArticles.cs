using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class CompositionArticles
    {
        public int CompositonID { get; set; }
        public int ArticticleID { get; set; }

        public virtual Article Articticle { get; set; }
        public virtual Composition Compositon { get; set; }
    }
}
