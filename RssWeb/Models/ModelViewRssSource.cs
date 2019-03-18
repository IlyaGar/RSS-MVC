using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RssWeb.Models
{
    public class ModelViewRssSource
    {
        public List<SelectListItem> rss { get; set; }

        public int SelecedRss { get; set; }
    }
}