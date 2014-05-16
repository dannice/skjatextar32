//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace skjatextar
{
    using System;
    using System.Collections.Generic;
    
    public partial class SrtFile
    {
        public SrtFile()
        {
            this.Comment = new HashSet<Comment>();
        }
    
        public int srtId { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> srtDate { get; set; }
        public Nullable<System.DateTime> rdDate { get; set; }
        public Nullable<int> srtCounter { get; set; }
        public Nullable<int> srtLike { get; set; }
        public Nullable<int> movieId { get; set; }
        public Nullable<int> tvId { get; set; }
        public Nullable<int> dataId { get; set; }
        public Nullable<int> type { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual TvShow TvShow { get; set; }
        public virtual SrtData SrtData { get; set; }
    }
}
