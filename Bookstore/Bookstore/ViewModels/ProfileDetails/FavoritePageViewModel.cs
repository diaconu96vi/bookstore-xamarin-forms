using Bookstore.ApplicationUtils;
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
    public class FavoritePageViewModel : BaseViewModel
    {
        public ObservableCollection<BookView> BooksList { get; set; }
        private FavoriteApiService _favoriteApiService { get; set; }
        private List<Favorite> _userFavorites { get; set; }
        public FavoritePageViewModel()
        {
            _favoriteApiService = new FavoriteApiService();
            ConfigureFavoriteList();
        }
        private async void ConfigureFavoriteList()
        {
            var favorites = await _favoriteApiService.GetAll();
            if(favorites == null)
            {
                _userFavorites = new List<Favorite>();
            }
            else
            {
                _userFavorites = favorites.Where(x => x.AppUserFK_SysID.Equals(ApplicationGeneralSettings.CurrentUser.Id)).ToList();
                ObservableCollection<BookView> booksCopy = new ObservableCollection<BookView>();
                foreach (var favorite in _userFavorites)
                {
                    var bookView = new BookView()
                    {
                        SysID = favorite.Book.BookSysID,
                        Title = favorite.Book.Title,
                        AuthorName = favorite.Book.Author.Name,
                        Price = favorite.Book.Price.ToString(),
                        Image = BitmapConverter.ByteToImageSource(favorite.Book.Image)
                    };
                    booksCopy.Add(bookView);
                }
                BooksList = new ObservableCollection<BookView>(booksCopy);
                OnPropertyChanged(nameof(BooksList));
            }
        }

        public async void RemoveFavorite(string bookSysID)
        {
            if(string.IsNullOrEmpty(bookSysID))
            {
                return;
            }
            int.Parse(bookSysID);
            var result = await _favoriteApiService.DeleteAsync(_userFavorites.FirstOrDefault(x => x.BookFK_SysID == int.Parse(bookSysID)).FavoriteSysID); 
            if(result)
            {
                var book = BooksList.FirstOrDefault(x => x.SysID == int.Parse(bookSysID));
                BooksList.Remove(book);
                OnPropertyChanged(nameof(book));
            }
        }

        public async Task ExecuteBookDetail(string SysID)
        {
            var selectedBook = BooksList.FirstOrDefault(x => x.SysID.Equals(int.Parse(SysID)));
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
