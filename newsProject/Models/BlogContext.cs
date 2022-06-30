namespace newsProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogContext : DbContext
    {
        public BlogContext()
            : base("name=BlogContext")
        {
        }

        public virtual DbSet<catalog> catalogs { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<catalog>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<catalog>()
                .Property(e => e.desc)
                .IsFixedLength();

            modelBuilder.Entity<catalog>()
                .HasMany(e => e.news)
                .WithOptional(e => e.catalog)
                .HasForeignKey(e => e.cat_id);

            modelBuilder.Entity<news>()
                .Property(e => e.title)
                .IsFixedLength();

            modelBuilder.Entity<news>()
                .Property(e => e.bref)
                .IsFixedLength();

            modelBuilder.Entity<news>()
                .Property(e => e.photo)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.photo)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .HasMany(e => e.news)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);
        }
    }
}
