using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class BookGenre
    {
        public int BookGenreSysID { get; set; }

        public int BookFK_SysID { get; set; }

        public int GenreFK_SysID { get; set; }

        public string Name { get; set; }

        //Foreign Keys
        public Book Book { get; set; }

        public Genre Genre { get; set; }
    }
}
