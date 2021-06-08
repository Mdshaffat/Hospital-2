using HospitalAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
