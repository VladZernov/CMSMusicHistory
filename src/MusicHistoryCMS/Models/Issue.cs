using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Issue
    {
        public Issue()
        {
            Articles = new HashSet<Article>();
        }

        public int ID { get; set; }
        public bool Approved { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
