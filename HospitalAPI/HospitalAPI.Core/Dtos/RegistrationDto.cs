using HospitalAPI.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Core.Dtos
{
    public class RegistrationDto
    {
        public int HospitalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Designation { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsAdctive { get; set; }
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }
}
