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
    public class PublisherApiService : BaseApiService
    {
        public PublisherApiService() : base()
        {

        }

        public async Task<List<Publisher>> GetAll()
        {
            var response = await HttpClient.GetAsync("Publisher");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var publisher = JsonConvert.DeserializeObject<List<Publisher>>(str);
                return publisher;
            }
            else
            {
                return new List<Publisher>();
            }
        }
        public async Task<Publisher> CreateAsync(Publisher model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Publisher", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var publisher = JsonConvert.DeserializeObject<Publisher>(str);
                return publisher;
            }
            else
            {
                return null;
            }
        }

        public async Task<Publisher> UpdateAsync(Publisher model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Publisher", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var publisher = JsonConvert.DeserializeObject<Publisher>(str);
                return publisher;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Publisher/{0}", id));
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
