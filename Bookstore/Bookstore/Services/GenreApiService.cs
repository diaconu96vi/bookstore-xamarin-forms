using Bookstore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class GenreApiService : BaseApiService
    {
        public GenreApiService() : base()
        {

        }

        public async Task<List<Genre>> GetAll()
        {
            var response = await HttpClient.GetAsync("Genre");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var genre = JsonConvert.DeserializeObject<List<Genre>>(str);
                return genre;
            }
            else
            {
                return null;
            }
        }
        public async Task<Genre> CreateAsync(Genre model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Genre", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var genre = JsonConvert.DeserializeObject<Genre>(str);
                return genre;
            }
            else
            {
                return null;
            }
        }
    }
}
