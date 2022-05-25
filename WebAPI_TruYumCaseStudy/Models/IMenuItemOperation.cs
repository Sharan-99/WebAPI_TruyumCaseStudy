using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPI_TruYumCaseStudy.Models
{
    public interface IMenuItemOperation<T> where T:class
    {
        T Add(T item);
        T Update(T item);
        T Delete(T item);
        IReadOnlyList<T> GetMenuItems(Expression<Func<T, bool>> condition = null);
        Task<int> SaveAsync();
    }
}
