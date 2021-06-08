using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HospitalAPI.DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseObject> ForgotPassword(string email)
        {
            ResponseObject responseObject = new();

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    responseObject.Message = "Failed";
                    responseObject.IsValid = false;
                    responseObject.Data = null;
                    return responseObject;
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                responseObject.Message = "Success";
                responseObject.IsValid = true;
                var dynamicProperties = new Dictionary<string, object> { ["Code"] = code, ["User"] = user };
                responseObject.Data = dynamicProperties;
                return responseObject;

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ForgotPassword {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                responseObject.Message = "Error";
                responseObject.IsValid = false;
                responseObject.Data = null;
                return responseObject;
            }
        }
        public async Task<ResponseObject> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            ResponseObject responseObject = new();
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
                if (user == null)
                {
                    responseObject.Message = "Failed";
                    responseObject.IsValid = false;
                    responseObject.Data = null;
                    return responseObject;
                }

                var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Code, resetPasswordDto.Password);
                if (result.Succeeded)
                {
                    responseObject.Message = "Success";
                    responseObject.IsValid = true;
                    responseObject.Data = null;
                    return responseObject;
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ForgotPassword {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            responseObject.Message = "Failed";
            responseObject.IsValid = false;
            responseObject.Data = null;
            return responseObject;
        }
    }
}
