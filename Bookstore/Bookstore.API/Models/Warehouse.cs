using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Warehouse
    {
        [Required]
        [Key]
        public int WarehouseSysID { get; set; }

        [ForeignKey("Book")]
        public int BookFK_SysID { get; set; }

        public int AvailableQuantity { get; set; }

        //Foreign Keys
        public Book Book { get; set; }
    }
}
