using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class CollectionComment
    {
        public int commentId { get; set; }
        [Required]
        public string comment { get; set; }
        public int report { get; set; }
        public DateTime? commentDate { get; set; }

        public int srtId { get; set; }
        [Required]
        public string title { get; set; }
        public DateTime srtDate { get; set; }
        public int srtReady { get; set; }
        public DateTime rdDate { get; set; }
        public int srtCounter { get; set; }
        public int srtLike { get; set; }
        public int type { get; set; }

        [Required]
        public string UserName { get; set; }

        public int? dataId { get; set; }
        [Required]
        public string dataFile { get; set; }
        public int? dataSize { get; set; }
        public string dataName { get; set; }
        public string dataCont { get; set; }
        public string dataText { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual SrtFile SrtFile { get; set; }
        public virtual SrtData SrtData { get; set; }
    }
}