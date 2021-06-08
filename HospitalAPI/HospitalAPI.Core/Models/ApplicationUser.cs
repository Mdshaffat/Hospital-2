using Microsoft.AspNetCore.Identity;
using System;

namespace HospitalAPI.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsAdctive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }


    }
}
