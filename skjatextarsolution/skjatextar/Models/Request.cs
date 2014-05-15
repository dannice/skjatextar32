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
        [Required]
        public string reqTitle { get; set; }
        public DateTime? reqDate { get; set; }
        public int? reqLike { get; set; }
        public int reqEpisodeNr { get; set; }
        public int reqSeasonNr { get; set; }
        public string reqEpisodeTitle { get; set; }
    }
}