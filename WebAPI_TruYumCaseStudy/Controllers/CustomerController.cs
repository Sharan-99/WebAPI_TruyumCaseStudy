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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMenuItemOperation<MenuItem> repo;
        private readonly IMenuItemOperation<Cart> cartRepo;
        public CustomerController(IMenuItemOperation<MenuItem> repo, IMenuItemOperation<Cart> cartRepo)
        {
            this.repo = repo;
            this.cartRepo = cartRepo;
        }


      
        [HttpGet]
        public IActionResult Get()
        {
            var menuItems = repo.GetMenuItems(m => m.Active == true && m.LaunchDate<=DateTime.UtcNow);
            if (menuItems == null)
                return NoContent();
            return Ok(menuItems);
        }

        //[HttpGet]
        //[Route("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var cartDetails = cartRepo.GetMenuItems(c => c.UserId == id);
        //    if (cartDetails == null)
        //        return NoContent();
        //    var total = (from i in cartDetails
        //                 select i.MenuItem.Price).Sum();
        //    return Ok(total);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cartDetails = cartRepo.GetMenuItems(u => u.UserId == id);
            if (cartDetails == null)
                return NoContent();
            double totalPrice = 0;
            for (int i = 0; i < cartDetails.Count; i++)
            {
                var menuItem = repo.GetMenuItems(u => u.Id == cartDetails[i].MenuItemId).FirstOrDefault();
                totalPrice += menuItem.Price;
            }
            return Ok(new { cartDetails, totalPrice });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cart cart)
        {
            cartRepo.Add(cart);
            int rowsAffected = await cartRepo.SaveAsync();
            if (rowsAffected == 1)
                return Ok("Cart Item Added");
            return BadRequest("Unable to add card item");

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int menuItemId)
        {
            var cartItem = cartRepo.GetMenuItems(u => u.UserId == id && u.MenuItemId == menuItemId).FirstOrDefault();
            cartRepo.Delete(cartItem);
            int rowsAffected = await cartRepo.SaveAsync();
            if (rowsAffected == 1)
                return Ok("Item deleted");
            return BadRequest("failed");
        }
    }

}
