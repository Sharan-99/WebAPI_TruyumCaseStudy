using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPI_TruYumCaseStudy.Models
{
    public class MenuItemOperation<T>:IMenuItemOperation<T> where T:class
    {
        private readonly WebApiContext context;
        public MenuItemOperation(WebApiContext context)
        {
            this.context = context;
        }

        //public List<MenuItem> GetMenuItems()
        //{
        //    var menuItems = (from i in context.MenuItems
        //                                select i).ToList();
        //    return menuItems;
        //}
        public IReadOnlyList<T> GetMenuItems(Expression<Func<T, bool>> condition = null)
        {
            var entities = context.Set<T>();
            if (condition != null)
                return entities.Where(condition).ToList();
            return entities.ToList();
        }
        public T Add(T item)
        {
            return context.Add(item).Entity;
        }

        public T Delete(T item)
        {
            return context.Remove(item).Entity;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public T Update(T item)
        {
            return context.Update(item).Entity;
        }
    }
}
