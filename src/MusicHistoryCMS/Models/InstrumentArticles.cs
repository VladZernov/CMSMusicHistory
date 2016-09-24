using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class InstrumentArticles
    {
        public int InstrumentID { get; set; }
        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }
        public virtual Instrument Instrument { get; set; }
    }
}
