using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Admin
{
    public class AddBooksPageViewModel
    {
        public Command SaveChangesCommand { get; set; }

        public string ISBN { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }

        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Publication> Publications { get; set; }

        public Author SelectedAuthor { get; set; }
        public Publication SelectedPublication { get; set; }

        public AddBooksPageViewModel()
        {
            Authors = GetAuthors();
            Publications = GetPublications();
            SaveChangesCommand = new Command(async () => await ExecuteSaveChangesCommand());
        }

        public async Task ExecuteSaveChangesCommand()
        {
            var date = Date.Date;
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
