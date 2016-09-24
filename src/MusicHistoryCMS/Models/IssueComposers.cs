using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class IssueComposers
    {
        public int ComposerID { get; set; }
        public int IssueID { get; set; }

        public virtual Composer Composer { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
