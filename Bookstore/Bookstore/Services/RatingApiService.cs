using Bookstore.Models;
using Bookstore.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class RatingApiService : BaseApiService
    {
        public RatingApiService() : base()
        {

        }

        public async Task<List<Rating>> GetAll()
        {
            var response = await HttpClient.GetAsync("Rating");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Rating = JsonConvert.DeserializeObject<List<Rating>>(str);
                return Rating;
            }
            else
            {
                return new List<Rating>();
            }
        }
        
        public async Task<List<Rating>> GetBookRatings(int id)
        {
            var response = await HttpClient.GetAsync(string.Format("Rating/GetBookRatings/{0}", id));
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Rating = JsonConvert.DeserializeObject<List<Rating>>(str);
                return Rating;
            }
            else
            {
                return new List<Rating>();
            }
        }

        public async Task<Rating> GetUserBookRating(Rating model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await HttpClient.PostAsync("Rating/GetRatingUserBook", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Rating = JsonConvert.DeserializeObject<Rating>(str);
                return Rating;
            }
            else
            {
                return null;
            }
        }
        public async Task<Rating> CreateAsync(Rating model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Rating", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Rating = JsonConvert.DeserializeObject<Rating>(str);
                return Rating;
            }
            else
            {
                return null;
            }
        }

        public async Task<Rating> UpdateAsync(Rating model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Rating", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Rating = JsonConvert.DeserializeObject<Rating>(str);
                return Rating;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Rating/{0}", id));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
