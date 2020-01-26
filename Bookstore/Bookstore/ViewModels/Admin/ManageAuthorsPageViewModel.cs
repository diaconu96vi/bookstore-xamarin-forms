using Bookstore.Models;
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
        public ManageAuthorsPageViewModel()
        {
            AddAuthorCommand = new Command(async () => await ExecuteAddAuthorCommand());
            UpdateAuthorCommand = new Command(async () => await ExecuteUpdateAuthorCommand());
            DeleteAuthorCommand = new Command(async () => await ExecuteDeleteAuthorCommand());
        }

        public async Task ExecuteAddAuthorCommand()
        {

        }        
        public async Task ExecuteUpdateAuthorCommand()
        {

        }        
        public async Task ExecuteDeleteAuthorCommand()
        {

        }
    }
}
