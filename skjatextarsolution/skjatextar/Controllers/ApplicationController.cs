using skjatextar.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace skjatextar.Controllers
{
    public abstract class ApplicationController : Controller
    {
        private SkjatextiEntities context = new SkjatextiEntities();

        public SkjatextiEntities DataContext
        {
            get { return context; }
        }
        
        public ApplicationController()
        {
            var bll = new SkjatextiRepository();
            ViewData["tvshows"] = bll.GetTvShows();
            ViewData["shows"] = bll.GetBothTvshowsAndMovies();
           
        }

        [HttpGet]
        public JsonResult GetEpisodeDataByShow()
        {
            string srtId = Request.QueryString["srtId"];
            var bll = new SkjatextiRepository();
            return Json(bll.GetEpisodes(int.Parse(srtId)), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetEpisodeData()
        {
            string epId = Request.QueryString["epId"];
            var bll = new SkjatextiRepository();
            return Json(bll.GetEpisode(int.Parse(epId)), JsonRequestBehavior.AllowGet);
        }
	}
}