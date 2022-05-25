using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMenuItemOperation<MenuItem> repo;
        public AdminController(IMenuItemOperation<MenuItem> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menu = repo.GetMenuItems();
            if (menu == null)
                return NoContent();
            return Ok(menu);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id,[FromBody] MenuItem menuitem)
        {
            var item = repo.GetMenuItems(m=>m.Id==id).FirstOrDefault();
            item.Name = menuitem.Name;
            item.Price = menuitem.Price;
            item.Active = menuitem.Active;
            item.LaunchDate = menuitem.LaunchDate;
            repo.Update(item);
            int rowsAffected = await repo.SaveAsync();
            if (rowsAffected == 1)
                return Ok(item);
            return BadRequest("Update failed");

        }

    }
}
