using Microsoft.EntityFrameworkCore;
using StoreManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Book{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(bc => bc.BookCategories).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(bc => bc.BookCategories).HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<BookAuthor>().HasKey(bc => new { bc.BookId, bc.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(bc => bc.Book).WithMany(bc => bc.BookAuthors).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(bc => bc.Author).WithMany(bc => bc.BookAuthors).HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Library>().HasKey(bc => new { bc.BookId, bc.CusId });
            modelBuilder.Entity<Library>().HasOne(bc => bc.Book).WithMany(bc => bc.Library).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<Library>().HasOne(bc => bc.User).WithMany(bc => bc.Library).HasForeignKey(b => b.CusId);

            modelBuilder.Entity<OrderBook>(entity =>
            {
                entity.HasKey(i => new
                {
                    i.BookId,
                    i.OrderId,
                });
            });

        }
    }
}