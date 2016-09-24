using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class IssuePeriods
    {
        public int PeriodID { get; set; }
        public int IssueID { get; set; }

        public virtual Issue Issue { get; set; }
        public virtual Period Period { get; set; }
    }
}
