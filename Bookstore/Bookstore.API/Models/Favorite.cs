using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Favorite
    {
        [Required]
        [Key]
        public int FavoriteSysID { get; set; }

        [ForeignKey("Book")]
        public int BookFK_SysID { get; set; }  
        
        [ForeignKey("AppUser")]
        public string AppUserFK_SysID { get; set; }

        //Foreign Keys
        public Book Book { get; set; }

        public AppUser AppUser { get; set; }
    }
}
