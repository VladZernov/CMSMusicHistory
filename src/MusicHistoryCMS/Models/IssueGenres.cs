using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class IssueGenres
    {
        public int GenreID { get; set; }
        public int IssueID { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
