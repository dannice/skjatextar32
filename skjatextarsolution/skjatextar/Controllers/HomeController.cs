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
             return View();
		}
 
		public ActionResult About()
		{
 			return View();
		}

		public ActionResult Contact()
		{
            return View();
		}

	}
}