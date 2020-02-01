using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.ViewModels.Order
{
    public class AddNewCardPageViewModel : BaseViewModel
    {
        private string _cardNumber, _cardCvv, _cardExpirationDate;

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
        public AddNewCardPageViewModel()
        {

        }
    }
}
