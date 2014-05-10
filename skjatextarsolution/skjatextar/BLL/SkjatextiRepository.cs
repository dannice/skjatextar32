using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{

    public class SkjatextiRepository
    {
        // Funtion to make a dropdown list.
        public List<Models.SrtFileModel> GetTvShow()
        {
            SkjatextiEntities context = new SkjatextiEntities();

            var listTitle = new List<Models.SrtFileModel>();
            var listEpisode = new List<Models.TvShowModel>();

            var query = (from item in context.SrtFile
                         join elem in context.TvShow
                         on item.tvId equals elem.tvId
                         orderby item.title
                         select item);      

            foreach (var item in query)
            {
                
                var srt = new Models.SrtFileModel();
                var show = new Models.TvShowModel();
                srt.title = item.title;
                show.episodeAbout = item.TvShow.episodeAbout;
                show.season = item.TvShow.season;
                show.episode = item.TvShow.episode;
                listTitle.Add(srt);
                listEpisode.Add(show);
                //listEpisode.Add(show.episode);
                //listEpisode.Add(show.season);

                listEpisode.Add(show);
            }
           // var valueList = listTitle.
            return listTitle;
        }

        public List<Models.SrtFileModel> GetMovie()
        {
            SkjatextiEntities context = new SkjatextiEntities();

            var listTitle = new List<Models.SrtFileModel>();
            var listMovie = new List<Models.MovieModel>();

            var query = (from item in context.SrtFile
                         join elem in context.Movie
                         on item.movieId equals elem.movieId
                         orderby item.title
                         select item);

            foreach (var item in query)
            {

                var srt = new Models.SrtFileModel();
                var movie = new Models.MovieModel();
                srt.title = item.title;
                movie.year = item.Movie.year;
                listTitle.Add(srt);
                listMovie.Add(movie);
                //listEpisode.Add(show.episode);
                //listEpisode.Add(show.season);

                listMovie.Add(movie);
            }
            // var valueList = listTitle.

            return listTitle;
        }
    }
}