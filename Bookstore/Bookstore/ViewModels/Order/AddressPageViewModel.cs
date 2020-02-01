using Bookstore.ApplicationUtils;
using Bookstore.Models;
using Bookstore.Services;
using Bookstore.ViewModels.TabbedPages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Order
{
    public class AddressPageViewModel : BaseViewModel
    {
        public string AddressTitle { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }

        public Command AddNewAddress { get; set; }

        private AddressApiService _addressApiService { get; set; }
        public AddressPageViewModel()
        {
            _addressApiService = new AddressApiService();
            AddNewAddress = new Command(async () => await ExecuteAddNewAddress());
        }

        public async Task ExecuteAddNewAddress()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
                return;
            }
            var model = new Address()
            {
                AddressTitle = AddressTitle,
                FullName = FullName,
                City = City,
                Country = Country,
                FullAddress = FullAddress,
                AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id
            };
            var result = await _addressApiService.CreateAsync(model);
            if (result != null)
            {
                MessagingCenter.Send<Address>(result, "AddAddress");
                await Application.Current.MainPage.Navigation.PopModalAsync(true);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        private bool CheckAddValuesEmpty()
        {
            if(string.IsNullOrEmpty(AddressTitle) || string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Country) || string.IsNullOrEmpty(FullAddress))
            {
                return true;
            }
            return false;
        }
    }
}
