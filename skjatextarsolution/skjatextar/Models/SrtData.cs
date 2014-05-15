using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class SrtDataModel
    {
        public int dataId { get; set; }
        [Required]
        public string dataFile { get; set; }
        public int? dataSize { get; set; }
        public string dataName { get; set; }
        public string dataCont { get; set; }
        public string dataText { get; set; }
    }
}