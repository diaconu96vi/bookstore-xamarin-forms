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

        public GenreView SelectedUpdateGenre { get; set; }
        public GenreView SelectedDeleteGenre { get; set; }

        public string AddGenreName { get; set; }
        public string UpdateGenreName { get; set; }

        private GenreApiService _apiService;
        private List<Genre> apiGenres { get; set; }

        public ObservableCollection<GenreView> Genres { get; set; }
        public ManageGenresPageViewModel()
        {
            Genres = new ObservableCollection<GenreView>();
            _apiService = new GenreApiService();
            ConfigureDataSource();
            AddGenresCommand = new Command(async () => await ExecuteAddGenresCommand());
            UpdateGenresCommand = new Command(async () => await ExecuteUpdateGenresCommand());
            DeleteGenresCommand = new Command(async () => await ExecuteDeleteGenresCommand());
        }

        public async Task ExecuteAddGenresCommand()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            var model = new Genre()
            {
                Name = AddGenreName
            };
            var result = await _apiService.CreateAsync(model);
            if (result != null)
            {
                GenreView newGenre = new GenreView()
                {
                    SysID = result.GenreSysID,
                    GenreName = result.Name
                };
                Genres.Add(newGenre);
                AddGenreName = string.Empty;
                OnPropertyChanged(nameof(Genres));
                OnPropertyChanged(nameof(AddGenreName));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        private bool CheckAddValuesEmpty()
        {
            if (string.IsNullOrEmpty(AddGenreName))
            {
                return true;
            }
            return false;
        }

        private async void ConfigureDataSource()
        {
            apiGenres = await _apiService.GetAll();
            if(apiGenres == null && apiGenres.Any())
            {
                return;
            }
            ObservableCollection<GenreView> convertGenres = new ObservableCollection<GenreView>();
            foreach(var genre in apiGenres.ToList())
            {
                var convertGenre = new GenreView()
                {
                    SysID = genre.GenreSysID,
                    GenreName = genre.Name
                };
                convertGenres.Add(convertGenre);
            }
            Genres = convertGenres;
            OnPropertyChanged(nameof(Genres));
        }
        public async Task ExecuteUpdateGenresCommand()
        {
            if (CheckUpdateValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var model = new Genre()
                {
                    GenreSysID = SelectedUpdateGenre.SysID,
                    Name = UpdateGenreName
                };
                var result = await _apiService.UpdateAsync(model);
                if (result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                }
                else
                {
                    Genres.Remove(SelectedUpdateGenre);
                    SelectedUpdateGenre = null;
                    UpdateGenreName = string.Empty;
                    var genreView = new GenreView()
                    {
                        SysID = result.GenreSysID,
                        GenreName = result.Name,
                        Image = BitmapConverter.ByteToImageSource(result.Image)
                    };
                    Genres.Add(genreView);
                    OnPropertyChanged(nameof(Genres));
                    OnPropertyChanged(nameof(SelectedUpdateGenre));
                    OnPropertyChanged(nameof(UpdateGenreName));
                }
            }
        }

        private bool CheckUpdateValuesEmpty()
        {
            if(string.IsNullOrEmpty(UpdateGenreName) || SelectedUpdateGenre == null)
            {
                return true;
            }
            return false;
        }
        public async Task ExecuteDeleteGenresCommand()
        {
            if (CheckDeleteValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var result = await _apiService.DeleteAsync(SelectedDeleteGenre.SysID);
                if (result)
                {
                    Genres.Remove(SelectedDeleteGenre);
                    SelectedDeleteGenre = null;
                    OnPropertyChanged(nameof(Genres));
                    OnPropertyChanged(nameof(SelectedDeleteGenre));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Couldn't delete", "Cancel");
                }
            }
        }    
        private bool CheckDeleteValuesEmpty()
        {
            if(SelectedDeleteGenre == null)
            {
                return true;
            }
            return false;
        }              
    }
}
