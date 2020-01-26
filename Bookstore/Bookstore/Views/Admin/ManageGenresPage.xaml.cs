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
    public partial class ManageGenresPage : ContentPage
    {
        public ManageGenresPage()
        {
            InitializeComponent();
            this.BindingContext = new ManageGenresPageViewModel();
        }
    }
}