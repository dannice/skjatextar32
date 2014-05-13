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
            var query = bll.GetTopTenSrt();

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

                // Extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // Starts to read file
                StreamReader streamReader = new StreamReader(file.InputStream);
                // Reads until end of the file.
                string text = streamReader.ReadToEnd();
                
                // Connects to database
                using (var db = new SkjatextiEntities())
                {
                    var dataItem = new SrtData();
                    // Puts filename into db
                    dataItem.dataName = fileName;
                    // Puts all text from file to db
                    dataItem.dataText = text;

                    db.SrtData.Add(dataItem);
                    db.SaveChanges();
                }
                streamReader.Close();
            }

            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        public ActionResult EditFile(int id)
        {
            var model = new SrtData();
            using (var db = new SkjatextiEntities())
            {
                var query = (from s in db.SrtData
                            where s.dataId == id
                            select s).FirstOrDefault();
                model.dataText = query.dataText;
            }
            var srtModel = new SrtDataModel { dataId = model.dataId, dataText = model.dataText, dataName = model.dataName, dataSize = model.dataSize };
            return View("EditFile",srtModel);
        }

        /*[HttpPost]
        public ActionResult EditFile()
        {

        }*/
        public ActionResult Search(string searchString)
        {
            // framkvæmir search í sql
            //returnar view með results
            return null;
        }

        public ActionResult Details(int? id)
        {
            SkjatextiRepository bll = new SkjatextiRepository();
            var getDetails = bll.GetBothTvshowsAndMovies();

            var result = (from elem in getDetails
                          where elem.tvId == id
                          select elem).SingleOrDefault();
            if(result != null)
            {
                return View(result);
            }
            return View("error");
          }
    }
}