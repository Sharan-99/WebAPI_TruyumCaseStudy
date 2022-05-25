using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_TruYumCaseStudy.Models
{
    public class WebApiContext:DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options):base(options)
        {

        }

        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
