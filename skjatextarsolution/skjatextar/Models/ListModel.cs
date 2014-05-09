using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class ListModel
    {
        public List <SrtFileModel> AllSrtFile { get; set; }
        public List <TvShowModel> AllTvShows{ get; set; }
        public List <MovieModel> AllMovies { get; set; }

    }
}