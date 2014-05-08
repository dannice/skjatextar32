﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class RequestModel
    {
        public int reqId { get; set; }
        public string requestText { get; set; }
        public DateTime? reqDate { get; set; }
        public int? reqLike { get; set; }
    }
}