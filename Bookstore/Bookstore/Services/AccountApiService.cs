﻿using Bookstore.ApplicationUtils;
using Bookstore.Models;
using Bookstore.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bookstore.Services
{
    public class AccountApiService : BaseApiService
    {
        public AccountApiService() : base()
        {

        }
        public async Task<Tuple<bool, List<IdentityError>>> RegisterAsync(SignupModel model)
        {
            AdminCreator.CreateAdmin(model);
            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Account/Register", content);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                var appUser = JsonConvert.DeserializeObject<AppUser>(str);
                ApplicationGeneralSettings.CurrentUser = appUser;
                await SecureStorage.SetAsync("User", str);
                return new Tuple<bool, List<IdentityError>>(true, null);
            }
            else
            {
                var str = await response.Content.ReadAsStringAsync();
                var myInstance = JsonConvert.DeserializeObject<List<IdentityError>>(str);
                return new Tuple<bool, List<IdentityError>>(false, myInstance);
            }
        }

        public async Task<bool> LoginAsync(LoginModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Account/Login", content);
            var str = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var appUser = JsonConvert.DeserializeObject<AppUser>(str);
                ApplicationGeneralSettings.CurrentUser = appUser;
                await SecureStorage.SetAsync("User", str);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AppUser> GetUser(LoginModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Account/GetUserByEmail", content);
            var str = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var appUser = JsonConvert.DeserializeObject<AppUser>(str);
                ApplicationGeneralSettings.CurrentUser = appUser;
                return appUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<Tuple<AppUser, List<IdentityError>>> ChangePassword(ChangePasswordModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync("Account/ChangePassword", content);
            var str = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var appUser = JsonConvert.DeserializeObject<AppUser>(str);
                ApplicationGeneralSettings.CurrentUser = appUser;
                return new Tuple<AppUser, List<IdentityError>>(appUser, null);
            }
            else
            {
                var myInstance = JsonConvert.DeserializeObject<List<IdentityError>>(str);
                return new Tuple<AppUser, List<IdentityError>>(null, myInstance);
            }
        }

        public async Task<bool> ChangeUserDetails(UserDetailsModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PutAsync("Account/ChangeUserDetails", content);
            var str = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var appUser = JsonConvert.DeserializeObject<AppUser>(str);
                ApplicationGeneralSettings.CurrentUser = appUser;
                return true;
            }

            return false;
        }
    }
}
