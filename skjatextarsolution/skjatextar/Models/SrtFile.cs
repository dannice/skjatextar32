using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class SrtFileModel
    {
        public int srtId { get; set; }
        [Required]
        public string title { get; set; }
        public DateTime srtDate { get; set; }
        public int srtReady { get; set; }
        public DateTime rdDate { get; set; }
        public int srtCounter { get; set; }
        public int srtLike { get; set; }
        public int type { get; set; }
    }
}