using Bookstore.Converters;
using Bookstore.Models;
using Bookstore.Models.ModelViews;
using Bookstore.Services;
using Bookstore.Views.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Order
{
    public class CreditCardPageViewModel : BaseViewModel
    {
        public ObservableCollection<CardView> CardsList { get; set; }
        public Command ConfirmOrderCommand { get; set; }

        private CardApiService _cardApiService { get; set; }
        public CreditCardPageViewModel()
        {
            _cardApiService = new CardApiService();
            ConfirmOrderCommand = new Command(async () => await ExecuteConfirmOrderCommand());
            MessagingCenter.Subscribe<Card>(this, "AddCard", (card) =>
            {
                CardView cardView = ConvertCardToCardView(card);               
                var cardsListCopy = CardsList;
                cardsListCopy.Add(cardView);
                CardsList = new ObservableCollection<CardView>(cardsListCopy);
                OnPropertyChanged(nameof(CardsList));
            });
            ConfigureCardList();
        }

        public async Task ExecuteConfirmOrderCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SuccessOrderPage());
        }

        public async void ConfigureCardList()
        {
            var cards = await _cardApiService.GetAll();
            if(!cards.Any())
            {
                return;
            }
            List<CardView> allCardViews = new List<CardView>();
            foreach(var card in cards.ToList())
            {
                CardView cardView = ConvertCardToCardView(card);
                allCardViews.Add(cardView);
            }
            CardsList = new ObservableCollection<CardView>(allCardViews);
            OnPropertyChanged(nameof(CardsList));
        }

        private CardView ConvertCardToCardView(Card card)
        {
            CardView cardView = new CardView()
            {
                SysID = card.SysID,
                OwnerName = card.OwnerName,
                CardNumber = card.CardNumber,
                CardExpirationDate = card.CardExpirationDate,
                CardCvv = card.CardCvv,
                AppUserFK_SysID = card.AppUserFK_SysID
            };

            cardView.CheckedImage = "unchecked.png";
            var cardConverter = new CardNumberToImageSourceConverter();
            cardView.CardImage = cardConverter.ConvertToImageSource(cardView.CardNumber).ToString();
            cardView.CardType = cardConverter.ConvertToString(cardView.CardNumber).ToString();

            return cardView;
        }
    }
}
