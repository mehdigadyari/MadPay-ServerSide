using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadPay.Data.DatabaseContext;
using MadPay.Data.Models;
using MadPay.Repo.Infrastructure;
using MadPay.Services.Site.Admin.Auth.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MadPay.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork<MadpayDbContext> _db;
    //    private readonly IAuthService _authService;
        public ValuesController(IUnitOfWork<MadpayDbContext> dbContext)
        {
            _db = dbContext;
           // _authService = authService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
          //    return new string[] { "value1", "value2" };

            var user = new User()
            {
                Address = "",
                City = "",
                //  DateOfBirth = "",
                Gender = true,
                IsAcive = true,
                Name = "",

                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },

                PhoneNumber = "",
                Status = true,
                UserName = ""
            };

            await _db.UserRepository.InsertAsync(user);
            await _db.SaveAsync();

            var model = await _db.UserRepository.GetAllAsync();
            //var u = await _authService.Register(user, "asdkasld");

            return Ok(model);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
