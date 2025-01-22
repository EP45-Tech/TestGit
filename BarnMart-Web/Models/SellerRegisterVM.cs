﻿using System.ComponentModel.DataAnnotations;

namespace BarnMart_Web.Models
{
    //Available only for ADMIN
    public class SellerRegisterVM
    {
        public SellerRegisterVM()
        {
            Lastname = "";
            Firstname = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
        }
        [Required]
        [MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
