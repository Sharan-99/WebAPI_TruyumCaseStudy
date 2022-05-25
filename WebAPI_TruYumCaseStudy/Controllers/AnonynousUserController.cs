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
   // [AllowAnonymous]
    public class AnonynousUserController : ControllerBase
    {
        //private readonly WebApiContext context;
        //public AnonynousUserController(WebApiContext context)
        //{
        //    this.context = context;
        //}

        private readonly IMenuItemOperation<MenuItem> repo;
        public AnonynousUserController(IMenuItemOperation<MenuItem> repo)
        {
            this.repo = repo;
        }
        public IActionResult Get()
        {
            var menu = repo.GetMenuItems();
            return Ok(menu);
        }
    }
}
