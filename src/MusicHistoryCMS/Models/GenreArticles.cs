using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class GenreArticles
    {
        public int GenreID { get; set; }
        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
