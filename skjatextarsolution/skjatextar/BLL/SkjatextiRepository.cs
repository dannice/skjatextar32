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


        // Gets all tvshows and movies
        public List<Models.CollectionOfSrt> GetBothTvshowsAndMovies()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();
           /*var query = from item in contex.SrtFile
                         join elem in contex.TvShow
                         on item.tvId equals elem.tvId
                         join melem in contex.Movie
                         on item.movieId equals melem.movieId
                         orderby item.title
                         select item;*/
            var query = from item in contex.SrtCollection
                         orderby item.title
                         select item;
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                //var movie = new Models.MovieModel();
                //var both = new Models.SrtFileModel();
                show.title = item.title;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;
                show.episodeTitle = item.episodeTitle;
                list.Add(show);

            }
            return list;
        }

        public List<Models.CollectionOfSrt> GetTopTenSrt()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();
            /*
            var query = (from item in contex.SrtFile
                         join elem in contex.TvShow
                         on item.tvId equals elem.tvId
                         join melem in contex.Movie
                         on item.movieId equals melem.movieId
                         orderby item.title
                         select item).Take(10);*/
            var query = (from item in contex.SrtCollection
                         orderby item.title
                         select item).Take(10);
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                //var movie = new Models.MovieModel();
                //var both = new Models.SrtFileModel();
                show.title = item.title;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;

                list.Add(show);

            }
            return list;
        }

    }
}