using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace exam1.Models
{
    public class ActivityViewModel
    {
    //    [Required]
    //     [Display(Name = "Title")]
    //     public string title { get; set; }

    //     [Required]
    //     [Display(Name = "Date")]
    //     [DataType(DataType.Date)]
    //     [DateValidation(ErrorMessage = "Date has to be a future date!")]
    //     public DateTime date { get; set; }

    //     [Required]
    //     [Display(Name = "Time")]
    //     [DataType(DataType.Date)]
    //     [DateValidation(ErrorMessage = "Time has to be a future time!")]
    //     public DateTime time { get; set; }

    //     [Required]
    //     [Display(Name = "Duration")]
    //     public int duration { get; set; }

    //     [Required]
    //     [Display(Name = "Description")]
    //     public string description { get; set; }


        [Required]
        [MinLength(3)]
        [Display(Name = "ProductName")]
        public string productname { get; set; }
        public string createdby { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "ProductName")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Time has to be a future time!")]
        public DateTime enddate { get; set; }
        public double startingbid { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        public double topbid { get; set; }
       

         



         
    }
}

