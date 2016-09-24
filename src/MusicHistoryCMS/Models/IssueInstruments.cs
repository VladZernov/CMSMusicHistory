using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class IssueInstruments
    {
        public int InstrumentID { get; set; }
        public int IssueID { get; set; }

        public virtual Instrument Instrument { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
