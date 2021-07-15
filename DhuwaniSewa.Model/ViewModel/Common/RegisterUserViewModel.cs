using AutoMapper;
using DhuwaniSewa.Model.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DhuwaniSewa.Model.ViewModel
{
    public class RegisterUserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string AppUserId { get; set; }
        public bool IsCompany { get; set; }
        public bool IsEmployee { get; set; }
        public string Email { get; set; }
        [Required]
        public bool IsServiceProvider { get; set; }
        public string MobileNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
