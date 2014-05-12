using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using skjatextar.BLL;
using skjatextar.Models;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace skjatextar.Controllers
{
    public class HomeController : ApplicationController
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

       

        /*public ActionResult Upload()
        {
            
            return View();
        }*/

        [HttpGet]
        public ActionResult Upload()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {

                /*// extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                StreamReader streamReader = new StreamReader(fileName);
                string text = streamReader.ReadToEnd();
                /*var db = new SkjatextiRepository()
                

                
                newsitem.title = query.Title;
                newsitem.date = query.Date;
                newsitem.texti = query.Name;
                newsitem.category = query.Category;
                newsitem.blogId = query.BlogId;*/
            }

            //streamReader.Close();
            //var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            //file.SaveAs(path);


            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
    }
}