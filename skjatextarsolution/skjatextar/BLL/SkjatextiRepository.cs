using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {

        /// <summary>
        /// Return a list of all tvshows.
        /// </summary>
        /// <returns></returns>
        public List<Models.SrtFileModel> GetTvShows()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.SrtFileModel>();

            //
            // Must use group by to have unique list of 
            // titles from the SrtFile table, since it duplicates 
            // Show names
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

        public List<Models.TvShowModel> GetEpisodes(int srtId)
        {
            SkjatextiEntities contex = new SkjatextiEntities();
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

        //
        // Returns info about episode to layout.
        //
        public Models.TvShowModel GetEpisode(int epId)
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new Models.TvShowModel();

            var result = (from item in contex.TvShow
                         where item.tvId == epId
                         select item).FirstOrDefault();

                    var episode = new Models.TvShowModel();
                    episode.episodeTitle = result.episodeTitle;
                    episode.season = result.season;
                    episode.episode = result.episode;
                    episode.episodeAbout = result.episodeAbout;
                    episode.tvId = result.tvId;
                    
            return episode;
        }

        // Search from inputbox
        public List<Models.CollectionOfSrt> Search(string s)
        {
            // Connect to db through Skjatexti.context.cs
            SkjatextiEntities contex = new SkjatextiEntities();
            // Creates new empty list using collectionofst
            var list = new List<Models.CollectionOfSrt>();

            // compares input string to database
            List<Models.CollectionOfSrt> listOfAll = GetBothTvshowsAndMovies().Where(b => ContainsIgnoreCase(b.title,s) 
                                                                                    || ContainsIgnoreCase(b.episodeTitle,s)
                                                                                    || ContainsIgnoreCase(b.episodeAbout,s)) .ToList();
            
            
            return listOfAll;
        }
        // Gets all tvshows and movies
        public List<Models.CollectionOfSrt> GetBothTvshowsAndMovies()
        {
            // Connect to db through Skjatexti.context.cs
            SkjatextiEntities contex = new SkjatextiEntities();
            // Creates new empty list using collectionofstr
            var list = new List<Models.CollectionOfSrt>();
            // Sql query thats selects all in SrtCollection and orders it by title
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
                list.Add(show);

            }
            return list;
        }

        public List<Models.CollectionOfSrt> GetTopTenSrt()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();
            var query = (from item in contex.SrtCollection
                         orderby item.title
                         select item).Take(20); //breyta i 10
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

        // Search tables of TvShows and Movies
        public List<Models.CollectionOfSrt> Search()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var searchResult = new List<Models.CollectionOfSrt>();
            var query = (from item in contex.SrtFile
                         where item.title == "Big"
                         select item).ToList();

            return searchResult;
        }

        /*public List<Models.CollectionOfSrt> GetSrtData()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var model = new List<Models.SrtDataModel>();
            var query = from b in contex.SrtData
                        select b.dataText;
                foreach (var item in query)
                {
                    var dataItem = new SrtData();
                    dataItem.dataText = item.
                    newsitem.blogId = item.BlogId;
                    newsitem.title = item.Title;
                    newsitem.date = item.Date;
                    newsitem.texti = item.Name;
                    newsitem.category = item.Category;
                    model.Add(newsitem);
                }
        }*/

        public SrtCollection GetMovieEpisodeById(int? id)
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var result = (from c in contex.SrtCollection
                          where c.tvId == id
                          select c).SingleOrDefault();
            var tvItem = new SrtCollection();
            tvItem.tvId = result.tvId;
            tvItem.title = result.title;
                

            return tvItem;
        }

        /// <summary>
        /// method to check if string contains another string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
       private bool ContainsIgnoreCase(string source, string toCheck)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return false;

            return source.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

       public List<Models.CollectionOfSrt> GetAllTvshows()
       {
           // Connect to db through Skjatexti.context.cs
           SkjatextiEntities contex = new SkjatextiEntities();
           // Creates new empty list using collectionofstr
           var list = new List<Models.CollectionOfSrt>();
           // Sql query thats selects all in SrtCollection and orders it by title
           var query = from item in contex.SrtCollection
                       where item.type == 2
                       orderby item.title
                       select item;
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
               list.Add(show);

           }
           return list;
       }

       public Models.CollectionOfSrt GetMovieAndEpisodeById(int id)
       {
           SkjatextiEntities contex = new SkjatextiEntities();

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

           return moep;
       }
    }
}