using Bookstore.Models;
using Bookstore.Models.ModelViews;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.ApplicationUtils
{
    public sealed class ShoppingBasket
    {
        private static ShoppingBasket _instance;
        private List<BookView> _orderItems;
        public Address ActiveAddress { get; set; }
        public IEnumerable<BookView> AddedOrderItems => _orderItems;
        public CardView ActiveCard { get; set; }
        public int TotalPrice { get; set; }

        public int ShippingPrice { get { return 10; } }


        private ShoppingBasket()
        {
            _orderItems = new List<BookView>();
        }

        public static ShoppingBasket Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ShoppingBasket();

                return _instance;
            }
        }

        public void AddOrderItem(BookView item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            BookView existing = _orderItems.Where(x => x.SysID == item.SysID).FirstOrDefault();

            if (existing != null)
            {
                existing.Quantity += item.Quantity;
                
            }
            else
            {
                _orderItems.Add(item);
            }
            TotalPrice += (int)Math.BigMul(int.Parse(item.Quantity), int.Parse(item.Price));

        }

        public void Delete(BookView item)
        {
            _orderItems.Remove(item);
            TotalPrice -= (int)Math.BigMul(int.Parse(item.Quantity), int.Parse(item.Price));
        }

        public void Clear()
        {
            _orderItems.Clear();
            TotalPrice = 0;
            ActiveCard = null;
            ActiveAddress = null;
        }
    }
}
