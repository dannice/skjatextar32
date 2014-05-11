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
            // Tilraun
            //
            var bll = new SkjatextiRepository();
            var both = bll.GetBothTvshowsAndMovies();
            ViewData["shows"] = both;
           
        }
	}
}