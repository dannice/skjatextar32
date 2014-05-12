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
            // Tilraun x
            //
            var bll = new SkjatextiRepository();
            var query = bll.GetBothTvshowsAndMovies();
            ViewData["shows"] = query;
           
        }
	}
}