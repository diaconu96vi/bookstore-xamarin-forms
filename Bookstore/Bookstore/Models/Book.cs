using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class Book
    {
        public int BookSysID { get; set; }

        public string ISBN { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte[] Image { get; set; }

        public int Price { get; set; }

        public int AuthorFK_SysID { get; set; }

        public int PublisherFK_SysID { get; set; }


        //Foreign Keys
        public Author Author { get; set; }

        public Publisher Publisher { get; set; }
    }
}
