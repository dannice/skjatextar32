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

        [Display(Name = "Titill")]
        [Required(ErrorMessage = "Verður að skrá titil!")]
        public string title { get; set; }

        public DateTime srtDate { get; set; }
        public int srtReady { get; set; }
        public DateTime rdDate { get; set; }
        public int srtCounter { get; set; }
        public int srtLike { get; set; }

        [Display(Name = "Tegund")]
        [Required(ErrorMessage = "Verður að velja tegund!")]
        public int? type { get; set; }

        public int? movieId { get; set; }

        [Display(Name = "Ártal")]
        [Required(ErrorMessage = "Verður að skrá ártal!")]
        public int? year { get; set; }

        public int? tvId { get; set; }

        [Display(Name = "Númer þáttar")]
        [Required(ErrorMessage = "Verður að skrá nr á þátt!")]
        public int? episode { get; set; }

        [Display(Name = "Sería")]
        [Required(ErrorMessage = "Verður að skrá seríu!")]
        public int? season { get; set; }

        [Display(Name = "Nafn þáttar")]
        public string episodeTitle { get; set; }

        [Display(Name = "Um þátt")]
        public string episodeAbout { get; set; }
        public int dataId { get; set; }

        [Display(Name = "Athugasemd")]
        [Required(ErrorMessage = "Verður að skrá athugasemd!")]
        public string dataFile { get; set; }

        public int? dataSize { get; set; }
        public string dataName { get; set; }
        public string dataCont { get; set; }
        public string dataText { get; set; }
        public int? dataReady { get; set; }

        public virtual TvShow TvShow { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual SrtFile SrtFile { get; set; }
        public virtual SrtData SrtData { get; set; }
    }
}