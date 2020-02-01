using Bookstore.Models;
using Bookstore.Services;
using Bookstore.ViewModels.Order;
using Bookstore.Views.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.TabbedPages
{
    public class BasketPageViewModel : BaseViewModel
    {
        public Command ContinueCommand { get; set; }
        public ObservableCollection<BookView> BooksList { get; set; }
        public ObservableCollection<Address> AddressList { get; set; }

        public string TotalPrice { get; set; }

        public Address SelectedAddress { get; set; }

        private AddressApiService _addressApiService { get; set; } 
        public BasketPageViewModel()
        {
            _addressApiService = new AddressApiService();
            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
            TotalPrice = "5 Lei";
            MessagingCenter.Subscribe<Address>(this, "AddAddress", (address) =>
            {
                AddressList.Add(address);
            });
            ConfigureAddressList();
        }

        public async Task ExecuteContinueCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreditCardPage(), true);
        }

        public async void ConfigureAddressList()
        {
            var addresses = await _addressApiService.GetAll();
            AddressList = new ObservableCollection<Address>(addresses);
            OnPropertyChanged(nameof(AddressList));
        }
    }
}
