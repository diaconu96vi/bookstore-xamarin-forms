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
    public class BookApiService : BaseApiService
    {
        public BookApiService() : base()
        {

        }

        public async Task<List<Book>> GetAll()
        {
            var response = await HttpClient.GetAsync("Book");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Book = JsonConvert.DeserializeObject<List<Book>>(str);
                return Book;
            }
            else
            {
                return new List<Book>();
            }
        }     
        
        public async Task<Book> GetBook(int BookID)
        {
            var response = await HttpClient.GetAsync(string.Format("Book/GetBook{0}", BookID));
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Book = JsonConvert.DeserializeObject<Book>(str);
                return Book;
            }
            else
            {
                return null;
            }
        }
        public async Task<Book> CreateAsync(Book model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Book", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Book = JsonConvert.DeserializeObject<Book>(str);
                return Book;
            }
            else
            {
                return null;
            }
        }

        public async Task<Book> UpdateAsync(Book model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Book", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Book = JsonConvert.DeserializeObject<Book>(str);
                return Book;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Book/{0}", id));
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
