using DAL;
using Entities;
using Service.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e NewsService by inheriting INewsService

    // NewsService class is used to implement all input validation operations for News CRUD operations

    public class NewsService:INewsService
    {
        /*
         * this service depends on NewsRepository instance for the crud operations
         */
        INewsRepository _newsRepo;
        public NewsService(INewsRepository newsRepo)
        {
            _newsRepo = newsRepo;
        }

        /*
         * Implement AddNews() method which should be used to 
         * save a new News, provided the news does not exist, 
         * else should throw NewsAlreadyExistsException
         */
        public async Task<News> AddNews(News news)
        {
            bool exist = await _newsRepo.IsNewsExist(news);
            if (!exist)
            {
                return await _newsRepo.AddNews(news);

            }
            else
                throw new NewsAlreadyExistsException($"This news is already added");
        }

        /*
         * Implement GetAllNews() method which should be used to 
         * get all news details for the provided userId,
         * however, should throw NewsNotFoundException if no news exist for the provided userId
         */
        public async Task<List<News>> GetAllNews(string userId)
        {
            List<News> newsList = await _newsRepo.GetAllNews(userId);
            if (newsList.Count != 0)
                return newsList;
            else
                throw new NewsNotFoundException($"No news found for user: {userId}");
        }

        /*
         * Implement GetNewsById() method which should be used to 
         * get complete news details for the provided newsId,
         * however, should throw NewsNotFoundException if no news exist for the provided newsId
         */
        public async Task<News> GetNewsById(int newsId)
        {
            News news = await _newsRepo.GetNewsById(newsId);
            if (news != null)
                return news;
            else
                throw new NewsNotFoundException($"No news found with Id: {newsId}");
        }

        /*
         * Implement RemoveNews() method which should be used to 
         * delete an existing news
         * however, should throw NewsNotFoundException if news with provided newsId does not exist         * 
         */
        public async Task<bool> RemoveNews(int newsId)
        {
            News news = await _newsRepo.GetNewsById(newsId);
            if (news != null)
                return await _newsRepo.RemoveNews(news);
            else
                throw new NewsNotFoundException($"No news found with Id: {newsId}");
        }
    }
}
