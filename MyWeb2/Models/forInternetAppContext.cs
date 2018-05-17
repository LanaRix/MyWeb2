using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyWeb2.Models
{
    public partial class forInternetAppContext : DbContext
    {
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Communication> Communication { get; set; }
        public virtual DbSet<User> User { get; set; }

        public forInternetAppContext(DbContextOptions<forInternetAppContext> options)
     : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasColumnType("nchar(200)");

                entity.Property(e => e.BookInformation)
                    .HasColumnName("bookInformation")
                    .HasColumnType("nchar(1000)");

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasColumnName("bookName")
                    .HasColumnType("nchar(200)");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.HasKey(e => e.CommunicateId);

                entity.Property(e => e.CommunicateId).HasColumnName("communicateId");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Communication_Book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Communication)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Communication_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("nchar(200)");
            });
        }
    }
}
