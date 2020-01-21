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
        public int AuthorSysID { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string CompanyName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
