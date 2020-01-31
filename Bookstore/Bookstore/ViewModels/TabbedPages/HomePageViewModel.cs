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
        public ObservableCollection<Author> AuthorsList { get; set; }

        public BookView SelectedBook { get; set; }
        public GenreView SelectedGenre { get; set; }

        public GenreApiService _genreApiService { get; set; }
        public BookApiService _bookApiService { get; set; }
        public AuthorApiService _authorApiService { get; set; }
        public PublisherApiService _publisherApiService { get; set; }

        public string SearchBarText { get; set; }

        private ObservableCollection<BookView> AllBooks { get; set; }

        public HomePageViewModel()
        {
            _genreApiService = new GenreApiService();
            _bookApiService = new BookApiService();
            _authorApiService = new AuthorApiService();
            _publisherApiService = new PublisherApiService();

            CarouselImages = RetrieveCarouselImages();
            ConfigureGenresListDataSource();
            ConfigureBooksListDataSource();
            ConfigureAuthorsListDataSource();
        }

        #region DataRetrievalForLists

        private async void ConfigureAuthorsListDataSource()
        {
            var allAuthors = await _authorApiService.GetAll();
            if(allAuthors == null || !allAuthors.Any())
            {
                return;
            }
            AuthorsList = new ObservableCollection<Author>(allAuthors);
            OnPropertyChanged(nameof(AuthorsList));
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
            AllBooks = convertedList.CloneJson();
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

        #endregion

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

        #region Filters
        public void ExecuteGenreDetail(string genreName)
        {
            var selectedGenre = GenresList.FirstOrDefault(x => x.GenreName.Equals(genreName));
            if (selectedGenre != null)
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new BookDetailPage(selectedBook));
            }
            else
            {
            }
        }       
        
        public void ApplyAuthorFilter(string authorName)
        {
            var filteredBooks = BooksList.Where(x => x.AuthorName.Equals(authorName));
            if (filteredBooks == null)
            {
                BooksList = null;
            }
            else
            {
                BooksList = new ObservableCollection<BookView>(filteredBooks);
            }
            OnPropertyChanged(nameof(BooksList));
        }

        public void FilterSearchBar()
        {
            if(string.IsNullOrEmpty(SearchBarText))
            {
                return;
            }
            else
            {
                var filteredBooks = BooksList.Where(x => x.Title.Contains(SearchBarText)).ToList();
                if(filteredBooks == null)
                {
                    BooksList = null;
                }
                else
                {
                    BooksList = new ObservableCollection<BookView>(filteredBooks);
                }
            }
            OnPropertyChanged(nameof(BooksList));
        }
        #endregion
    }
}
