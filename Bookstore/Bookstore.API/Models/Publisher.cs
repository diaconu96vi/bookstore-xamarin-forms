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
        public int SysID { get; set; }

        public string Name { get; set; }

        //Reverse navigation

        public List<Book> Books { get; set; }

        public Publisher()
        {
            Books = new List<Book>();
        }
    }
}
