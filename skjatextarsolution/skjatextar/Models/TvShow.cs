using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class TvShowModel
    {
        public int tvId { get; set; }
        [Required]
        public int episode { get; set; }
        [Required]
        public int season { get; set; }
        public string episodeTitle { get; set; }
        public string episodeAbout { get; set; }
    }
}