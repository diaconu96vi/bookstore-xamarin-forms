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
    public class FavoriteApiService : BaseApiService
    {
        public FavoriteApiService() : base()
        {

        }

        public async Task<List<Favorite>> GetAll()
        {
            var response = await HttpClient.GetAsync("Favorite");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Favorite = JsonConvert.DeserializeObject<List<Favorite>>(str);
                return Favorite;
            }
            else
            {
                return new List<Favorite>();
            }
        }

        public async Task<Favorite> GetFavorite(Favorite model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await HttpClient.PostAsync("Favorite/GetFavoriteUserBook", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Favorite = JsonConvert.DeserializeObject<Favorite>(str);
                return Favorite;
            }
            else
            {
                return null;
            }
        }
        public async Task<Favorite> CreateAsync(Favorite model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Favorite", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Favorite = JsonConvert.DeserializeObject<Favorite>(str);
                return Favorite;
            }
            else
            {
                return null;
            }
        }

        public async Task<Favorite> UpdateAsync(Favorite model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Favorite", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Favorite = JsonConvert.DeserializeObject<Favorite>(str);
                return Favorite;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Favorite/{0}", id));
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
