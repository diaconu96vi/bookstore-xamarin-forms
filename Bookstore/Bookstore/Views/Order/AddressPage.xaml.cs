using Bookstore.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressPage : ContentPage
    {
        public AddressPage()
        {
            InitializeComponent();
            this.BindingContext = new AddressPageViewModel();
        }
    }
}