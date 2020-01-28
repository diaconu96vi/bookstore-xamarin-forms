using Bookstore.CustomControls;
using Bookstore.Models;
using Bookstore.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.DetailPages
{
    public class BookDetailPageViewModel : BaseViewModel
    {
        public BookView ActiveBook { get; set; }

        public Command AddBasketCommand { get; set; }
        public Command BuyFastCommand { get; set; }
        public string TopLabel { get; set; }
        public string BookTitleTop { get; set; }
        public string AuthorTop { get; set; }
        public string PriceTop { get; set; }        
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Price { get; set; }
        public string Publisher { get; set; }
        public string Date { get; set; }

        public ImageSource BookImage { get; set; }
        public ImageSource FavoriteIcon { get; set; }

        public ObservableCollection<StarView> UserRatingStars { get; set; }
        public ObservableCollection<StarView> GeneralRatingStars { get; set; }

        public string UserRatingText { get; set; }
        public string GeneralRatingText { get; set; }

        private bool _isFavorite { get; set; }

        public BookDetailPageViewModel(BookView bookView)
        {
            ActiveBook = bookView;
            AddBasketCommand = new Command(async () => await ExecuteAddBasketCommand());
            BuyFastCommand = new Command(async () => await ExecuteBuyFastCommand());
            ConfigureRatingList();
            ConfigureBookBinding();

        }

        public async void ConfigureRatingList()
        {
            List<StarView> userRatings = new List<StarView>();
            userRatings.Add(new StarView() { StarID = 1, StarClassID = "1", StarImageString = "fillstar.png" } );
            userRatings.Add(new StarView() { StarID = 2, StarClassID = "2", StarImageString = "fillstar.png" } );
            userRatings.Add(new StarView() { StarID = 3, StarClassID = "3", StarImageString = "fillstar.png" } );
            userRatings.Add(new StarView() { StarID = 4, StarClassID = "4", StarImageString = "fillstar.png" } );
            userRatings.Add(new StarView() { StarID = 5, StarClassID = "5", StarImageString = "emptystar.png" } );
            UserRatingStars = new ObservableCollection<StarView>(userRatings);
            OnPropertyChanged(nameof(UserRatingStars));            
            List<StarView> generalRatings = new List<StarView>();
            generalRatings.Add(new StarView() { StarID = 1, StarClassID = "1", StarImageString = "fillstar.png" } );
            generalRatings.Add(new StarView() { StarID = 2, StarClassID = "2", StarImageString = "fillstar.png" } );
            generalRatings.Add(new StarView() { StarID = 3, StarClassID = "3", StarImageString = "fillstar.png" } );
            generalRatings.Add(new StarView() { StarID = 4, StarClassID = "4", StarImageString = "emptystar.png" } );
            generalRatings.Add(new StarView() { StarID = 5, StarClassID = "5", StarImageString = "emptystar.png" } );
            GeneralRatingStars = new ObservableCollection<StarView>(generalRatings);
            OnPropertyChanged(nameof(GeneralRatingStars));

            //to remove
            UserRatingText = "4";
            GeneralRatingText = "2.5";
            FavoriteIcon = "faxList.png";

            OnPropertyChanged(nameof(UserRatingText));
            OnPropertyChanged(nameof(GeneralRatingText));
            OnPropertyChanged(nameof(FavoriteIcon));
        }

        private void ConfigureBookBinding()
        {
            BookImage = ActiveBook.Image;
            TopLabel = ActiveBook.Title;

            BookTitleTop = ActiveBook.Title;
            AuthorTop = ActiveBook.AuthorName;
            PriceTop = string.Format("{0} Lei", ActiveBook.Price);

            BookTitle = string.Format("Title : {0}", ActiveBook.Title);
            Author = string.Format("Author : {0}", ActiveBook.AuthorName);
            Publisher = string.Format("Publisher : {0}", ActiveBook.PublicationName);
            Price = string.Format("{0} Lei", ActiveBook.Price); ;
            Date = string.Format("Published in : {0}", ActiveBook.PublicationDate.Year.ToString());

            OnPropertyChanged(nameof(BookImage));
            OnPropertyChanged(nameof(TopLabel));

            OnPropertyChanged(nameof(BookTitleTop));
            OnPropertyChanged(nameof(AuthorTop));
            OnPropertyChanged(nameof(PriceTop));

            OnPropertyChanged(nameof(BookTitle));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(Publisher));
            OnPropertyChanged(nameof(Date));
        }
        public async Task ExecuteAddBasketCommand()
        {

        }

        public async Task ExecuteBuyFastCommand()
        {

        }
        public async void ConfigureFavoriteIcon()
        {
            if(_isFavorite)
            {
                _isFavorite = false;
                FavoriteIcon = "faxList.png";
            }
            else
            {
                _isFavorite = true;
                FavoriteIcon = "favFill.png";
            }
            OnPropertyChanged(nameof(FavoriteIcon));
        }        
        public async void UpdateUserRating(string starViewClassID)
        {
            List<StarView> userRatings = new List<StarView>();
            for (var i=1; i <= 5; i++)
            {              
                var newStarView = new StarView() { StarID = i, StarClassID = i.ToString(), StarImageString = "fillstar.png" };
                if(i > int.Parse(starViewClassID))
                {
                    newStarView.StarImageString = "emptystar.png";
                }
                userRatings.Add(newStarView);
            }
            UserRatingStars = new ObservableCollection<StarView>(userRatings);
            UserRatingText = starViewClassID;
            OnPropertyChanged(nameof(UserRatingStars));
            OnPropertyChanged(nameof(UserRatingText));
        }
    }
}
