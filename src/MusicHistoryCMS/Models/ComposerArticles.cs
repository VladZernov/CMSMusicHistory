using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class ComposerArticles
    {
        public int ComposerID { get; set; }
        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }
        public virtual Composer Composer { get; set; }
    }
}
