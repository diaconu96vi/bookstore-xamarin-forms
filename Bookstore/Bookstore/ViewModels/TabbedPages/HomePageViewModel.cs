using Bookstore.Models;
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
        public HomePageViewModel()
        {
            CarouselImages = RetrieveCarouselImages();
            GenresList = new ObservableCollection<GenreView>()
            {
                new GenreView()
                {
                    GenreName = "Romance",
                    Image = "barberSeat.png"
                }
            };
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
