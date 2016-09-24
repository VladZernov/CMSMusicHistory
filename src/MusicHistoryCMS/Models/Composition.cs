using System;
using System.Collections.Generic;

namespace MusicHistoryCMS.Models
{
    public partial class Composition
    {
        public int ID { get; set; }
        public int? ComposerID { get; set; }
        public string Description { get; set; }
        public int? GenreID { get; set; }
        public int? KeywordID { get; set; }
        public string Name { get; set; }
        public int? Value { get; set; }
        public int? Year { get; set; }
        public string YoutubeKey { get; set; }

        public virtual Composer Composer { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
