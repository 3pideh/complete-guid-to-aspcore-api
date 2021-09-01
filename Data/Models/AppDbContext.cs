using Microsoft.EntityFrameworkCore;
using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author_Book>() 
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Author_Books)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Author_Book>()
               .HasOne(b => b.Author)
               .WithMany(ba => ba.Author_Books)
               .HasForeignKey(bi => bi.AuthorId);

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
       
        public DbSet<Author_Book> Authors_Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
