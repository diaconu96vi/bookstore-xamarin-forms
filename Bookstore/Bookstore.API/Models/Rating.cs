using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Rating
    {
        [Required]
        [Key]
        public int RatingSysID { get; set; }

        [ForeignKey("Book")]
        public int BookFK_SysID { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserFK_SysID { get; set; }

        public int RatingGrade { get; set; }

        //Foreign Keys
        public Book Book { get; set; }

        public AppUser AppUser { get; set; }
    }
}
