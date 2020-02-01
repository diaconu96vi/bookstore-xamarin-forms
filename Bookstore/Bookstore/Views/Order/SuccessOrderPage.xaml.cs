using Bookstore.ApplicationUtils;
using Bookstore.Views.TabbedPages;
using Plugin.SharedTransitions;
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
    public partial class SuccessOrderPage : ContentPage
    {
        public SuccessOrderPage()
        {
            InitializeComponent();
        }
        private void ContinueClick(object sender, EventArgs e)
        {
            ShoppingBasket.Instance.Clear();
            Application.Current.MainPage = new SharedTransitionNavigationPage(new ParentTabbedPage());
        }
    }
}