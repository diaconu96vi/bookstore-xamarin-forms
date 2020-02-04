using Bookstore.ApplicationUtils;
using Bookstore.Models;
using Bookstore.Models.ModelViews;
using Bookstore.Services;
using Bookstore.Views.DetailPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.ProfileDetails
{
    public class CommentPageViewModel : BaseViewModel
    {
        public ObservableCollection<CommentView> CommentsList { get; set; }
        private CommentApiService _commentApiService { get; set; }
        private BookApiService _bookApiService { get; set; }

        private List<Comment> _userComments { get; set; }
        public CommentPageViewModel()
        {
            _bookApiService = new BookApiService();
            _commentApiService = new CommentApiService();
            ConfigureCommentsList();
        }

        public async void ConfigureCommentsList()
        {
            var allComments = await _commentApiService.GetAll();
            if(allComments == null)
            {
                _userComments = new List<Comment>();
            }
            else
            {
                _userComments = allComments.Where(x => x.AppUserFK_SysID.Equals(ApplicationGeneralSettings.CurrentUser.Id)).ToList();
                ObservableCollection<CommentView> commentsViews = new ObservableCollection<CommentView>();
                foreach (var comment in _userComments)
                {
                    var newCommentView = new CommentView()
                    {
                        CommentSysID = comment.CommentSysID,
                        AppUserFK_SysID = comment.AppUserFK_SysID,
                        BookFK_SysID = comment.BookFK_SysID,
                        CommentText = comment.CommentText,
                        Date = comment.Date.ToString(),
                        UserName = comment.UserName
                    };
                    commentsViews.Add(newCommentView);
                }
                CommentsList = new ObservableCollection<CommentView>(commentsViews);
                OnPropertyChanged(nameof(CommentsList));
            }         
        }

        public async void RemoveComment(string commentSysID)
        {
            if (string.IsNullOrEmpty(commentSysID))
            {
                return;
            }         
            var result = await _commentApiService.DeleteAsync(int.Parse(commentSysID));
            if (result)
            {
                var commentView = CommentsList.FirstOrDefault(x => x.CommentSysID == int.Parse(commentSysID));
                CommentsList.Remove(commentView);
                CommentsList = new ObservableCollection<CommentView>(CommentsList);
                OnPropertyChanged(nameof(CommentsList));
            }
        }

        public async Task ExecuteBookDetail(string bookSysID)
        {
            if(string.IsNullOrEmpty(bookSysID))
            {
                return;
            }
            var selectedBook = await _bookApiService.GetBook(int.Parse(bookSysID));
            if (selectedBook != null)
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new BookDetailPage(selectedBook));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected book", "Ok");
            }
        }
    }
}
