using Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    //Inherit DbContext class and use Entity Framework Code First Approach
    public class NewsDbContext:DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> option):base(option)
        {

        }
        /*
        This class should be used as DbContext to speak to database and should make the use of 
        Code First Approach. It should autogenerate the database based upon the model class in 
        your application
        */
        public DbSet<News> NewsList { get; set; }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        //Create a Dbset for News,USerProfile and Reminders

        /*Override OnModelCreating function to configure relationship between entities and initialize*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var reminder = modelBuilder.Entity<Reminder>();
            reminder.HasOne(x => x.News).WithMany(x => x.Reminders).HasForeignKey(x => x.NewsId).IsRequired();
            var news = modelBuilder.Entity<News>();

            news.HasOne(x => x.User).WithMany(x => x.News).HasForeignKey(x => x.CreatedBy).IsRequired();
            var User = modelBuilder.Entity<UserProfile>();
            User.HasKey(x => x.UserId);
        }
    }
}

