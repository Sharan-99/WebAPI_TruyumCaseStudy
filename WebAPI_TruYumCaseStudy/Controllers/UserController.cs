using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_TruYumCaseStudy.Models;

namespace WebAPI_TruYumCaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMenuItemOperation<User> repo;
        public UserController(IMenuItemOperation<User> repo)
        {
            this.repo = repo;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            repo.Add(user);
            int rowsAffected = await repo.SaveAsync();
            if (rowsAffected == 1)
                return Ok("User Information Added to Database");
            return BadRequest("Failed");
        }

        [HttpGet]
        public IActionResult Get(int id,[FromBody]string password)
        {
            var userdetails = repo.GetMenuItems(u => u.Id == id).FirstOrDefault();
            if (userdetails == null)
                return BadRequest("User does not exist");
            if (userdetails.Password == password)
                return Ok(true);
                // return RedirectToAction("Get", "Auth", new { userId = id });
                //return this.RedirectToAction<AuthController>(i => i.Get(id));
                //return RedirectToRoute(nameof(AuthController)+nameof(AuthController.Get),new { id = id });
            return BadRequest("UserName/Password is incorrect");
        }
    }
}
