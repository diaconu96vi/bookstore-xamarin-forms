using Bookstore.ApplicationUtils;
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

        public CardView SelectedCardView { get; set; }
        private CardApiService _cardApiService { get; set; }
        private OrderApiService _orderApiService { get; set; }
        private OrderDetailApiService _orderDetailApiService { get; set; }

        public string CompleteButtonText { get; set; }
        public CreditCardPageViewModel()
        {
            _cardApiService = new CardApiService();
            _orderApiService = new OrderApiService();
            _orderDetailApiService = new OrderDetailApiService();
            ConfirmOrderCommand = new Command(async () => await ExecuteConfirmOrderCommand());
            CompleteButtonText = string.Format("{0} Lei", ShoppingBasket.Instance.TotalPrice + ShoppingBasket.Instance.ShippingPrice);
            OnPropertyChanged(nameof(CompleteButtonText));
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

        public void SelectCardCheck(string cardID)
        {
            if (string.IsNullOrEmpty(cardID))
            {
                return;
            }
            if(SelectedCardView != null)
            {
                SelectedCardView.CheckedImage = "unchecked.png";
            }
            var cardSysID = int.Parse(cardID);
            SelectedCardView = CardsList.FirstOrDefault(x => x.SysID == cardSysID);
            SelectedCardView.CheckedImage = "checked.png";

            var newCardList = CardsList;
            CardsList = new ObservableCollection<CardView>(newCardList);
            OnPropertyChanged(nameof(CardsList));
        }
        public async Task ExecuteConfirmOrderCommand()
        {
            if(SelectedCardView == null)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected card", "Cancel");
                return;
            }
            ShoppingBasket.Instance.ActiveCard = SelectedCardView;
            var neworder = new Models.Order()
            {
                AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id,
                CardFK_Sys = ShoppingBasket.Instance.ActiveCard.SysID,
                AddressFK_SysID = ShoppingBasket.Instance.ActiveAddress.AddressSysID,
                TotalPrice = ShoppingBasket.Instance.TotalPrice,
                Date = DateTime.Now,
                State = "In progress",
                UserName = ApplicationGeneralSettings.CurrentUser.UserName
            };            
            if(ApplicationGeneralSettings.FacebookUser != null)
            {
                neworder.UserName = ApplicationGeneralSettings.FacebookUser.Name;
            }
            var result = await _orderApiService.CreateAsync(neworder);
            if (result != null)
            {
                var orderDetails = new List<OrderDetail>();
                foreach(var book in ShoppingBasket.Instance.AddedOrderItems.ToList())
                {
                    var newOrderDetail = new OrderDetail()
                    {
                        OrderFK_SysID = result.OrderSysID,
                        BookFK_SysID = book.SysID,
                        Quantity = int.Parse(book.Quantity)
                    };
                    orderDetails.Add(newOrderDetail);
                }
                var resultOrderDetails = await _orderDetailApiService.CreateAsync(orderDetails);
                if(resultOrderDetails == null || !resultOrderDetails.Any())
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "OrderDetails could not be created", "Cancel");
                    return;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new SuccessOrderPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Order could not be created", "Cancel");
                return;
            }
        }

        public async void DeleteSelectedCard(string cardID)
        {
            if(string.IsNullOrEmpty(cardID))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected card", "Cancel");
                return;
            }
            var cardSysID = int.Parse(cardID);
            var result = await _cardApiService.DeleteAsync(cardSysID);
            if(result)
            {
                var deletedCard = CardsList.FirstOrDefault(x => x.SysID == cardSysID);
                var newCardList = CardsList;
                newCardList.Remove(deletedCard);
                CardsList = new ObservableCollection<CardView>(newCardList);
                OnPropertyChanged(nameof(CardsList));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
            }
            
        }

        public async void ConfigureCardList()
        {
            var cards = await _cardApiService.GetAll();
            var userCards = cards.Where(x => x.AppUserFK_SysID.Equals(ApplicationGeneralSettings.CurrentUser.Id)).ToList();
            if(!userCards.Any())
            {
                return;
            }
            List<CardView> allCardViews = new List<CardView>();
            foreach(var card in userCards.ToList())
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
