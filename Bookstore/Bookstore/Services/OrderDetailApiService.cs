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
    public class OrderDetailApiService : BaseApiService
    {
        public OrderDetailApiService() : base()
        {

        }

        public async Task<List<OrderDetail>> GetAll()
        {
            var response = await HttpClient.GetAsync("OrderDetail");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var OrderDetail = JsonConvert.DeserializeObject<List<OrderDetail>>(str);
                return OrderDetail;
            }
            else
            {
                return new List<OrderDetail>();
            }
        }

        public async Task<List<OrderDetail>> GetBookOrderDetails(int orderSysID)
        {
            var response = await HttpClient.GetAsync(string.Format("OrderDetail/GetBookOrderDetails/{0}", orderSysID));
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var OrderDetail = JsonConvert.DeserializeObject<List<OrderDetail>>(str);
                return OrderDetail;
            }
            else
            {
                return new List<OrderDetail>();
            }
        }
        public async Task<List<OrderDetail>> CreateAsync(List<OrderDetail> model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("OrderDetail", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var OrderDetail = JsonConvert.DeserializeObject<List<OrderDetail>>(str);
                return OrderDetail;
            }
            else
            {
                return new List<OrderDetail>();
            }
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetail model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("OrderDetail", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var OrderDetail = JsonConvert.DeserializeObject<OrderDetail>(str);
                return OrderDetail;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(string.Format("OrderDetail/{0}", id));
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
