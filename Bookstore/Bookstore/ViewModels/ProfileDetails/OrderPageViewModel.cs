using Bookstore.ApplicationUtils;
using Bookstore.Models.ModelViews;
using Bookstore.Services;
using Bookstore.Views.ProfileDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.ProfileDetails
{
    public class OrderPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrderView> OrdersList { get; set; }

        private OrderApiService _orderApiService { get; set; }
        public OrderPageViewModel()
        {
            _orderApiService = new OrderApiService();
            ConfigureOrdersListDataSource();
        }

        public async void ConfigureOrdersListDataSource()
        {
            var userOrders = await _orderApiService.GetUserOrders(new Models.Order() { AppUserFK_SysID = ApplicationGeneralSettings.CurrentUser.Id });
            if(userOrders.Any())
            {
                var userOrdersView = new List<OrderView>();
                foreach (var order in userOrders.ToList())
                {
                    var newOrderView = new OrderView()
                    {
                        OrderSysID = order.OrderSysID,
                        AppUserFK_SysID = order.AppUserFK_SysID,
                        AddressFK_SysID = order.AddressFK_SysID,
                        Address = order.Address,
                        State = order.State,
                        Date = order.Date.ToString(),
                        TotalPrice = order.TotalPrice.ToString(),
                        UserName = order.UserName
                    };
                    newOrderView.DisplayText = string.Format("FullName : {0}, City : {1} Address : {2}", newOrderView.Address.FullName, newOrderView.Address.City, newOrderView.Address.FullAddress);
                    userOrdersView.Add(newOrderView);
                }
                OrdersList = new ObservableCollection<OrderView>(userOrdersView);
                OnPropertyChanged(nameof(OrdersList));
            }
        }

        public async Task OpenOrderDetail(string SysID)
        {
            if(string.IsNullOrEmpty(SysID))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected Order", "Ok");
                return;
            }
            var selectedOrder = OrdersList.FirstOrDefault(x => x.OrderSysID == int.Parse(SysID));
            if (selectedOrder != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new OrderDetailPage(selectedOrder.OrderSysID));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No selected book", "Ok");
            }
        }
    }
}
