using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models.ServiceModel;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<ResponseObject> ForgotPassword(string email);
        Task<ResponseObject> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
