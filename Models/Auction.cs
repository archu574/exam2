using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam2.Models
{
    public class Auction
    {
        [Key]
        public int id {get;set;}
        public string productname { get; set; }
        public string createdby { get; set; }
        public string description { get; set; }
        public DateTime enddate { get; set; }
        public double startingbid { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        public double topbid { get; set; }
       

         public List <UserhasAuction> userslist {get; set;}
      
        public Auction ()
        {
            userslist = new List<UserhasAuction>();
        }
    }
}
