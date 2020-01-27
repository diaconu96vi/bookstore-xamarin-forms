using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Author
    {
        [Required]
        [Key]
        public int SysID { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }


        //Reverse navigation
        public List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
