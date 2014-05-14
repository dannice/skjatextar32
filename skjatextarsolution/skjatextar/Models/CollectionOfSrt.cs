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
        [Required(ErrorMessage = "Verður að skrá titil!")]
        public string title { get; set; }
        public DateTime srtDate { get; set; }
        public int srtReady { get; set; }
        public DateTime rdDate { get; set; }
        public int srtCounter { get; set; }
        public int srtLike { get; set; }
        public int type { get; set; }
        public int? movieId { get; set; }
        [Required(ErrorMessage = "Verður að skrá ártal!")]
        public int? year { get; set; }
        public int? tvId { get; set; }
        [Required(ErrorMessage = "Verður að skrá nafn á þátt!")]
        public int? episode { get; set; }
        [Required(ErrorMessage = "Verður að skrá seríu!")]
        public int? season { get; set; }
        public string episodeTitle { get; set; }
        public string episodeAbout { get; set; }
        public int dataId { get; set; }
        [Required]
        public string dataFile { get; set; }
        public int? dataSize { get; set; }
        public string dataName { get; set; }
        public string dataCont { get; set; }
        public string dataText { get; set; }

        public virtual TvShow TvShow { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual SrtFile SrtFile { get; set; }
        public virtual SrtData SrtData { get; set; }
    }
}