using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class BookGenre
    {
        [Required]
        [Key]
        public int BookGenreSysID { get; set; }

        [ForeignKey("Book")]
        public int BookFK_SysID { get; set; }  
        
        [ForeignKey("Genre")]
        public int GenreFK_SysID { get; set; }

        //Foreign Keys
        public Book Book { get; set; }

        public Genre Genre { get; set; }


    }
}
