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
            ViewBag.Message = "";

            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, FormCollection col)
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
                // Radio button from upload view
                var radioMovie = col["radioMovie"];
                var radioTv = col["radioTv"];
                string title = col["title"];
                
                // Connects to database
                using (var db = new SkjatextiEntities())
                {
                    var dataItem = new SrtData();
                    var movieItem = new Movie();
                    var tvItem = new TvShow();
                    var srtItem = new SrtFile();
                    // Puts filename into db
                    dataItem.dataName = fileName;
                    // Puts all text from file to db
                    dataItem.dataText = text;
                    db.SrtData.Add(dataItem);
                    srtItem.dataId = dataItem.dataId;
                    if (!String.IsNullOrEmpty(radioMovie))
                    {
                        int year = Convert.ToInt32(col["yearMovie"]);
                        movieItem.year = year;
                        db.Movie.Add(movieItem);
                        srtItem.movieId = movieItem.movieId;
                        // Type 1 if movie.
                        srtItem.type = 1;
                    }
                    else if (!String.IsNullOrEmpty(radioTv))
                    {
                        // Vantar að setja inn að episodeNr og seasonNr er skylda.
                        // Vantar að setja inn að episodeTite og episodeAbout er ekki skylda.
                        string episodeTitle = col["episodeTitle"];
                        string episodeAbout = col["episodeAbout"];
                        int episodeNr = Convert.ToInt32(col["episodeNr"]);
                        int seasonNr = Convert.ToInt32(col["seasonNr"]);
                        tvItem.episode = episodeNr;
                        tvItem.season = seasonNr;
                        tvItem.episodeTitle = episodeTitle;
                        tvItem.episodeAbout = episodeAbout;
                        db.TvShow.Add(tvItem);
                        srtItem.tvId = tvItem.tvId;
                        srtItem.type = 2;
                    }

                    // Hér vantar error message um ef hvorugt er valið, mynd eða þáttaröð.

                    srtItem.title = title;
                    srtItem.srtDate = DateTime.Today;

                    db.SrtFile.Add(srtItem);
                    db.SaveChanges();
                }
                streamReader.Close();
            }

            // Redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        [HttpGet]
        // Gets file from database and displays it on page
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

        [HttpPost]
        // Takes changes made in textbox, pushes it to db and overwrites current data
        public ActionResult EditFile(FormCollection col, int id)
        {
            string dataText = col["dataText"];
            using (var db = new SkjatextiEntities())
            {
                var edit = (from sr in db.SrtData
                           where sr.dataId == id
                           select sr).FirstOrDefault();

                edit.dataText = dataText;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            SkjatextiRepository bll = new SkjatextiRepository();
            var getDetails = bll.GetBothTvshowsAndMovies();
            var result = (from elem in getDetails
                          where elem.srtId == id 
                          select elem).SingleOrDefault();

            if (result != null)
            {
                return View(result);
            }
            return View("error");
        }

        // Downloads from database an .srt file
        public FileResult Download(int? id)
        {
            // New empty string
            string text = "";
            // New empty filename
            string filename = "";
            // int? counter = ++;
            // Gets connected to database and gets data that matches id
            using (var db = new SkjatextiEntities())
            {
                var query = (from s in db.SrtCollection
                             where s.srtId == id
                             select s).FirstOrDefault();
                /*var query = (from s in db.SrtData
                             where s.dataId == id
                             select s).FirstOrDefault();*/
                text = query.dataText;
                filename = query.dataName;
                // var srtItem = new SrtFile();
                // counter = srtItem.srtCounter;

            }
            // Returns files with UTF-8 encoding, changes it to Bytes and exports it to .srt file
            return File(new System.Text.UTF8Encoding().GetBytes(text), "text/plain; charset=utf-8", filename);
        }

        // Showing search result from text box
        [HttpPost]
        public ActionResult SearchResult()
        {
            string query = Request.Params.Get("srch-term");

            var bll = new SkjatextiRepository();
            var results = bll.Search(query);

            ViewData["query"] = query;

            return View(results);
        }
    }
}