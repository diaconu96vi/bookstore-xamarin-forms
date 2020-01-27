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

namespace Bookstore.ViewModels.Admin
{
    public class ManageGenresPageViewModel : BaseViewModel
    {
        public Command AddGenresCommand { get; set; }
        public Command UpdateGenresCommand { get; set; }
        public Command DeleteGenresCommand { get; set; }

        public Command AddGenreImageCommand { get; set; }
        public Command UpdateGenreImageCommand { get; set; }

        public GenreView SelectedUpdateGenre { get; set; }
        public GenreView SelectedDeleteGenre { get; set; }

        public ImageSource SelectedAddGenreImage { get; set; }

        public string AddGenreName { get; set; }
        public string UpdateGenreName { get; set; }

        private byte[] _addGenreImage;
        private GenreApiService _apiService;
        private List<Genre> apiGenres { get; set; }

        public ObservableCollection<GenreView> Genres { get; set; }
        public ManageGenresPageViewModel()
        {
            _apiService = new GenreApiService();
            ConfigureDataSource();
            AddGenresCommand = new Command(async () => await ExecuteAddGenresCommand());
            UpdateGenresCommand = new Command(async () => await ExecuteUpdateGenresCommand());
            DeleteGenresCommand = new Command(async () => await ExecuteDeleteGenresCommand());

            AddGenreImageCommand = new Command(async () => await ExecuteAddGenreImageCommand());
            UpdateGenreImageCommand = new Command(async () => await ExecuteUpdateGenreImageCommand());
        }

        public async Task ExecuteAddGenresCommand()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            var model = new Genre()
            {
                Name = AddGenreName,
                Image = _addGenreImage,
            };
            var result = await _apiService.CreateAsync(model);
            if (result != null)
            {
                GenreView newGenre = new GenreView()
                {
                    SysID = result.GenreSysID,
                    GenreName = result.Name,
                    Image = SelectedAddGenreImage
                };
                Genres.Add(newGenre);
                OnPropertyChanged(nameof(Genres));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        private bool CheckAddValuesEmpty()
        {
            if (string.IsNullOrEmpty(AddGenreName) || _addGenreImage == null)
            {
                return true;
            }
            return false;
        }

        private async void ConfigureDataSource()
        {
            apiGenres = await _apiService.GetAll();
            ObservableCollection<GenreView> convertGenres = new ObservableCollection<GenreView>();
            foreach(var genre in apiGenres.ToList())
            {
                var convertGenre = new GenreView()
                {
                    SysID = genre.GenreSysID,
                    GenreName = genre.Name,
                    Image = BitmapConverter.ByteToImageSource(genre.Image)
                };
                convertGenres.Add(convertGenre);
            }
            Genres = convertGenres;
            OnPropertyChanged(nameof(Genres));
        }
        public async Task ExecuteUpdateGenresCommand()
        {

        }
        public async Task ExecuteDeleteGenresCommand()
        {

        }    
        
        public async Task ExecuteAddGenreImageCommand()
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
            if (selectedFileImage == null)
            {
                return;
            }
            var stream = selectedFileImage.GetStream();
            SelectedAddGenreImage = ImageSource.FromStream(() => stream);
            OnPropertyChanged(nameof(SelectedAddGenreImage));
            _addGenreImage = BitmapConverter.StreamToByte(stream);
        }
        public async Task ExecuteUpdateGenreImageCommand()
        {

        }
    }
}
