using Bookstore.Converters;
using Bookstore.Models;
using Bookstore.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Bookstore.ViewModels.Admin
{
    public class ManageBooksPageViewModel : BaseViewModel
    {
        public Command SaveChangesCommand { get; set; }
        public Command DeleteBookCommand { get; set; }
        public Command AddImageCommand { get; set; }

        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public DateTime Date { get; set; }
        public string Price { get; set; }
        public ImageSource SelectedImage { get; set; }

        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Publisher> Publishers { get; set; }

        public Author SelectedAuthor { get; set; }
        public Publisher SelectedPublication { get; set; }
        public BookView SelectedBook { get; set; }

        public AuthorApiService authorApiService { get; set; }
        public PublisherApiService publisherApiService { get; set; }
        public BookApiService bookApiService { get; set; }

        private byte[] _addedImage;

        //Delete
        public ObservableCollection<BookView> Books { get; set; }
        public ManageBooksPageViewModel()
        {
            authorApiService = new AuthorApiService();
            bookApiService = new BookApiService();
            publisherApiService = new PublisherApiService();
            ConfigureAuthorsDataSource();
            ConfigurePublishersDataSource();
            ConfigureBooksDataSource();
            SaveChangesCommand = new Command(async () => await ExecuteSaveChangesCommand());
            AddImageCommand = new Command(async () => await ExecuteAddImageCommand());
            DeleteBookCommand = new Command(async () => await ExecuteDeleteBookCommand());
        }

        public async Task ExecuteDeleteBookCommand()
        {
            if (CheckDeleteValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var result = await bookApiService.DeleteAsync(SelectedBook.SysID);
                if (result)
                {
                    Books.Remove(SelectedBook);
                    SelectedBook = null;
                    OnPropertyChanged(nameof(Books));
                    OnPropertyChanged(nameof(SelectedBook));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Couldn't delete", "Cancel");
                }
            }
        }

        private bool CheckDeleteValuesEmpty()
        {
            if(SelectedBook == null)
            {
                return false;
            }
            return false;
        }
        public async Task ExecuteSaveChangesCommand()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            var book = new Book()
            {
                Title = BookTitle,
                ISBN = ISBN,
                Price = int.Parse(Price),
                Image = _addedImage,
                AuthorFK_SysID = SelectedAuthor.SysID,
                PublisherFK_SysID = SelectedPublication.SysID,
                PublicationDate = Date
            };
            var result = await bookApiService.CreateAsync(book);
            if (result != null)
            {
                BookView newBook = new BookView()
                {
                    SysID = result.BookSysID,
                    Title = result.Title,
                };
                Books.Add(newBook);
                OnPropertyChanged(nameof(Books));
                ClearElements();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        private void ClearElements()
        {
            BookTitle = string.Empty;
            Date = DateTime.Today.Date;
            Price = null;
            ISBN = null;
            SelectedAuthor = null;
            SelectedPublication = null;
            SelectedImage = null;
            OnPropertyChanged(nameof(BookTitle));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(SelectedAuthor));
            OnPropertyChanged(nameof(SelectedPublication));
            OnPropertyChanged(nameof(SelectedImage));

        }

        private bool CheckAddValuesEmpty()
        {
            if(SelectedAuthor == null || SelectedPublication == null || SelectedImage == null || string.IsNullOrEmpty(BookTitle) && Date == null || string.IsNullOrEmpty(Price))
            {
                return true;
            }
            return false;
        }
        public async Task ExecuteAddImageCommand()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Media not supported on this device", "Ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Small
            };
            var selectedFileImage = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            if(selectedFileImage == null)
            {
                return;
            }
            var stream = selectedFileImage.GetStream();
            _addedImage = BitmapConverter.StreamToByte(stream);
            SelectedImage = ImageSource.FromStream(() => stream);
            OnPropertyChanged(nameof(SelectedImage));
        }

        private async void ConfigureAuthorsDataSource()
        {
            var authorsApi = await authorApiService.GetAll();
            if (authorsApi == null && authorsApi.Any())
            {
                return;
            }
            Authors = new ObservableCollection<Author>(authorsApi);
            OnPropertyChanged(nameof(Authors));
        }        

        private async void ConfigurePublishersDataSource()
        {
            var publishersApi = await publisherApiService.GetAll();
            if (publishersApi == null && publishersApi.Any())
            {
                return;
            }
            Publishers = new ObservableCollection<Publisher>(publishersApi);
            OnPropertyChanged(nameof(Publishers));
        }
        
        private async void ConfigureBooksDataSource()
        {
            var booksApi = await bookApiService.GetAll();
            if (booksApi == null && booksApi.Any())
            {
                return;
            }
            ObservableCollection<BookView> newBooks = new ObservableCollection<BookView>();
            foreach(var book in booksApi.ToList())
            {
                var newBook = new BookView()
                {
                    SysID = book.BookSysID,
                    Title = book.Title
                };
                newBooks.Add(newBook);
            }
            Books = newBooks;
            OnPropertyChanged(nameof(Books));
        }
    }
}
