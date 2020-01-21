using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Book
    {
        [Required]
        [Key]
        public int BookSysID { get; set; }

        public string ISBN { get; set; } 
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Edition { get; set; }

        public int Price { get; set; }

        [ForeignKey("Author")]
        public int AuthorFK_SysID { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherFK_SysID { get; set; }


        //Foreign Keys
        public Author Author { get; set; }

        public Publisher Publisher { get; set; }
    }
}
