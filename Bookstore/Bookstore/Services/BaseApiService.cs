using Bookstore.ApplicationUtils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Bookstore.Services
{
    public abstract class BaseApiService
    {
        private HttpClient _httpClient;

        public BaseApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApplicationGeneralSettings.ApiUrl)
            };
        }

        protected HttpClient HttpClient => _httpClient;

    }
}
