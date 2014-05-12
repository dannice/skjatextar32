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

                //extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                StreamReader streamReader = new StreamReader(path);
                string text = streamReader.ReadToEnd();
                
                using (var db = new SkjatextiEntities())
                {
                    var dataItem = new SrtData();
                    dataItem.dataName = fileName;
                    dataItem.dataText = text;

                    db.SrtData.Add(dataItem);
                    db.SaveChanges();
                }
                streamReader.Close();
            }

            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
    }
}