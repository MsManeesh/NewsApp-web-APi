using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DAL
{
    //Inherit the respective interface and implement the methods in
    // this class i.e NewsRepository by inheriting INewsRepository
    public class NewsRepository:INewsRepository
    {
        NewsDbContext _dbContext;
        public NewsRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<News> AddNews(News news)
        {
            _dbContext.NewsList.Add(news);
            await _dbContext.SaveChangesAsync();
            return news;
        }

        public async Task<List<News>> GetAllNews(string userId)
        {
            List<News>  newsList =await _dbContext.NewsList.Where(x => x.CreatedBy == userId).ToListAsync();
            return (newsList);
        }

        public async Task<News> GetNewsById(int newsId)
        {
            return await _dbContext.NewsList.FirstOrDefaultAsync(x => x.NewsId == newsId);
        }

        public async Task<bool> IsNewsExist(News news)
        {
            News exist =await _dbContext.NewsList.FirstOrDefaultAsync(x => x.Title == news.Title);
            if (exist != null)
                return true;
            else
                return false;
        }

        public async Task<bool> RemoveNews(News news)
        {
            _dbContext.NewsList.Remove(news);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        /* Implement all the methods of respective interface asynchronously*/
        /* Implement AddNews method to add the new news details*/
        /* Implement GetAllNews method to get the news details of perticular userid*/
        /* Implement GetNewsById method to get the existing news by news id*/
        /* Implement IsNewsExist method to check the news deatils exist or not*/
        /* Implement RemoveNews method to remove the existing news*/
    }
}
