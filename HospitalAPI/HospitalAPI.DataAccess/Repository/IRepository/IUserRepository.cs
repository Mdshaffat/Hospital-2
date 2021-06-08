using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
   public interface IUserRepository
    {
        Task<IReadOnlyList<ApplicationUser>> UserListAsync();
        Task<ApplicationUser> GetUser(string id);

    }
}
