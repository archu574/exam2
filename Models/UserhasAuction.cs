using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam2.Models
{
    public class UserhasAuction
    {
        public int id {get;set;}

        [ForeignKey("User")]
        public int UserId  {get;set;}

        [ForeignKey("Auction")]
        public int AuctionId  {get;set;}


        public User User {get;set;}


        public Auction Auction {get;set;}

        
    }
}