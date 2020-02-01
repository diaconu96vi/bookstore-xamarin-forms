using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class CardApiService : BaseApiService
    {
        public CardApiService() : base()
        {

        }

        public async Task<List<Card>> GetAll()
        {
            var response = await HttpClient.GetAsync("Card");
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Cards = JsonConvert.DeserializeObject<List<Card>>(str);
                return Cards;
            }
            else
            {
                return new List<Card>();
            }
        }
        public async Task<Card> CreateAsync(Card model)
        {
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Card", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var Card = JsonConvert.DeserializeObject<Card>(str);
                return Card;
            }
            else
            {
                return null;
            }
        }
    }
}
