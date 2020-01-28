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

namespace Bookstore.ViewModels.TabbedPages
{
    public class HomePageViewModel : BaseViewModel
    {

        public ObservableCollection<Models.CarouselView> CarouselImages { get; set; }
        public ObservableCollection<GenreView> GenresList { get; set; }
        public ObservableCollection<BookView> BooksList { get; set; }

        public BookView SelectedBook { get; set; }
        public GenreView SelectedGenre { get; set; }

        public GenreApiService _genreApiService { get; set; }
        public BookApiService _bookApiService { get; set; }
        public AuthorApiService _authorApiService { get; set; }
        public PublisherApiService _publisherApiService { get; set; }

        public HomePageViewModel()
        {
            _genreApiService = new GenreApiService();
            _bookApiService = new BookApiService();
            _authorApiService = new AuthorApiService();
            _publisherApiService = new PublisherApiService();
            CarouselImages = RetrieveCarouselImages();
            ConfigureGenresListDataSource();
            ConfigureBooksListDataSource();

            BooksList = new ObservableCollection<BookView>()
            {
                new BookView()
                {
                    Title = "Morometii",
                    AuthorName = "Marin Preda",
                    Price = "30",
                    Image = "machine.jpg"
                }
            };
        }
        
        private async void ConfigureGenresListDataSource()
        {
            var allGenres = await _genreApiService.GetAll();
            if(allGenres == null && allGenres.Any())
            {
                return;
            }
            var convertedList = new ObservableCollection<GenreView>();
            foreach (var genre in allGenres.ToList())
            {
                var convertGenre = new GenreView()
                {
                    SysID = genre.GenreSysID,
                    GenreName = genre.Name,
                    Image = BitmapConverter.ByteToImageSource(genre.Image)
                };
                convertedList.Add(convertGenre);
            }
            GenresList = convertedList;
            OnPropertyChanged(nameof(GenresList));
        }

        private async void ConfigureBooksListDataSource()
        {
            var allBooks = await _bookApiService.GetAll();
            if(allBooks == null && allBooks.Any())
            {
                return;
            }
            var convertedList = new ObservableCollection<BookView>();
            foreach (var book in allBooks.ToList())
            {
                var author = await _authorApiService.GetRecordAsync(book.AuthorFK_SysID);
                var publisher = await _publisherApiService.GetRecordAsync(book.PublisherFK_SysID);
                var convertBook = new BookView()
                {
                    SysID = book.BookSysID,
                    Title = book.Title,
                    AuthorName = author.Name,
                    Price = book.Price.ToString(),
                    PublicationDate = book.PublicationDate,
                    PublicationName = publisher.Name,
                    Image = BitmapConverter.ByteToImageSource(book.Image)
                };
                convertedList.Add(convertBook);
            }
            BooksList = convertedList;
            OnPropertyChanged(nameof(BooksList));
        }

        private ObservableCollection<Models.CarouselView> RetrieveCarouselImages()
        {
            return new ObservableCollection<Models.CarouselView>()
            {
                new Models.CarouselView()
                {
                    CarouselImage = "bookshop_carousel1.jpg"
                },
                new Models.CarouselView()
                {
                    CarouselImage = "bookshop_carousel2.jpg"
                },
                new Models.CarouselView()
                {
                    CarouselImage = "bookshop_carousel3.jpg"
                },
                new Models.CarouselView()
                {
                    CarouselImage = "bookshop_carousel4.jpg"
                }
            };
        }   

        public async Task ExecuteBookDetail(string bookTitle)
        {
            var selectedBook = BooksList.FirstOrDefault(x => x.Title.Equals(bookTitle));
            if(selectedBook != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new BookDetailPage(selectedBook));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected book", "Ok");
            }
            
        }

        public async Task ExecuteGenreDetail(string genreName)
        {
            var selectedBook = GenresList.FirstOrDefault(x => x.GenreName.Equals(genreName));
            if (selectedBook != null)
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new BookDetailPage(selectedBook));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected book", "Ok");
            }
        }
    }
}
