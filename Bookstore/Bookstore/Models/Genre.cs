using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class Genre
    {
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
