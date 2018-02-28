using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exam2.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name= "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage="Firstname can only contain letters!")]
        public string firstname { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name= "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage="Firstname can only contain letters!")]
        public string lastname { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name= "UserName")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage="UserName can only contain letters!")]
        public string username { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        // public int accountbal {get; set;}
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("password",ErrorMessage="Password and confirmation password must match!")]
        [Display(Name="Confirm Password")]
        public string passwordconfirmation { get; set; }

        public double wallet {get; set;}

        public List <UserhasAuction> auctionsbyusers {get; set;}
      
        public User ()
        {
            auctionsbyusers = new List<UserhasAuction>();
        }


    }
}
