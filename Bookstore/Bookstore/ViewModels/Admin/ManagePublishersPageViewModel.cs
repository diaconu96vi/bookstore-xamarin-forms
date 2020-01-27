using Bookstore.Models;
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
        public ManagePublishersPageViewModel()
        {
            AddPublisherCommand = new Command(async () => await ExecuteAddPublisherCommand());
            UpdatePublisherCommand = new Command(async () => await ExecuteUpdatePublisherCommand());
            DeletePublisherCommand = new Command(async () => await ExecuteDeletePublisherCommand());
        }

        public async Task ExecuteAddPublisherCommand()
        {

        }
        public async Task ExecuteUpdatePublisherCommand()
        {

        }
        public async Task ExecuteDeletePublisherCommand()
        {

        }
    }
}
