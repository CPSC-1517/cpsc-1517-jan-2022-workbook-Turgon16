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
    public class SupplierServices
    {
        #region Setup of the Context Connection Variable and Class Constructor

        // variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal SupplierServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }
        #endregion

        #region Queries
        public List<Supplier> Supplier_List()
        {
            IEnumerable<Supplier> info = _context.Suppliers
                                        .OrderByDescending(x => x.CompanyName);
            return info.ToList();
        }
        #endregion
    }
}
