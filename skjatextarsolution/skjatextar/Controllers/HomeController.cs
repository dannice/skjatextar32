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
            var query = new BLL.SkjatextiRepository().GetTopTenSrt();
           
      
            //return View(users);
            return View(query);
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

        public ActionResult Upload()
        {
            return View();
        }
	}
}