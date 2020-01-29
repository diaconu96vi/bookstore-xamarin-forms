using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Comment
    {
        public int CommentSysID { get; set; }
        public int BookFK_SysID { get; set; }
        public string AppUserFK_SysID { get; set; }

        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        //Foreign Keys
        public Book Book { get; set; }

        public AppUser AppUser { get; set; }
    }
}
