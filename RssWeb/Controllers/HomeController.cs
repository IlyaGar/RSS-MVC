using Ninject;
using RssWeb.Context;
using RssWeb.Models;
using RssWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace RssWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRssRepository _rssRepository;
        private readonly ISourceRepository _sourceRepository;
        private IEnumerable<FullRss> _result;
        private IEnumerable<RssSource> _sources;

        public HomeController(IRssRepository rssRepository, ISourceRepository sourceRepository)
        {
            _rssRepository = rssRepository;
            _sourceRepository = sourceRepository;

            _sources = _sourceRepository.GetAllAsync().Result;
            ViewBag.sourcesList = new SelectList(_sources, "Id", "Title");
            _result = _rssRepository.GetAllAsync().Result.Join(_sourceRepository.GetAllAsync().Result, r => r.SourceId, s => s.Id, (r, s) => new FullRss
            {
                Headline = r.Headline,
                Description = r.Description,
                Date = r.Date,
                Source = s.Title,
                SourceId = s.Id
            });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ajax()
        {
            ViewBag.Pages = 0;
            ViewBag.Page = 0;
            return View();
        }

        public ActionResult AjaxRss(string selectedSource, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            string sort = null;
            var result = _result;
            ViewBag.Pages = Math.Ceiling((decimal)(result.Count() / pageSize));
            ViewBag.Page = page;
            if (Request.Form["Sort"] != null)
            {
                sort = Request.Form["Sort"].ToString();
                if (sort == "Date")
                    result = result.OrderByDescending(r => r.Date);
                else
                    result = result.OrderBy(r => r.Source);
            }
            if (selectedSource == "All" || selectedSource == "" || selectedSource == null)
                return PartialView(result.ToList().ToPagedList(pageNumber, pageSize));
            else
            {
                return PartialView(result.Where(r => r.SourceId == Int32.Parse(selectedSource)).ToList().ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public ActionResult Show(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = _result;
            return View(result.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Show(string selectedSource, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            string sort = null;
            var result = _result;
            if (Request.Form["Sort"] != null)
            {
                sort = Request.Form["Sort"].ToString();
                if (sort == "Date")
                    result = result.OrderByDescending(r => r.Date);
                else
                    result = result.OrderByDescending(r => r.Source);
            }
            ViewBag.Sort = sort;
            ViewBag.Select = selectedSource;
            if (selectedSource == "All" || selectedSource == "" || selectedSource == null)
                return View(result.ToList());
            else
            {
                return View(result.Where(r => r.SourceId == Int32.Parse(selectedSource)).ToList());
            }
        }

        [HttpGet]
        public ActionResult ShowList(string selectedSource, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            string sort = null;
            var sor = ViewBag.Sort;
            var sel = ViewBag.Select;
            var result = _result;
            if (Request.Form["Sort"] != null)
            {
                sort = Request.Form["Sort"].ToString();
                if (sort == "Date")
                    result = result.OrderByDescending(r => r.Date);
                else
                    result = result.OrderByDescending(r => r.Source);
            }
            if (selectedSource == "All" || selectedSource == "" || selectedSource == null)
                return View(@"~/Views/Home/Show.cshtml", result.ToList().ToPagedList(pageNumber, pageSize));
            else
            {
                return View(@"~/Views/Home/Show.cshtml", result.Where(r => r.SourceId == Int32.Parse(selectedSource)).ToList().ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpPost]
        public ActionResult ShowPage(FullRss Model, int? page)
        {
            return View(@"~/Views/Home/Show.cshtml", Model);
        }
    }
}