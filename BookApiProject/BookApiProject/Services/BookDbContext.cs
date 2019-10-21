using BookApiProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiProject.Services
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
            //Create the Migrations - allow any changes done from code side to be migrated to db
            Database.Migrate(); 
        }

        //Creates tables from models
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }

        // create the many to many relationships
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            // Many to Many in the BookCategories

            // establish entities then keys 
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId }); // primary keys for book and category
            // establish relationship from point of Book
            modelBuilder.Entity<BookCategory>() // in BookCategory
                .HasOne(b => b.Book) // we have one book 
                .WithMany(bc => bc.BookCategories) // with many categories
                .HasForeignKey(b => b.BookId); // and foreign key is the BookId
            // establish relationship from point of Categories
            modelBuilder.Entity<BookCategory>() // in BookCategory
                .HasOne(c => c.Category) // we have one book 
                .WithMany(bc => bc.BookCategories) // with many categories
                .HasForeignKey(c => c.CategoryId); // and foreign key is the BookId
        }

    }
}
