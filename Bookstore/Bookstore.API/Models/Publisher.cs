using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Publisher
    {
        [Required]
        [Key]
        public int PublisherSysID { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
