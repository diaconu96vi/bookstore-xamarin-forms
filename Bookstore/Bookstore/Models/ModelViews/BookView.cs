using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bookstore.Models
{
    public class BookView
    {
        public int SysID { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string AuthorName { get; set; }
        public string PublicationName { get; set; }

        public string Quantity { get; set; }

        public ImageSource  Image { get; set; }
    }
}
