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
    public class CommentApiService : BaseApiService
    {
        public CommentApiService() : base()
        {

        }

        public async Task<List<Comment>> GetAll()
        {
            var response = await HttpClient.GetAsync("Comment");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Comment = JsonConvert.DeserializeObject<List<Comment>>(str);
                return Comment;
            }
            else
            {
                return new List<Comment>();
            }
        }

        public async Task<List<Comment>> GetBookComments(int bookID)
        {
            var response = await HttpClient.GetAsync(string.Format("Comment/GetBookComments/{0}", bookID));
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Comment = JsonConvert.DeserializeObject<List<Comment>>(str);
                return Comment;
            }
            else
            {
                return new List<Comment>(); ;
            }
        }
        public async Task<Comment> CreateAsync(Comment model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Comment", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Comment = JsonConvert.DeserializeObject<Comment>(str);
                return Comment;
            }
            else
            {
                return null;
            }
        }

        public async Task<Comment> UpdateAsync(Comment model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Comment", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Comment = JsonConvert.DeserializeObject<Comment>(str);
                return Comment;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Comment/{0}", id));
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
