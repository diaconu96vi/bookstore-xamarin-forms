using Bookstore.ApplicationUtils;
using Bookstore.CustomControls;
using Bookstore.Models;
using Bookstore.Models.ModelViews;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.DetailPages
{
    public class BookDetailPageViewModel : BaseViewModel
    {
        public BookView ActiveBook { get; set; }
        public Favorite UserFavorite { get; set; }
        public Rating UserRating { get; set; }

        public Command AddBasketCommand { get; set; }
        public Command AddNewComment { get; set; }
        public string TopLabel { get; set; }
        public string BookTitleTop { get; set; }
        public string AuthorTop { get; set; }
        public string PriceTop { get; set; }        
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Price { get; set; }
        public string Publisher { get; set; }
        public string Date { get; set; }
        public string AddCommentText { get; set; }

        public ImageSource BookImage { get; set; }
        public ImageSource FavoriteIcon { get; set; }
        public List<int> QuantityList { get; set; }
        public int SelectedQuantity { get; set; }

        public ObservableCollection<StarView> UserRatingStars { get; set; }
        public ObservableCollection<StarView> GeneralRatingStars { get; set; }
        public ObservableCollection<CommentView> CommentsList { get; set; }

        public string UserRatingText { get; set; }
        public string GeneralRatingText { get; set; }

        private bool _isFavorite { get; set; }

        private FavoriteApiService _favoriteApiService { get; set; }
        private RatingApiService _ratingApiService { get; set; }
        private CommentApiService _commentApiService { get; set; }

        public BookDetailPageViewModel(BookView bookView)
        {
            _favoriteApiService = new FavoriteApiService();
            _ratingApiService = new RatingApiService();
            _commentApiService = new CommentApiService();
            QuantityList = Enumerable.Range(1, 10).ToList();
            ActiveBook = bookView;
            AddBasketCommand = new Command(async () => await ExecuteAddBasketCommand());
            AddNewComment = new Command(async () => await ExecuteAddNewCommentCommand());
            ConfigureFavoriteStartup();
            ConfigureRatingList();
            ConfigureBookBinding();
            ConfigureCommentsList();
            OnPropertyChanged(nameof(QuantityList));
        }
        #region Configures
        public async void ConfigureFavoriteStartup()
        {
            var favoriteModel = new Favorite()
            {
                BookFK_SysID = ActiveBook.SysID,
                AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id
            };
            UserFavorite = await _favoriteApiService.GetFavorite(favoriteModel);
            if(UserFavorite == null)
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
        public async void ConfigureCommentsList()
        {
            var bookComments = await _commentApiService.GetBookComments(ActiveBook.SysID);
            List<CommentView> commentViews = new List<CommentView>();
            if(bookComments.Any())
            {
                foreach(var comment in bookComments)
                {
                    var newCommentView = new CommentView()
                    {
                        CommentSysID = comment.CommentSysID,
                        CommentText = comment.CommentText,
                        Date = comment.Date.ToString(),
                        UserName = comment.UserName
                    };
                    commentViews.Add(newCommentView);
                }               
            }
            CommentsList = new ObservableCollection<CommentView>(commentViews);
            OnPropertyChanged(nameof(CommentsList));
        }
        public async void ConfigureRatingList()
        {
            var ratingModel = new Rating()
            {
                BookFK_SysID = ActiveBook.SysID,
                AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id
            };
            var userRating = await _ratingApiService.GetUserBookRating(ratingModel);
            if(userRating == null)
            {
                ConfigureUserRatingList(null);
            }    
            else
            {
                ConfigureUserRatingList(userRating);
                UserRating = userRating;
            }

            var bookRatings = await _ratingApiService.GetBookRatings(ActiveBook.SysID);
            if (bookRatings == null || !bookRatings.Any())
            {
                ConfigureBookRatingList(0, 0);
            }
            else
            {
                int total = 0;
                foreach(var generalRating in bookRatings.ToList())
                {
                    total += generalRating.RatingGrade;
                }
                if (total > 0 && bookRatings.Count > 0)
                {
                    double averageRating = total / bookRatings.Count;
                    var finalRating = (int)(Math.Round(averageRating));
                    ConfigureBookRatingList(finalRating, bookRatings.Count);
                }
            }
        }
        private void ConfigureUserRatingList(Rating userRating)
        {
            if (userRating == null)
            {
                List<StarView> userRatings = new List<StarView>();
                userRatings.Add(new StarView() { StarID = 1, StarClassID = "1", StarImageString = "fillstar.png" });
                userRatings.Add(new StarView() { StarID = 2, StarClassID = "2", StarImageString = "fillstar.png" });
                userRatings.Add(new StarView() { StarID = 3, StarClassID = "3", StarImageString = "fillstar.png" });
                userRatings.Add(new StarView() { StarID = 4, StarClassID = "4", StarImageString = "fillstar.png" });
                userRatings.Add(new StarView() { StarID = 5, StarClassID = "5", StarImageString = "fillstar.png" });
                UserRatingStars = new ObservableCollection<StarView>(userRatings);
                UserRatingText = "Please rate";
            }
            else
            {
                List<StarView> userRatings = new List<StarView>();
                for (var i = 1; i <= 5; i++)
                {
                    var newStarView = new StarView() { StarID = i, StarClassID = i.ToString(), StarImageString = "fillstar.png" };
                    if (i > userRating.RatingGrade)
                    {
                        newStarView.StarImageString = "emptystar.png";
                    }
                    userRatings.Add(newStarView);
                }
                UserRatingStars = new ObservableCollection<StarView>(userRatings);
                UserRatingText = userRating.RatingGrade.ToString();
            }
            OnPropertyChanged(nameof(UserRatingStars));
            OnPropertyChanged(nameof(UserRatingText));
        }

        private void ConfigureBookRatingList(int averageRating, int numberOfRatings)
        {
            if(numberOfRatings == 0)
            {
                List<StarView> generalRatings = new List<StarView>();
                generalRatings.Add(new StarView() { StarID = 1, StarClassID = "1", StarImageString = "emptystar.png" });
                generalRatings.Add(new StarView() { StarID = 2, StarClassID = "2", StarImageString = "emptystar.png" });
                generalRatings.Add(new StarView() { StarID = 3, StarClassID = "3", StarImageString = "emptystar.png" });
                generalRatings.Add(new StarView() { StarID = 4, StarClassID = "4", StarImageString = "emptystar.png" });
                generalRatings.Add(new StarView() { StarID = 5, StarClassID = "5", StarImageString = "emptystar.png" });
                GeneralRatingStars = new ObservableCollection<StarView>(generalRatings);
                GeneralRatingText = "No ratings";
            }
            else
            {
                List<StarView> generalRatings = new List<StarView>();
                for (var i = 1; i <= 5; i++)
                {
                    var newStarView = new StarView() { StarID = i, StarClassID = i.ToString(), StarImageString = "fillstar.png" };
                    if (i > averageRating)
                    {
                        newStarView.StarImageString = "emptystar.png";
                    }
                    generalRatings.Add(newStarView);
                }
                GeneralRatingStars = new ObservableCollection<StarView>(generalRatings);
                GeneralRatingText = string.Format(string.Format("{0} by {1} users", averageRating.ToString(), numberOfRatings.ToString()));
            }
            OnPropertyChanged(nameof(GeneralRatingStars));
            OnPropertyChanged(nameof(GeneralRatingText));
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
        #endregion
        #region Commands
        public async Task ExecuteAddBasketCommand()
        {
            if(SelectedQuantity == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Please select quantity", "Cancel");
                return;
            }
            ActiveBook.Quantity = SelectedQuantity.ToString();
            ShoppingBasket.Instance.AddOrderItem(ActiveBook);
            MessagingCenter.Send<BookView>(ActiveBook, "AddBasketRefresh");
        } 
        
        public async Task ExecuteAddNewCommentCommand()
        {
            if(string.IsNullOrEmpty(AddCommentText))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Comment is empty", "Cancel");
                return;
            }
            var newComment = new Comment()
            {
                AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id,
                BookFK_SysID = ActiveBook.SysID,
                CommentText = AddCommentText,
                UserName = ApplicationGeneralSettings.CurrentUser.UserName,
                Date = DateTime.Today.Date
            };

            var result = await _commentApiService.CreateAsync(newComment);
            if(result != null)
            {
                var commentView = new CommentView()
                {
                    CommentSysID = result.CommentSysID,
                    UserName = result.UserName,
                    AppUserFK_SysID = result.AppUserFK_SysID,
                    BookFK_SysID = result.BookFK_SysID,
                    Date = result.Date.ToString(),
                    CommentText = result.CommentText
                };
                CommentsList.Add(commentView);
                CommentsList = new ObservableCollection<CommentView>(CommentsList);
                AddCommentText = string.Empty;
                OnPropertyChanged(nameof(CommentsList));
                OnPropertyChanged(nameof(AddCommentText));
            }
        }
        #endregion
        public async void ConfigureFavoriteIcon()
        {
            if(_isFavorite)
            {
                _isFavorite = false;
                FavoriteIcon = "faxList.png";
                await _favoriteApiService.DeleteAsync(UserFavorite.FavoriteSysID);
                UserFavorite = null;
            }
            else
            {
                var favoriteModel = new Favorite()
                {
                    BookFK_SysID = ActiveBook.SysID,
                    AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id
                };
                var result = await _favoriteApiService.CreateAsync(favoriteModel);
                if(result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                }
                else
                {
                    UserFavorite = result;
                    _isFavorite = true;
                    FavoriteIcon = "favFill.png";
                }
            }
            OnPropertyChanged(nameof(FavoriteIcon));
        }        
        public async void UpdateUserRating(string starViewClassID)
        {
            if (UserRating != null)
            {
                UserRating.RatingGrade = int.Parse(starViewClassID);
                var newRating = await _ratingApiService.UpdateAsync(UserRating);
                if (newRating == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                    return;
                }
            }
            else
            {
                var newRating = new Rating()
                {
                    BookFK_SysID = ActiveBook.SysID,
                    AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id,
                    RatingGrade = int.Parse(starViewClassID)
                };
                var result = await _ratingApiService.CreateAsync(newRating);
                if(result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                    return;
                }
                else
                {
                    UserRating = result;
                }
            }
            ConfigureRatingList();
        }
    }
}
