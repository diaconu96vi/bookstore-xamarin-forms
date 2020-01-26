using Bookstore.ViewModels.Admin;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class ManageAuthorsPage : ContentPage
    {
        public ManageAuthorsPage()
        {
            InitializeComponent();
            this.BindingContext = new ManageAuthorsPageViewModel();
        }
    }
}