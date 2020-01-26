using Bookstore.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Bookstore.ViewModels.Admin
{
    public class AddBooksPageViewModel : BaseViewModel
    {
        public Command SaveChangesCommand { get; set; }
        public Command AddImageCommand { get; set; }

        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public DateTime Date { get; set; }
        public string Price { get; set; }
        public ImageSource SelectedImage { get; set; }

        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Publication> Publications { get; set; }

        public Author SelectedAuthor { get; set; }
        public Publication SelectedPublication { get; set; }

        public AddBooksPageViewModel()
        {
            Authors = GetAuthors();
            Publications = GetPublications();
            SaveChangesCommand = new Command(async () => await ExecuteSaveChangesCommand());
            AddImageCommand = new Command(async () => await ExecuteAddImageCommand());
        }

        public async Task ExecuteSaveChangesCommand()
        {
            var date = Date.Date;
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
            SelectedImage = ImageSource.FromStream(() => selectedFileImage.GetStream());
            OnPropertyChanged(nameof(SelectedImage));
        }

        private ObservableCollection<Author> GetAuthors()
        {
            ObservableCollection<Author> authors = new ObservableCollection<Author>()
            {
                new Author()
                {
                    SysID = 1,
                    Name = "eminescu"
                },
                new Author()
                {
                    SysID = 1,
                    Name = "blaga"
                }
            };
            return authors;
        }        

        private ObservableCollection<Publication> GetPublications()
        {
            ObservableCollection<Publication> publications = new ObservableCollection<Publication>()
            {
                new Publication()
                {
                    SysID = 1,
                    Name = "carturestis"
                },
                new Publication()
                {
                    SysID = 1,
                    Name = "diverta"
                }
            };
            return publications;
        }
    }
}
