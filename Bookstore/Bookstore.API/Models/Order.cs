using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Order
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

        public DateTime Date { get; set; }

        public string State { get; set; }

        public int TotalPrice { get; set; }

        public string UserName { get; set; }

        //Foreign Keys
        public AppUser AppUser { get; set; }
        public Card Card { get; set; }
        public Address Address { get; set; }
    }
}
