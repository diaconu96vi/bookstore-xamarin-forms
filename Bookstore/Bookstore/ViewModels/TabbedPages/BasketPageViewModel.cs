using Bookstore.ApplicationUtils;
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
        public string ShippingPrice { get; set; }

        public Address SelectedAddress { get; set; }

        private AddressApiService _addressApiService { get; set; } 
        public BasketPageViewModel()
        {
            _addressApiService = new AddressApiService();
            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
            MessagingCenter.Subscribe<Address>(this, "AddAddress", (address) =>
            {
                AddressList.Add(address);
            });
            MessagingCenter.Subscribe<BookView>(this, "AddBasketRefresh", (bookView) =>
            {
                RefreshBasket();
            });
            ConfigureAddressList();
            ConfigureShoppingCart();
        }
        private void RefreshBasket()
        {
            BooksList = new ObservableCollection<BookView>(ShoppingBasket.Instance.AddedOrderItems);
            TotalPrice = string.Format("+ {0} Lei", ShoppingBasket.Instance.TotalPrice);
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(BooksList));
        }
        public async Task ExecuteContinueCommand()
        {
            if(SelectedAddress == null)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected address", "Cancel");
                return;
            }
            else if(BooksList == null || !BooksList.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No books in cart", "Cancel");
                return;
            }
            ShoppingBasket.Instance.ActiveAddress = SelectedAddress;
            await Application.Current.MainPage.Navigation.PushAsync(new CreditCardPage(), true);
        }

        public void ConfigureShoppingCart()
        {
            BooksList = new ObservableCollection<BookView>(ShoppingBasket.Instance.AddedOrderItems);
            TotalPrice = string.Format("+ {0} Lei", ShoppingBasket.Instance.TotalPrice);
            ShippingPrice = string.Format("{0} Lei", ShoppingBasket.Instance.ShippingPrice);
            OnPropertyChanged(nameof(BooksList));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(ShippingPrice));
        }

        public async void ConfigureAddressList()
        {
            var addresses = await _addressApiService.GetAll();
            AddressList = new ObservableCollection<Address>(addresses.Where(x => x.AppUserFK_SysID.Equals(ApplicationGeneralSettings.CurrentUser.Id)).ToList());
            OnPropertyChanged(nameof(AddressList));
        }

        public async void RemoveProduct(string bookSysID)
        {
            if(string.IsNullOrEmpty(bookSysID))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected book", "Cancel");
            }
            var bookView = BooksList.FirstOrDefault(x => x.SysID == int.Parse(bookSysID));
            if(bookView == null)
            {
                return;
            }
            ShoppingBasket.Instance.Delete(bookView);
            RefreshBasket();
        }
    }
}
