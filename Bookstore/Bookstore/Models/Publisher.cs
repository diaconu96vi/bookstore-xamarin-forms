using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class Publisher
    {
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
