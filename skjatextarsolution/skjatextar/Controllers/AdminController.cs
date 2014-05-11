using skjatextar.Models.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Data.Linq;

namespace skjatextar.Controllers
{
	public class AdminController : Controller
	{
		//
		// GET: /Admin/
		X_UploadFile _DB = new X_UploadFile();
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult UploadFile()
		{
			ViewData["Success"] = "";
			return View();
		}
	}
}