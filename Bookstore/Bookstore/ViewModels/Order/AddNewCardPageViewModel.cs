using Bookstore.ApplicationUtils;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Order
{
    public class AddNewCardPageViewModel : BaseViewModel
    {
        private string _cardNumber, _cardCvv, _cardExpirationDate, _ownerName;  
        public string OwnerName
        {
            get => _ownerName;
            set
            {
                _ownerName = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }

        }

        public string CardCvv
        {
            get => _cardCvv;
            set
            {
                _cardCvv = value;
                OnPropertyChanged(nameof(CardCvv));
            }
        }
        public string CardExpirationDate
        {
            get => _cardExpirationDate;
            set
            {
                _cardExpirationDate = value;
                OnPropertyChanged(nameof(CardExpirationDate));
            }
        }

        public Command AddNewCardCommand { get; set; }
        private CardApiService _cardApiService { get; set; }
        public AddNewCardPageViewModel()
        {
            _cardApiService = new CardApiService();
            AddNewCardCommand = new Command(async () => await ExecuteAddNewCardCommand());
        }

        public async Task ExecuteAddNewCardCommand()
        {
            if (CheckValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are empty", "Cancel");
                return;
            }
            var model = new Card()
            {
                OwnerName = OwnerName,
                CardNumber = CardNumber,
                CardExpirationDate = CardExpirationDate,
                CardCvv = CardCvv,
                AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id
            };
            var result = await _cardApiService.CreateAsync(model);
            if (result != null)
            {
                MessagingCenter.Send<Card>(result, "AddCard");
                await Application.Current.MainPage.Navigation.PopModalAsync(true);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
        }

        private bool CheckValuesEmpty()
        {
            if (string.IsNullOrEmpty(OwnerName) || string.IsNullOrEmpty(CardNumber) || string.IsNullOrEmpty(CardCvv) || string.IsNullOrEmpty(CardExpirationDate))
            {
                return true;
            }
            return false;
        }
    }
}
