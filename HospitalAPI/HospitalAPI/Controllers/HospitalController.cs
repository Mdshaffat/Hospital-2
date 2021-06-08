using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public HospitalController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetHospitalList() 
        {
            var hospitals =await context.Hospital.ToListAsync();
            return Ok(hospitals);

        }
        [HttpPost]
        public async Task<ActionResult> PostHospital(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                context.Add(hospital);
                await context.SaveChangesAsync();
                return Ok(await context.Hospital.ToListAsync());
            }
            return Ok(await context.Hospital.ToListAsync());
        }


    }
}
