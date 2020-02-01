using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Services
{
    public class Card
    {
        public int SysID { get; set; }
        public string AppUserFK_SysID { get; set; }
        public string OwnerName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }
    }
}
