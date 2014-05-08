using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class SrtDataModel
    {
        public int dataId { get; set; }
        [Required]
        public string dataTime { get; set; }
        [Required]
        public string dataText { get; set; }
    }
}