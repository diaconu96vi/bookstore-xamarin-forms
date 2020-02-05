using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models.ModelViews
{
    public class CommentView
    {
        public int CommentSysID { get; set; }
        public int BookFK_SysID { get; set; }
        public string AppUserFK_SysID { get; set; }

        public string CommentText { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
    }
}
