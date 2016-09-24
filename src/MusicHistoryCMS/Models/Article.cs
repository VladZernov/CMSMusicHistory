using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Article
    {
        public int ID { get; set; }
        public string AuthorID { get; set; }
        public DateTime Date { get; set; }
        public int? IssueID { get; set; }
        public int? SubjectID { get; set; }
        public string Text { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Issue Issue { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
