using Entities;
using NewsAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;
namespace Test.ControllerTests.IntegrationTest
{
    [Collection("News Fixture")]
    [TestCaseOrderer("Test.PriorityOrderer", "test")]
    public class NewsControllerTest
    {
        private readonly HttpClient _client;
        public NewsControllerTest(NewsWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact, TestPriority(1)]
        public async Task GetByIdShouldReturnNews()
        {
            // The endpoint or route of the controller action.
            int newsId =101;
            var httpResponse = await _client.GetAsync($"/api/news/{newsId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var news= JsonConvert.DeserializeObject<News>(stringResponse);
            Assert.Equal("IT industry in 2020", news.Title);
        }

        [Fact, TestPriority(2)]
        public async Task PostShouldReturnNews()
        {
            // The endpoint or route of the controller action.
            News news = new News { Title = "chandrayaan2-spacecraft", Content = "The Lander of Chandrayaan-2 was named Vikram after Dr Vikram A Sarabhai", PublishedAt = DateTime.Now, CreatedBy = "Jack" };
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            var httpResponse = await _client.PostAsync($"/api/news",news,formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<News>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsAssignableFrom<News>(actual);
            Assert.Equal(103, actual.NewsId);
        }

        [Fact, TestPriority(3)]
        public async Task GetShouldReturnNewsList()
        {
            // The endpoint or route of the controller action.
            string userId = "Jack";
            var httpResponse = await _client.GetAsync($"/api/news/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var lstNews = JsonConvert.DeserializeObject<IList<News>>(stringResponse);
            Assert.Equal(3, lstNews.Count);
        }
        [Fact, TestPriority(4)]
        public async Task DeleteShouldSuccess()
        {
            // The endpoint or route of the controller action.
            int newsId = 102;
            var httpResponse = await _client.DeleteAsync($"/api/news/{newsId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(5)]
        public async Task GetShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            string userId = "Kevin";
            var httpResponse = await _client.GetAsync($"/api/news/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"No news found for user: {userId}", stringResponse);
        }

        [Fact, TestPriority(6)]
        public async Task GetByIdShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            int newsId = 104;
            var httpResponse = await _client.GetAsync($"/api/news/{newsId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"No news found with Id: {newsId}", stringResponse);
        }

        [Fact, TestPriority(7)]
        public async Task PostShouldReturnConflict()
        {
            // The endpoint or route of the controller action.
            News news = new News { Title = "IT industry in 2020", Content = "It is expected to have positive growth in 2020.", PublishedAt = DateTime.Now, UrlToImage = null, CreatedBy = "Jack", Url = null };
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            var httpResponse = await _client.PostAsync($"/api/news", news, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Conflict, httpResponse.StatusCode);
            Assert.Equal($"This news is already added", stringResponse);
        }
        [Fact, TestPriority(8)]
        public async Task DeleteShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            int newsId = 104;
            var httpResponse = await _client.DeleteAsync($"/api/news/{newsId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"No news found with Id: {newsId}", stringResponse);
        }
    }
}
