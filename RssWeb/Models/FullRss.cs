using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssWeb.Models
{
    public class FullRss
    {
        public string Source { get; set; }

        public string Headline { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int SourceId { get; set; }
    }
}