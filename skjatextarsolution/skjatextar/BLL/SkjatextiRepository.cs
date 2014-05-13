using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {
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
                var both = new Models.SrtFileModel();
                show.title = item.title;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;
                

                list.Add(show);

            }
            return list;
        }

        public List<Models.CollectionOfSrt> TvShowAndSrtFileJoin()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();

            var query = from item in contex.SrtFile
                        join elem in contex.TvShow
                        on item.tvId equals elem.tvId
                        select item;
            foreach(var item in query )
            {
                var show = new Models.CollectionOfSrt();
                
                show.episode = item.TvShow.episode;
                show.episodeAbout = item.TvShow.episodeAbout;
                show.episodeTitle = item.TvShow.episodeTitle;
                show.season = item.TvShow.season;
                show.tvId = item.TvShow.tvId;
                show.title = item.title;
                

                list.Add(show);
            }
            return list;
        }

    }
}