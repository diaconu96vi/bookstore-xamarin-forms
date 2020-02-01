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
    public class AddressApiService : BaseApiService
    {
        public AddressApiService() : base()
        {

        }

        public async Task<List<Address>> GetAll()
        {
            var response = await HttpClient.GetAsync("Address");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Addresss = JsonConvert.DeserializeObject<List<Address>>(str);
                return Addresss;
            }
            else
            {
                return new List<Address>();
            }
        }
        public async Task<Address> CreateAsync(Address model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Address", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Address = JsonConvert.DeserializeObject<Address>(str);
                return Address;
            }
            else
            {
                return null;
            }
        }
    }
}
