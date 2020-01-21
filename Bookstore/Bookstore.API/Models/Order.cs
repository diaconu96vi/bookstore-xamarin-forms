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

        public DateTime Date { get; set; }

        public string State { get; set; }

        public int TotalPrice { get; set; }

        //Foreign Keys
        public AppUser AppUser { get; set; }
    }
}
