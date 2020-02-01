using Bookstore.Models;
using Bookstore.Views.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.TabbedPages
{
    public class BasketPageViewModel : BaseViewModel
    {
        public Command ContinueCommand { get; set; }
        public ObservableCollection<BookView> BooksList { get; set; }

        public string TotalPrice { get; set; }
        public BasketPageViewModel()
        {
            ContinueCommand = new Command(async () => await ExecuteContinueCommand());
            BooksList = new ObservableCollection<BookView>();
            TotalPrice = "5 Lei";
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(BooksList));
        }

        public async Task ExecuteContinueCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreditCardPage(), true);
        }
    }
}
