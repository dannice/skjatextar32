﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using skjatextar.BLL;

namespace skjatextar.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            //var bll = new UserRepository();
            //var users = bll.GetUsers();

            var srt = new SkjatextiRepository();
            var list = srt.GetTvShow();
            return View(list);
		}

		public ActionResult About()
		{
			//ViewBag.Message = "Your application description page.";
            var bll = new UserRepository();
            var request = bll.GetRequests();
            return View(request);

			//return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}