using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using skjatextar.BLL;
using skjatextar.Models;


namespace skjatextar.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
           
            var bll = new SkjatextiRepository();
            var both = bll.GetBothTvshowsAndMovies();

            //return View(users);
            return View(both);
		}
       
       [HttpGet]
       public ActionResult Details(int? id)
        {
           SkjatextiEntities context = new SkjatextiEntities();
           var bll = new SkjatextiRepository();
           var both = bll.GetBothTvshowsAndMovies();
           var query = (from item in both
                        where item.tvId == id
                        select item).SingleOrDefault();
                                                       
           if (query != null)
            { 
               return View(query);
             }
                    
          return View("error");
        }

       [HttpPost]
       public ActionResult Details(int? id, FormCollection form)
       {
           SkjatextiEntities context = new SkjatextiEntities();
           var bll = new SkjatextiRepository();
           var both = bll.GetBothTvshowsAndMovies();
           var query = (from item in both
                          where item.tvId == id
                          select item).SingleOrDefault();

           if (ModelState.IsValid)
           {
               UpdateModel(query);

               context.SaveChanges();
               return RedirectToAction("Index");

           }

           return View(query);
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