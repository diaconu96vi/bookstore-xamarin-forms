using Bookstore.Models;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Admin
{
    public class ManageBookGenresPageViewModel : BaseViewModel
    {
        public Command AddBookGenreCommand { get; set; }
        public Command DeleteBookGenreCommand { get; set; }

        public string AddBookGenreName { get; set; }

        public Book SelectedBook { get; set; }

        public Genre SelectedGenre { get; set; }

        public BookGenre SelectedDeleteBookGenre { get; set; }

        public ObservableCollection<Book> Books { get; set; }

        public ObservableCollection<Genre> Genres { get; set; }

        public ObservableCollection<BookGenre> BookGenres { get; set; }

        private BookApiService _booksApiService;

        private GenreApiService _genreApiService;

        private BookGenreApiService _bookGenreApiService;

        public ManageBookGenresPageViewModel()
        {
            _booksApiService = new BookApiService();
            _genreApiService = new GenreApiService();
            _bookGenreApiService = new BookGenreApiService();

            ConfigureBooksDataSource();
            ConfigureGenresDataSource();
            ConfigureBookGenresDataSource();

            AddBookGenreCommand = new Command(async () => await ExecuteAddBookGenreCommand());
            DeleteBookGenreCommand = new Command(async () => await ExecuteDeleteBookGenreCommand());
        }

        public async Task ExecuteAddBookGenreCommand()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            var model = new BookGenre()
            {
                Name = AddBookGenreName,
                BookFK_SysID = SelectedBook.BookSysID,
                GenreFK_SysID = SelectedGenre.GenreSysID
            };

            if (CheckExistingItem(model))
            {
                await Application.Current.MainPage.DisplayAlert("Duplicate Item", "A book genre with the selected values already exists", "OK");
            }

            var result = await _bookGenreApiService.CreateAsync(model);
            if (result != null)
            {
                BookGenres.Add(result);
                ClearAddElements();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        public async Task ExecuteDeleteBookGenreCommand()
        {
            if (SelectedDeleteBookGenre == null)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var result = await _bookGenreApiService.DeleteAsync(SelectedDeleteBookGenre.BookGenreSysID);
                if (result)
                {
                    BookGenres.Remove(SelectedDeleteBookGenre);
                    SelectedDeleteBookGenre = null;
                    OnPropertyChanged(nameof(BookGenres));
                    OnPropertyChanged(nameof(SelectedDeleteBookGenre));
                }
            }
        }

        private async void ConfigureBooksDataSource()
        {
            var booksList = await _booksApiService.GetAll();
            if (booksList != null && !booksList.Any())
            {
                return;
            }
            Books = new ObservableCollection<Book>(booksList);
            OnPropertyChanged(nameof(Books));
        }

        private async void ConfigureGenresDataSource()
        {
            var genresList = await _genreApiService.GetAll();
            if (genresList != null && !genresList.Any())
            {
                return;
            }
            Genres = new ObservableCollection<Genre>(genresList);
            OnPropertyChanged(nameof(Genres));
        }

        private async void ConfigureBookGenresDataSource()
        {
            var bookGenresList = await _bookGenreApiService.GetAll();
            if (bookGenresList == null)
            {
                BookGenres = new ObservableCollection<BookGenre>();
                return;
            }
            BookGenres = new ObservableCollection<BookGenre>(bookGenresList);
            OnPropertyChanged(nameof(BookGenres));
        }

        private bool CheckAddValuesEmpty()
        {
            if (string.IsNullOrEmpty(AddBookGenreName) || SelectedBook == null || SelectedGenre == null)
            {
                return true;
            }
            return false;
        }

        private void ClearAddElements()
        {
            AddBookGenreName = string.Empty;
            SelectedBook = null;
            SelectedGenre = null;
            OnPropertyChanged(nameof(AddBookGenreName));
            OnPropertyChanged(nameof(SelectedBook));
            OnPropertyChanged(nameof(SelectedGenre));
        }

        private bool CheckExistingItem(BookGenre model)
        {
            if (BookGenres.Any(x => x.BookFK_SysID == model.BookFK_SysID && x.GenreFK_SysID == model.GenreFK_SysID))
                return true;
            else
                return false;
        }
    }
}
