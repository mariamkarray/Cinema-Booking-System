//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinema.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class booking
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Enter the movie name")]
        public string movie_name { get; set; }
        [Required(ErrorMessage = "Date is required to check if there're available seats.")]
        public Nullable<System.DateTime> data_movie { get; set; }
    }
}
