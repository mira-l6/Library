using Microsoft.EntityFrameworkCore;
using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Library.App_Data
{
    public class LibraryDbContext : IdentityDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Category>()
                .HasMany(c=>c.ChildCategories)
                .WithOne(c=>c.ParentCategory)
                .HasForeignKey(c=> c.ParentId)
                .OnDelete(DeleteBehavior.ClientNoAction); // da proverq drugite opcii
        }
    }
}
