using Bookstore.Models;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Admin
{
    public class ManagePublishersPageViewModel : BaseViewModel
    {
        public Command AddPublisherCommand { get; set; }
        public Command UpdatePublisherCommand { get; set; }
        public Command DeletePublisherCommand { get; set; }

        public string AddPublisherName { get; set; }
        public string UpdatePublisherName { get; set; }

        public Publisher SelectedUpdatePublisher { get; set; }
        public Publisher SelectedDeletePublisher { get; set; }

        public ObservableCollection<Publisher> Publishers { get; set; }

        private PublisherApiService _apiService { get; set; }
        public ManagePublishersPageViewModel()
        {
            _apiService = new PublisherApiService();
            ConfigureDataSource();
            AddPublisherCommand = new Command(async () => await ExecuteAddPublisherCommand());
            UpdatePublisherCommand = new Command(async () => await ExecuteUpdatePublisherCommand());
            DeletePublisherCommand = new Command(async () => await ExecuteDeletePublisherCommand());
        }

        public async Task ExecuteAddPublisherCommand()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            var model = new Publisher()
            {
                Name = AddPublisherName
            };
            var result = await _apiService.CreateAsync(model);
            if (result != null)
            {
                Publishers.Add(result);
                OnPropertyChanged(nameof(Publishers));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        private bool CheckAddValuesEmpty()
        {
            if (!string.IsNullOrEmpty(AddPublisherName))
            {
                return true;
            }
            return false;
        }

        private bool CheckUpdateValuesEmpty()
        {
            if (!string.IsNullOrEmpty(UpdatePublisherName))
            {
                return true;
            }
            return false;
        }

        private async void ConfigureDataSource()
        {
            var publishersList = await _apiService.GetAll();
            foreach (var publisher in publishersList)
            {
                Publishers.Add(publisher);
            }
            OnPropertyChanged(nameof(Publishers));
        }
        public async Task ExecuteUpdatePublisherCommand()
        {
            if (CheckUpdateValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var model = new Publisher()
                {
                    SysID = SelectedUpdatePublisher.SysID,
                    Name = UpdatePublisherName,
                };
                var result = await _apiService.UpdateAsync(model);
                if (result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                }
                else
                {
                    Publishers.Remove(SelectedUpdatePublisher);
                    SelectedUpdatePublisher = null;
                    Publishers.Add(result);
                    OnPropertyChanged(nameof(Publishers));
                    OnPropertyChanged(nameof(SelectedUpdatePublisher));
                }
            }
        }
        public async Task ExecuteDeletePublisherCommand()
        {
            if (CheckDeleteValuesAreEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var result = await _apiService.DeleteAsync(SelectedUpdatePublisher.SysID);
                if (result)
                {
                    Publishers.Remove(SelectedDeletePublisher);
                    SelectedDeletePublisher = null;
                    OnPropertyChanged(nameof(Publishers));
                    OnPropertyChanged(nameof(SelectedDeletePublisher));
                }
            }
        }

        private bool CheckDeleteValuesAreEmpty()
        {
            if (SelectedDeletePublisher == null)
            {
                return true;
            }
            return false;
        }
    }
}
