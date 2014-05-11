using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {
        public List<Models.CollectionOfSrt> GetBothTvshowsAndMovies()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();
            var query = from item in contex.SrtFile
                        join elem in contex.TvShow
                        on item.tvId equals elem.tvId
                        join melem in contex.Movie
                        on item.movieId equals melem.movieId
                        orderby item.title
                        select item;
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                //var movie = new Models.MovieModel();
                //var both = new Models.SrtFileModel();
                show.title = item.title;
                show.episodeAbout = item.TvShow.episodeAbout;
                show.season = item.TvShow.season;
                show.episode = item.TvShow.episode;
                show.year = item.Movie.year;
                show.episodeTitle = item.TvShow.episodeTitle;
                list.Add(show);

            }
            return list;
        }

        public List<Models.CollectionOfSrt> GetTopTenSrt()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();
            var query = (from item in contex.SrtFile
                         join elem in contex.TvShow
                         on item.tvId equals elem.tvId
                         join melem in contex.Movie
                         on item.movieId equals melem.movieId
                         orderby item.title
                         select item).Take(10);
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                //var movie = new Models.MovieModel();
                //var both = new Models.SrtFileModel();
                show.title = item.title;
                show.episodeAbout = item.TvShow.episodeAbout;
                show.season = item.TvShow.season;
                show.episode = item.TvShow.episode;
                show.year = item.Movie.year;

                list.Add(show);

            }
            return list;
        }

    }
}