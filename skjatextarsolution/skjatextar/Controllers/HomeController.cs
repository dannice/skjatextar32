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
        /// <summary>
        /// Connection to repository that every function can use.
        /// </summary>
        SkjatextiRepository bll = new SkjatextiRepository();

        public ActionResult Index()
        {
            var query = bll.GetTopTenSrt();

            ViewBag.tvpopular = bll.GetAllTvshows();
            ViewBag.request = bll.GetRequests();
            ViewBag.moviepopular = bll.GetAllMovies();
            ViewBag.newest = bll.GetNewBothTvshowsAndMovies();

            //return View(users);
            return View(query);
        }

        public ActionResult About()
        {
            ViewBag.Main = "Um Ístexta";
            ViewBag.Message = "Siðareglur Ístexta";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }






        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Function that uploads file by reading it and input text from file to db.
        /// </summary>
        [Authorize]
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

                var radioType = col["type"];
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
                    dataItem.dataReady = 1;
                    dataItem.dataSize = file.ContentLength;
                    db.SrtData.Add(dataItem);
                    srtItem.dataId = dataItem.dataId;
                    if ("1".Equals(radioType))
                    {
                        int year = Convert.ToInt32(col["year"]);
                        movieItem.year = year;
                        db.Movie.Add(movieItem);
                        srtItem.movieId = movieItem.movieId;
                        // Type 1 if movie.
                        srtItem.type = 1;
                    }
                    else if ("2".Equals(radioType))
                    {
                        string episodeTitle = col["episodeTitle"];
                        string episodeAbout = col["episodeAbout"];
                        int episodeNr = Convert.ToInt32(col["episode"]);
                        int seasonNr = Convert.ToInt32(col["season"]);
                        tvItem.episode = episodeNr;
                        tvItem.season = seasonNr;
                        tvItem.episodeTitle = episodeTitle;
                        tvItem.episodeAbout = episodeAbout;
                        db.TvShow.Add(tvItem);
                    
                        srtItem.tvId = tvItem.tvId;
                        srtItem.type = 2;
                    }

                    srtItem.title = title;
                    srtItem.srtDate = DateTime.Now;

                    db.SrtFile.Add(srtItem);
                    db.SaveChanges();
                }
                streamReader.Close();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Gets file from database and displays it on page.
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult EditFile(int id)
        {
            var model = new SrtDataModel();
            //CollectionOfSrt srtModel;
            using (var db = new SkjatextiEntities())
            {
                var query = (from s in db.SrtCollection
                             where s.srtId == id
                             select s).FirstOrDefault();

                model.dataText = query.dataText;
                model.dataReady = query.dataReady;
            }

            return View("EditFile", model);
        }

        /// <summary>
        /// Takes changes made in textbox and overwrites current data in db. Returns user to index view.
        /// </summary>
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFile(FormCollection col, int id)
        {
            string dataText = col["dataText"];
            var dataReady = col["checkReady"];

            using (var db = new SkjatextiEntities())
            {
                var edit = (from sr in db.SrtFile
                            join dt in db.SrtData
                            on sr.dataId equals dt.dataId
                            where sr.srtId == id
                            select dt).SingleOrDefault();

                edit.dataText = dataText;

                if (String.IsNullOrEmpty(dataReady))
                {
                    edit.dataReady = 1;
                }
                else
                {
                    edit.dataReady = 2;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays information about selected show/movie by id.
        /// </summary>
        public ActionResult Details(int? id)
        {
            var getDetails = bll.GetMovieAndEpisodeById(Convert.ToInt32(id));

            if (id != null)
            {
                return View(getDetails);
            }

            return View("error");
        }

        /// <summary>
        /// Downloads from database an .srt file.
        /// </summary>
        public FileResult Download(int? id)
        {
            // New empty string.
            string text = "";
            // New empty filename.
            string filename = "";

            // Gets connected to database and gets data that matches id.
            using (var db = new SkjatextiEntities())
            {
                var query = (from s in db.SrtCollection
                             where s.srtId == id
                             select s).FirstOrDefault();

                text = query.dataText;
                filename = query.dataName;
            }

            UpDownloadCounter(id);

            // Returns files with UTF-8 encoding, changes it to Bytes and exports it to .srt file.
            return File(new System.Text.UTF8Encoding().GetBytes(text), "text/plain; charset=utf-8", filename);
        }

        /// <summary>
        /// Ups counter each time a file is downloaded.
        /// </summary>
        private void UpDownloadCounter(int? srtId)
        {
            using (var db = new SkjatextiEntities())
            {
                var query = (from s in db.SrtFile
                             where s.srtId == srtId
                             select s).FirstOrDefault();
                int? count = query.srtCounter;
                if (count == null)
                {
                    query.srtCounter = 1;
                }
                else
                {
                    query.srtCounter++;
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Showing search result from text box.
        /// </summary>
        [HttpPost]
        public ActionResult SearchResult()
        {
            string query = Request.Params.Get("srch-term");

            var bll = new SkjatextiRepository();
            var results = bll.Search(query);

            ViewData["query"] = query;

            return View(results);
        }

        /// <summary>
        /// Function for comments that is never used because website does not offer users to comment.
        /// </summary>
        [HttpGet]
        public ActionResult GetComments()
        {
            CommentRepository cmm = new CommentRepository();
            var model = cmm.GetAllComments();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Function for comments that is never used because website does not offer users to comment.
        /// </summary>
        public ActionResult InsertCommment(string strComment)
        {
            CommentRepository comRep = new CommentRepository();

            if (!String.IsNullOrEmpty(strComment))
            {
                Comment c = new Comment();

                c.comment1 = strComment;
                String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                if (!String.IsNullOrEmpty(strUser))
                {
                    int slashPos = strUser.IndexOf("\\");
                    if (slashPos != -1)
                    {
                        strUser = strUser.Substring(slashPos + 1);
                    }
                    c.AspNetUsers.UserName = strUser;

                    comRep.AddComment(c);
                }
                else
                {
                    c.AspNetUsers.UserName = "Unknown user";
                }
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("CommentText", "Comment text cannot be empty!");
                return Json("FAILED", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Function for comments that is never used because website does not offer users to comment.
        /// </summary>
        private string getCurrentUser()
        {
            var user = new RegisterViewModel();
            String strUser = user.UserName;
            if (!String.IsNullOrEmpty(strUser))
            {
                int slashPos = strUser.IndexOf("\\");
                if (slashPos != -1)
                {
                    strUser = strUser.Substring(slashPos + 1);
                }
            }
            return strUser;
        }

        /// <summary>
        /// View for new request.
        /// </summary>
        [HttpGet]
        public ActionResult NewRequest()
        {
            SkjatextiRepository repo = new SkjatextiRepository();

            ViewData["requests"] = repo.GetAllRequests();

            return View();
        }

        /// <summary>
        /// Pushes inputs from request form to database.
        /// </summary>
        [HttpPost]
        public ActionResult NewRequest(FormCollection col)
        {
            string title = col["reqTitle"];
            string episodeTitle = col["reqEpisodeTitle"];

            int? year = string.IsNullOrEmpty(col["reqYear"]) ? 0 : Convert.ToInt32(col["reqYear"]);
            int? season = string.IsNullOrEmpty(col["reqSeasonNr"]) ? 0 : Convert.ToInt32(col["reqSeasonNr"]);
            int? episode = string.IsNullOrEmpty(col["reqEpisodeNr"]) ? 0 : Convert.ToInt32(col["reqEpisodeNr"]);

            using (var db = new SkjatextiEntities())
            {
                var request = new Request();

                request.reqTitle = title;
                request.reqEpisodeTitle = episodeTitle;
                request.reqYear = year;
                request.reqSeasonNr = season;
                request.reqEpisodeNr = episode;
                request.reqDate = DateTime.Now;
                db.Request.Add(request);

                db.SaveChanges();
            }

            return RedirectToAction("NewRequest");
        }

        /// <summary>
        /// Deletes request if user is logged in.
        /// </summary>
        public ActionResult DeleteRequest(int? reqId)
        {
            SkjatextiRepository repo = new SkjatextiRepository();

            repo.DeleteRequest(reqId);

            return RedirectToAction("NewRequest");
        }
       


    }   
}