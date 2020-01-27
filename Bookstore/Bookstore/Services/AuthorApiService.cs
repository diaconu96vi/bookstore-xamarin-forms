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
    public class AuthorApiService : BaseApiService
    {
        public AuthorApiService() : base()
        {

        }

        public async Task<List<Author>> GetAll()
        {
            var response = await HttpClient.GetAsync("Author");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var authors = JsonConvert.DeserializeObject<List<Author>>(str);
                return authors;
            }
            else
            {
                return null;
            }
        }
        public async Task<Author> CreateAsync(Author model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Author", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var author = JsonConvert.DeserializeObject<Author>(str);
                return author;
            }
            else
            {
                return null;
            }
        }

        public async Task<Author> UpdateAsync(Author model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Author", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var author = JsonConvert.DeserializeObject<Author>(str);
                return author;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Author/{0}", id));
            if(response.IsSuccessStatusCode)
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
