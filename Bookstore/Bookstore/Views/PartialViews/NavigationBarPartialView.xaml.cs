using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.PartialViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBarPartialView
    {
        public NavigationBarPartialView(string title, bool visible, bool isModalpage)
        {
            InitializeComponent();
            Title.Text = title;
            BackButton.IsVisible = visible;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                if (isModalpage)
                    Navigation.PopModalAsync();
                else
                    Navigation.PopAsync();
            };

            BackButton.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}