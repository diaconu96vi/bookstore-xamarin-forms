using Bookstore.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bookstore.Services
{
    public class Card
    {
        [Required]
        [Key]
        public int SysID { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserFK_SysID { get; set; }
        public string OwnerName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }

        //Reverse navigation
        public AppUser AppUser { get; set; }
    }
}
