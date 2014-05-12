using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class CollectionOfSrt
    {
        public int srtId { get; set; }
        [Required]
        public string title { get; set; }
        public DateTime srtDate { get; set; }
        public int srtReady { get; set; }
        public DateTime rdDate { get; set; }
        public int srtCounter { get; set; }
        public int srtLike { get; set; }
        public int movieId { get; set; }
        [Required]
        public int? year { get; set; }
        public int tvId { get; set; }
        [Required]
        public int? episode { get; set; }
        [Required]
        public int? season { get; set; }
        public string episodeTitle { get; set; }
        public string episodeAbout { get; set; }
    }
}