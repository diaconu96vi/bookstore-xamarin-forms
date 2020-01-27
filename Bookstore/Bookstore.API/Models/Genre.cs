using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Genre
    {
        [Required]
        [Key]
        public int GenreSysID { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        //Reverse navigation
        public List<BookGenre> BookGenres { get; set; }

        public Genre()
        {
            BookGenres = new List<BookGenre>();
        }
    }
}
