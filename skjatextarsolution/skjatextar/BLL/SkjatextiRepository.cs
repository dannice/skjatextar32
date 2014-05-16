using skjatextar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {
        /// <summary>
        /// Connection to db.
        /// </summary>
        SkjatextiEntities contex = new SkjatextiEntities();

        /// <summary>
        /// Return a list of all tvshows.
        /// </summary>
        public List<Models.SrtFileModel> GetTvShows()
        {
            var list = new List<Models.SrtFileModel>();

            var result = (from item in contex.SrtFile
                          where item.type == 2
                          group item by new {item.title} 
                          into showGroup
                          select showGroup.FirstOrDefault());

             foreach(var item in result)
             {
                 var srtFile = new Models.SrtFileModel();
                 srtFile.title = item.title;
                 srtFile.srtId = item.srtId;
                 list.Add(srtFile);
             }
            return list;
        }

        /// <summary>
        /// Returns list of all episodes in serie.
        /// </summary>
        public List<Models.TvShowModel> GetEpisodes(int srtId)
        {
            var list = new List<Models.TvShowModel>();

            var srtList = from srtItem in contex.SrtFile
                          join srtItem2 in contex.SrtFile
                          on srtItem.title equals srtItem2.title
                          where srtItem.srtId == srtId
                          select srtItem2.tvId;

            var result = from item in contex.TvShow
                         select item;
           
            foreach (var item in result)
            {
                if (srtList.Contains(item.tvId))
                {
                    var episode = new Models.TvShowModel();
                    episode.episodeTitle = item.episodeTitle;
                    episode.season = item.season;
                    episode.episode = item.episode;
                    episode.episodeAbout = item.episodeAbout;
                    episode.tvId = item.tvId;
                    list.Add(episode);
                }
            }
            return list;
        }

        /// <summary>
        ///  Returns info about episode by id.
        /// </summary>
        public Models.CollectionOfSrt GetEpisode(int epId)
        {
            var result = (from item in contex.SrtCollection
                         where item.tvId == epId
                         select item).FirstOrDefault();
                    
                    var episode = new Models.CollectionOfSrt();
                    episode.srtId = result.srtId;
                    episode.episodeTitle = result.episodeTitle;
                    episode.season = result.season;
                    episode.episode = result.episode;
                    episode.episodeAbout = result.episodeAbout;
                    episode.tvId = result.tvId;

            return episode;
        }

        /// <summary>
        /// Return a list of all requests from users.
        /// </summary>
        public List<Models.RequestModel> GetAllRequests()
        {
            SkjatextiEntities context = new SkjatextiEntities();

            var list = new List<Models.RequestModel>();

            var requestList = from item in context.Request
                              orderby item.reqDate descending
                              select item;
            foreach (var item in requestList)
            {
                var reqItem = new Models.RequestModel() ;
                reqItem.reqId = item.reqId;
                reqItem.reqTitle = item.reqTitle;
                reqItem.reqYear = item.reqYear;
                reqItem.reqSeasonNr = item.reqSeasonNr;
                reqItem.reqEpisodeTitle = item.reqEpisodeTitle;
                reqItem.reqEpisodeNr = item.reqEpisodeNr;
                reqItem.reqDate = item.reqDate;

                list.Add(reqItem);
            }

            return list;
        }

        /// <summary>
        /// Search from inputbox.
        /// </summary>
        public List<Models.CollectionOfSrt> Search(string s)
        {
            var list = new List<Models.CollectionOfSrt>();

            // Compares input string to database.
            List<Models.CollectionOfSrt> listOfAll = GetBothTvshowsAndMovies().Where(b => ContainsIgnoreCase(b.title,s) 
                                                                                    || ContainsIgnoreCase(b.episodeTitle,s)
                                                                                    || ContainsIgnoreCase(b.episodeAbout,s)) .ToList();
            
            
            return listOfAll;
        }

        /// <summary>
        /// Gets all tvshows and movies.
        /// </summary>
        public List<Models.CollectionOfSrt> GetBothTvshowsAndMovies()
        {
            var list = new List<Models.CollectionOfSrt>();
            // Sql query thats selects all in SrtCollection and orders it by title.
            var query = from item in contex.SrtCollection
                         orderby item.srtId
                         select item;
            // Loops through every item in query.
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                show.srtId = item.srtId;
                show.title = item.title;
                show.tvId = item.tvId;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;
                show.episodeTitle = item.episodeTitle;
                show.tvId = item.tvId;
                show.movieId = item.movieId;
                show.type = item.type;
                show.dataReady = item.dataReady;
                list.Add(show);

            }
            return list;
        }

        /// <summary>
        /// Returns list of top ten tvshows and movies ordered newest.
        /// </summary>
        public List<Models.CollectionOfSrt> GetNewBothTvshowsAndMovies()
        {
            // Creates new empty list using collectionofstr
            var list = new List<Models.CollectionOfSrt>();
            // Sql query thats selects all in SrtCollection and orders it by title.
            var query = (from item in contex.SrtCollection
                         orderby item.srtDate descending
                         select item).Take(10);
            // Loops through every item in query.
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                show.srtId = item.srtId;
                show.title = item.title;
                show.tvId = item.tvId;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;
                show.episodeTitle = item.episodeTitle;
                show.tvId = item.tvId;
                show.movieId = item.movieId;
                show.type = item.type;
                list.Add(show);
            }
            return list;
        }

        /// <summary>
        /// Lists up 10 episode and tvshow by alphabet.
        /// </summary> 
        public List<Models.CollectionOfSrt> GetTopTenSrt()
        {
            var list = new List<Models.CollectionOfSrt>();
            var query = (from item in contex.SrtCollection
                         orderby item.title 
                         select item).Take(10); 
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                show.title = item.title;
                show.srtId = item.srtId;
                show.tvId = item.tvId;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;
                show.tvId = item.tvId;
                show.movieId = item.movieId;
                show.type = item.type;
                list.Add(show);
            }
            return list;
        }

        /// <summary>
        /// Finds episode or movie by id and returns it.
        /// </summary>
        public SrtCollection GetMovieEpisodeById(int? id)
        {
            var result = (from c in contex.SrtCollection
                          where c.tvId == id
                          select c).SingleOrDefault();
            var tvItem = new SrtCollection();
            tvItem.tvId = result.tvId;
            tvItem.title = result.title;
                
            return tvItem;
        }

        /// <summary>
        /// Makes search input not case sensitive.
        /// </summary>
       private bool ContainsIgnoreCase(string source, string toCheck)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return false;

            return source.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

       /// <summary>
       /// Returns list of all tv shows.
       /// </summary>
       public List<Models.CollectionOfSrt> GetAllTvshows()
       {
           var list = new List<Models.CollectionOfSrt>();
           // Sql query thats selects all in SrtCollection and orders it by title.
           var query = (from item in contex.SrtCollection
                        where item.type == 2
                        orderby item.srtCounter descending 
                        select item).Take(10);
           // Loops through every item in query.
           foreach (var item in query)
           {
               var show = new Models.CollectionOfSrt();
               show.title = item.title;
               show.tvId = item.tvId;
               show.episodeAbout = item.episodeAbout;
               show.season = item.season;
               show.episode = item.episode;
               show.episodeTitle = item.episodeTitle;
               show.type = item.type;
               show.srtId = item.srtId;
               list.Add(show);
           }
           return list;
       }

       /// <summary>
       /// Returns list of all movies.
       /// </summary>
       public List<Models.CollectionOfSrt> GetAllMovies()
       {
           var list = new List<Models.CollectionOfSrt>();
           // Sql query thats selects all in SrtCollection and orders it by title.
           var query = (from item in contex.SrtCollection
                        where item.type == 1
                        orderby item.srtCounter descending 
                        select item).Take(10);
           // Loops through every item in query.
           foreach (var item in query)
           {
               var show = new Models.CollectionOfSrt();
               show.title = item.title;
               show.year = item.year;
               show.type = item.type;
               show.srtId = item.srtId;
               list.Add(show);
           }
           return list;
       }

       /// <summary>
       /// Returns episode or movie by id.
       /// </summary>
       public Models.CollectionOfSrt GetMovieAndEpisodeById(int id)
       {
           var result = (from item in contex.SrtCollection
                         where item.srtId == id
                         select item).FirstOrDefault();

           var moep = new Models.CollectionOfSrt();
           moep.title = result.title;
           moep.tvId = result.tvId;
           moep.episodeAbout = result.episodeAbout;
           moep.season = result.season;
           moep.episode = result.episode;
           moep.episodeTitle = result.episodeTitle;
           moep.type = result.type;
           moep.dataReady = result.dataReady;
           moep.srtId = result.srtId;
           moep.year = result.year;
           moep.dataId = result.Expr4;
           return moep;
       }

       /// <summary>
       /// Returns list of 10 newest request.
       /// </summary>
       public List<Models.RequestModel> GetRequests()
       {
           var list = new List<Models.RequestModel>();

           var result = (from item in contex.Request
                         orderby item.reqDate descending
                         select item).Take(10);
            foreach (var item in result)
	        {
		         var reqItem = new Models.RequestModel();
                 reqItem.reqId = item.reqId;
                 reqItem.reqDate = item.reqDate;
                 reqItem.reqTitle = item.reqTitle;
                 reqItem.reqSeasonNr = item.reqSeasonNr;
                 reqItem.reqEpisodeNr = item.reqEpisodeNr;
                 reqItem.reqEpisodeTitle = item.reqEpisodeTitle;
                 list.Add(reqItem);
	        }

            return list;
       }

       /// <summary>
       /// Deletes request by id.
       /// </summary>
       public void DeleteRequest(int? reqId)
       {
           SkjatextiEntities context = new SkjatextiEntities();

           var result = (from item in context.Request
                         where item.reqId == reqId
                         select item).FirstOrDefault();

           context.Request.Remove(result);

           context.SaveChanges();          
       }
    }
}