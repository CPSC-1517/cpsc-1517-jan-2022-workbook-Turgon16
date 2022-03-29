using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class CategoryServices
    {
        #region Setup of the Context Connection Variable and Class Constructor

        // variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal CategoryServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }
        #endregion

        #region Queries>
        public List<Category> Category_List()
        {
            IEnumerable<Category> info = _context.Categories
                                        .OrderByDescending(x => x.CategoryName;
            return info.ToList();
        }
        #endregion
    }
}
