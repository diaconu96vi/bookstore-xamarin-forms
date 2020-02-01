using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models.ModelViews
{
    public class CardView
    {
        public int SysID { get; set; }
        public string AppUserFK_SysID { get; set; }
        public string OwnerName { get; set; }
        public string CheckedImage { get; set; }
        public string CardImage { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }
    }
}
