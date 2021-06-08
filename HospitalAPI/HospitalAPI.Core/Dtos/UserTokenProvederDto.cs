using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos
{
    public class UserTokenProvederDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
