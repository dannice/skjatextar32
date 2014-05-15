using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class RequestModel
    {
        public int reqId { get; set; }

        [Display(Name = "Titill")]
        [Required(ErrorMessage = "Verður að skrá titil!")]
        public string reqTitle { get; set; }

        public DateTime? reqDate { get; set; }
        public int? reqLike { get; set; }

        [Display(Name = "Númer þáttar")]
        public int? reqEpisodeNr { get; set; }

        [Display(Name = "Sería")]
        public int? reqSeasonNr { get; set; }

        [Display(Name = "Nafn þáttar")]
        public string reqEpisodeTitle { get; set; }

        [Display(Name = "Ártal")]
        public int? reqYear { get; set; }
    }
}