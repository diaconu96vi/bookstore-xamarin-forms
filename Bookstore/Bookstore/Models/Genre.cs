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
    }
}
