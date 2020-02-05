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
    public class OrderApiService : BaseApiService
    {
        public OrderApiService() : base()
        {

        }

        public async Task<List<Order>> GetAll()
        {
            var response = await HttpClient.GetAsync("Order");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Order = JsonConvert.DeserializeObject<List<Order>>(str);
                return Order;
            }
            else
            {
                return new List<Order>();
            }
        }

        public async Task<List<Order>> GetUserOrders(Order userOrder)
        {
            var json = JsonConvert.SerializeObject(userOrder);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Order/GetUserOrders", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Order = JsonConvert.DeserializeObject<List<Order>>(str);
                return Order;
            }
            else
            {
                return new List<Order>(); ;
            }
        }
        public async Task<Order> CreateAsync(Order model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Order", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Order = JsonConvert.DeserializeObject<Order>(str);
                return Order;
            }
            else
            {
                return null;
            }
        }

        public async Task<Order> UpdateAsync(Order model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Order", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Order = JsonConvert.DeserializeObject<Order>(str);
                return Order;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("Order/{0}", id));
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
