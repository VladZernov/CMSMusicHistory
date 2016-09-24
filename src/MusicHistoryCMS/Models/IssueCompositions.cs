using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class IssueCompositions
    {
        public int CompositionID { get; set; }
        public int IssueID { get; set; }

        public virtual Composition Composition { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
