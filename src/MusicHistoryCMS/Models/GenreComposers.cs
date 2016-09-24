using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class GenreComposers
    {
        public int ComposerID { get; set; }
        public int GenreID { get; set; }

        public virtual Composer Composer { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
