using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebStore.Server.Models;



namespace WebStore.Server.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }
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

            List<IdentityRole> roles = new List<IdentityRole>
            { 
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
