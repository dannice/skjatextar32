using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace skjatextar.Models.Upload
{
	public class upload
	{
		[Key]
		public virtual int Upload_id { get; set; }
		[Required]
		public virtual string Title { get; set; }
	}
}
