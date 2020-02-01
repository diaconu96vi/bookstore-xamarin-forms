using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models.ModelViews
{
    public class OrderItemView
    {
        public int OrderItemId { get; set; }

        public BookView BookView { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }

        public string Price { get; set; }

        public string Total { get; set; }
    }
}
