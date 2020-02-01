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
    public class BookGenreApiService : BaseApiService
    {
        public BookGenreApiService() : base()
        {

        }

        public async Task<List<BookGenre>> GetAll()
        {
            var response = await HttpClient.GetAsync("BookGenre");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var genre = JsonConvert.DeserializeObject<List<BookGenre>>(str);
                return genre;
            }
            else
            {
                return null;
            }
        }

        public async Task<BookGenre> CreateAsync(BookGenre model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("BookGenre", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var bookGenre = JsonConvert.DeserializeObject<BookGenre>(str);
                return bookGenre;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("BookGenre/{0}", id));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<BookGenre>> GetByGenre(int genreId)
        {
            var response = await HttpClient.GetAsync(string.Format("BookGenre/Genre/{0}", genreId));
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var genre = JsonConvert.DeserializeObject<List<BookGenre>>(str);
                return genre;
            }
            else
            {
                return null;
            }
        }
    }
}
