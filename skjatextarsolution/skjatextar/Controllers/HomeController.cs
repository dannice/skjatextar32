using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using skjatextar.BLL;
using skjatextar.Models;


namespace skjatextar.Controllers
{
	public class HomeController : ApplicationController
	{
		public ActionResult Index() 
		{
           
            var bll = new SkjatextiRepository();
            var topten = bll.GetTopTenSrt();
            //var both = bll.GetBothTvshowsAndMovies(); var með þetta 

            //return View(users);
            return View(topten);
		}
       

       public ActionResult Details(int? id)
        {
           SkjatextiEntities context = new SkjatextiEntities();
           var bll = new SkjatextiRepository();
           var test = bll.TvShowAndSrtFileJoin();
           var query = (from item in test
                        where item.tvId == id
                        select item).FirstOrDefault();
           if (query != null)
           {
               return View(query);
           }
           return View("Error");
                                                     

        }

      
       

       private ActionResult View(Func<int?, ActionResult> Details)
       {
           throw new NotImplementedException();
       }

        

		public ActionResult About()
		{
			//ViewBag.Message = "Your application description page.";
            //var bll = new SkjatextiBLL();
            //var request = bll.GetRequests();
            //return View(request);

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}