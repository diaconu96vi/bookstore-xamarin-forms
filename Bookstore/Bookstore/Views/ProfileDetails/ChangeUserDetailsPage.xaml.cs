using Bookstore.ViewModels.ProfileDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.ProfileDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeUserDetailsPage : ContentPage
    {
        public ChangeUserDetailsPage()
        {
            this.BindingContext = new ChangeUserDetailsPageViewModel();
            InitializeComponent();
        }
    }
}