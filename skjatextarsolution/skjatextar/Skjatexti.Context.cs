﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SkjatextiEntities : DbContext
    {
        public SkjatextiEntities()
            : base("name=SkjatextiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<SrtData> SrtData { get; set; }
        public virtual DbSet<SrtFile> SrtFile { get; set; }
        public virtual DbSet<TvShow> TvShow { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<SrtCollection> SrtCollection { get; set; }
    }
}
