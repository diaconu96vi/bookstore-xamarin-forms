using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bookstore.Models.ModelViews
{
    public class OrderView
    {
        [Required]
        [Key]
        public int OrderSysID { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserFK_SysID { get; set; }
        [ForeignKey("Address")]
        public int AddressFK_SysID { get; set; }
        [ForeignKey("Card")]
        public int CardFK_Sys { get; set; }

        public string Date { get; set; }

        public string State { get; set; }

        public string TotalPrice { get; set; }

        public string UserName { get; set; }

        public string DisplayText { get; set; }

        //Foreign Keys
        public AppUser AppUser { get; set; }
        public Card Card { get; set; }
        public Address Address { get; set; }
    }
}
