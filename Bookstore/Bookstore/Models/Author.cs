﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class Author
    {
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
