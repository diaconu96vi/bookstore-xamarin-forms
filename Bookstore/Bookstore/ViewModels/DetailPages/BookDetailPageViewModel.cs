using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bookstore.ViewModels.DetailPages
{
    public class BookDetailPageViewModel : BaseViewModel
    {
        public BookView ActiveBook { get; set; }

        public string TopLabel { get; set; }

        public ImageSource BookImage { get; set; }

        public BookDetailPageViewModel(BookView bookView)
        {
            ActiveBook = bookView;
            TopLabel = string.Format("{0} - {1}", ActiveBook.AuthorName, ActiveBook.Title);
            BookImage = ActiveBook.Image;
            OnPropertyChanged(nameof(TopLabel));
            OnPropertyChanged(nameof(BookImage));
        }
    }
}
