using Bookstore.Converters;
using Bookstore.Models;
using Bookstore.Services;
using Bookstore.Views.DetailPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.ProfileDetails
{
    public class OrderDetailPageViewModel : BaseViewModel
    {
        public ObservableCollection<BookView> BooksList { get; set; }
        public ObservableCollection<BookView> _initialBookList { get; set; }
        private int _orderSysID { get; set; }

        private OrderDetailApiService _orderDetailApiService { get; set; }
        private AuthorApiService _authorApiService { get; set; }
        private PublisherApiService _publisherApiService { get; set; }
        public OrderDetailPageViewModel(int OrderSysID)
        {
            _orderSysID = OrderSysID;
            _orderDetailApiService = new OrderDetailApiService();
            _authorApiService = new AuthorApiService();
            _publisherApiService = new PublisherApiService();
            ConfigureBooksListDataSource();
        }

        public async void ConfigureBooksListDataSource()
        {
            var orderBooks = await _orderDetailApiService.GetBookOrderDetails(_orderSysID);
            if(orderBooks == null || !orderBooks.Any())
            {
                return;
            }
            ObservableCollection<BookView> booksCopy = new ObservableCollection<BookView>();
            _initialBookList = new ObservableCollection<BookView>();
            foreach (var orderDetail in orderBooks)
            {
                var bookView = new BookView()
                {
                    SysID = orderDetail.Book.BookSysID,
                    Title = orderDetail.Book.Title,
                    Image = BitmapConverter.ByteToImageSource(orderDetail.Book.Image),
                    Price = orderDetail.Book.Price.ToString(),
                    PublicationDate = orderDetail.Book.PublicationDate
                };
                var _initialBookView = new BookView()
                {
                    SysID = orderDetail.Book.BookSysID,
                    Title = orderDetail.Book.Title,
                    Image = BitmapConverter.ByteToImageSource(orderDetail.Book.Image),
                    Price = orderDetail.Book.Price.ToString(),
                    PublicationDate = orderDetail.Book.PublicationDate
                };
                var author = await _authorApiService.GetRecordAsync(orderDetail.Book.AuthorFK_SysID);
                var publisher = await _publisherApiService.GetRecordAsync(orderDetail.Book.PublisherFK_SysID);
                if (author != null)
                {
                    _initialBookView.AuthorName = author.Name;
                    bookView.AuthorName = author.Name;
                }
                if (publisher != null)
                {
                    _initialBookView.PublicationName = author.Name;
                    bookView.PublicationName = publisher.Name;
                }

                _initialBookList.Add(_initialBookView);
                bookView.Price = string.Format("{0} x {1}", orderDetail.Book.Price.ToString(), orderDetail.Quantity.ToString());
                booksCopy.Add(bookView);
                
            }
            BooksList = new ObservableCollection<BookView>(booksCopy);
            OnPropertyChanged(nameof(BooksList));
        }

        public async Task ExecuteBookDetail(string SysID)
        {
            var selectedBook = _initialBookList.FirstOrDefault(x => x.SysID.Equals(int.Parse(SysID)));
            if (selectedBook != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new BookDetailPage(selectedBook));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected book", "Ok");
            }
        }
    }
}
