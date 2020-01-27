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
    public class ManageAuthorsPageViewModel : BaseViewModel
    {
        public Command AddAuthorCommand { get; set; }
        public Command UpdateAuthorCommand { get; set; }
        public Command DeleteAuthorCommand { get; set; }

        public string AddAuthorName { get; set; }
        public string UpdateAuthorName { get; set; }

        public Author SelectedUpdateAuthor { get; set; }
        public Author SelectedDeleteAuthor { get; set; }

        public ObservableCollection<Author> Authors { get; set; }

        private AuthorApiService _apiService;

        public ManageAuthorsPageViewModel()
        {
            ConfigureAuthorsDataSource();
            AddAuthorCommand = new Command(async () => await ExecuteAddAuthorCommand());
            UpdateAuthorCommand = new Command(async () => await ExecuteUpdateAuthorCommand());
            DeleteAuthorCommand = new Command(async () => await ExecuteDeleteAuthorCommand());
        }

        public async Task ExecuteAddAuthorCommand()
        {
            if (CheckAddValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            var model = new Author()
            {
                Name = AddAuthorName
            };
            var result = await _apiService.CreateAsync(model);
            if (result != null)
            {
                Authors.Add(result);
                OnPropertyChanged(nameof(Authors));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }      
        
        private bool CheckAddValuesEmpty()
        {
            if(!string.IsNullOrEmpty(AddAuthorName))
            {
                return true;
            }
            return false;
        }

        private bool CheckUpdateValuesEmpty()
        {
            if(!string.IsNullOrEmpty(UpdateAuthorName))
            {
                return true;
            }
            return false;
        }

        private async void ConfigureAuthorsDataSource()
        {
            var authorsList = await _apiService.GetAll();
            foreach(var author in authorsList)
            {
                Authors.Add(author);
            }
            OnPropertyChanged(nameof(Authors));
        }
        public async Task ExecuteUpdateAuthorCommand()
        {
            if(CheckUpdateValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var model = new Author()
                {
                    SysID = SelectedUpdateAuthor.SysID,
                    Name = UpdateAuthorName,
                };
                var result = await _apiService.UpdateAsync(model);
                if(result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                }
                else
                {
                    Authors.Remove(SelectedUpdateAuthor);
                    SelectedUpdateAuthor = null;
                    Authors.Add(result);
                    OnPropertyChanged(nameof(Authors));
                    OnPropertyChanged(nameof(SelectedUpdateAuthor));
                }
            }
        }        
        public async Task ExecuteDeleteAuthorCommand()
        {
            if(CheckDeleteValuesAreEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
            }
            else
            {
                var result = await _apiService.DeleteAsync(SelectedDeleteAuthor.SysID);
                if(result)
                {
                    Authors.Remove(SelectedDeleteAuthor);
                    SelectedDeleteAuthor = null;
                    OnPropertyChanged(nameof(Authors));
                    OnPropertyChanged(nameof(SelectedDeleteAuthor));
                }
            }
        }

        private bool CheckDeleteValuesAreEmpty()
        {
            if(SelectedDeleteAuthor == null)
            {
                return true;
            }
            return false;
        }
    }
}
