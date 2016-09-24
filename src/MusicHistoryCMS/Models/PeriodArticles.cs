using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class PeriodArticles
    {
        public int PeriodID { get; set; }
        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }
        public virtual Period Period { get; set; }
    }
}
