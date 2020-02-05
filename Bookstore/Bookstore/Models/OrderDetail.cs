using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class OrderDetail
    {
        [Required]
        [Key]
        public int OrderDetailSysID { get; set; }

        [ForeignKey("Book")]
        public int BookFK_SysID { get; set; }

        [ForeignKey("Order")]
        public int OrderFK_SysID { get; set; }

        public int Quantity { get; set; }

        //Reverse
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
