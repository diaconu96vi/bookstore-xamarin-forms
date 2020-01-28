using Bookstore.Models;
using Bookstore.ViewModels.DetailPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        public BookDetailPage(BookView item)
        {
            this.BindingContext = new BookDetailPageViewModel(item);
            InitializeComponent();
        }
        private double _yaxis = 0;
        //private readonly List<ProductList> _pList = new List<ProductList>();
        //private readonly List<StartList> _startList = new List<StartList>();
        //public ProductDetail()
        //{
        //    _pList.Add(new ProductList
        //    {
        //        ProductImg = "machine.jpg"
        //    });
        //    _pList.Add(new ProductList
        //    {
        //        ProductImg = "m2.jpg"
        //    });
        //    _startList.Add(new StartList
        //    {
        //        StarImg = "fillstar.png"
        //    });
        //    _startList.Add(new StartList
        //    {
        //        StarImg = "fillstar.png"
        //    });
        //    _startList.Add(new StartList
        //    {
        //        StarImg = "fillstar.png"
        //    });
        //    _startList.Add(new StartList
        //    {
        //        StarImg = "fillstar.png"
        //    });
        //    _startList.Add(new StartList
        //    {
        //        StarImg = "emptystar.png"
        //    });

        //InitializeComponent();
        //starList.ItemsSource = _startList;
        //starListComment.ItemsSource = _startList;
        //starListComment2.ItemsSource = _startList;
        //CarouselView.ItemsSource = _pList;
        //MainScroll.Scrolled += MainScroll_Scrolled;

        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            var height = Math.Round(Application.Current.MainPage.Height);
            var ycordinate = Math.Round(e.ScrollY);
            if (ycordinate > (height / 3))
            {
                NavbarStack.IsVisible = true;
                return;
            }

            NavbarStack.IsVisible = false;
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void MoreCommentsClick(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new CommentsPage());
        }
    }
}