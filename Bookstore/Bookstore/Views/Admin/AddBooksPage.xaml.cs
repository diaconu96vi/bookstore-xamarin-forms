using Bookstore.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBooksPage : ContentPage
    {
        public AddBooksPage()
        {
            InitializeComponent();
            this.BindingContext = new AddBooksPageViewModel();
        }
    }
}