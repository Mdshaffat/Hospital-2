using AutoMapper;
using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserRepository _userrepo;
        private readonly IMapper _mapper;

        public UserManagementController(IUserRepository userrepo, IMapper mapper)
        {
            _userrepo = userrepo;
            _mapper = mapper;
        }

        [HttpGet("userlist")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUser()
        {
            var userList = await _userrepo.UserListAsync();

            return Ok(_mapper.Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<UserDto>>(userList));
        }
    }
}
