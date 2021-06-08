using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Errors;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountRepository _accountrepo;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITokenService tokenService,
                                 IEmailSender emailSender,
                                 IHttpContextAccessor httpContextAccessor,
                                 IAccountRepository accountrepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _accountrepo = accountrepo;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserTokenProvederDto>> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return new UserTokenProvederDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                FullName = user.FirstName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        [HttpPost("registration")]
        public async Task<ActionResult<UserTokenProvederDto>> Registration(RegistrationDto registrationDto)
        {
            var user = new ApplicationUser
            {
                HospitalId = registrationDto.HospitalId,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                UserName = registrationDto.Email,
                Designation = registrationDto.Designation,
                PhoneNumber = registrationDto.PhoneNumber,
                JoiningDate = registrationDto.JoiningDate,
                IsAdctive = registrationDto.IsAdctive,
                CreatedBy = registrationDto.CreatedBy,
                CreatedOn = DateTime.Now,
                UpdatedBy = registrationDto.CreatedBy,
                UpdatedOn = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded) return BadRequest();



            return new UserTokenProvederDto
            {
                FullName = user.FirstName +' '+user.LastName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenProvederDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserTokenProvederDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                FullName = user.FirstName
            };
        }

        //Forgot Password
        [HttpPost("[action]/{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountrepo.ForgotPassword(email);

                if (!result.IsValid)
                {
                    return Ok(new { Message = "Result Is Not Valid" });
                }
                var callbackUrl = string.Concat("/api/Account/ResetPassword", new { userId = (string)result.Data["User"].Id, code = (string)result.Data["Code"] });
                await _emailSender.SendEmailAsync(
                email,
                "Reset Password",
                //$"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                callbackUrl
                );
                return Ok(new { Message = "Success" });
            }
            return BadRequest("We have Encountered an Error");
        }


        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return BadRequest(new ApiResponse(401));
            }
            return RedirectToAction("ResetPassword", "Account", new ResetPasswordDto { Code = code });
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseObject>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return await _accountrepo.ResetPassword(resetPasswordDto);
        }
    }


}
