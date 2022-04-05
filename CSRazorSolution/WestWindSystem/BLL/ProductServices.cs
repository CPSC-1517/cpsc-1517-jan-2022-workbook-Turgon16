#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore.ChangeTracking; // for entityEntry<>
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region Setup of the Context Connection Variable and Class Constructor

        // variable to hold an instance of context class
        private readonly WestWindContext _context;

        //constructor to create an instance of the registered context class
        internal ProductServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }
        #endregion

        #region Queries
        public List<Product> Product_CategoryProducts(int categoryid)
        {
            IEnumerable<Product> info = _context.Products
                                .Where(x => x.CategoryID == categoryid)
                                .OrderBy(x => x.CategoryID);
            return info.ToList();
        }

        // for the CRUD you are maintaining a SINGLE row on the database
        // this row will be obtained by the primary key value of the row

        public Product Product_GetByID(int productid)
        {
            // where is matching on the primary key field. Linq by default
            // is expecting to return 0, 1, or more rows.
            // WHEN you receive variable (product) expects ONLY a SINGLE
            // instance of the class (Product), you will "tell" Linq to return
            // the 'first' instance found OR a 'default' value
            // of the variable data type (is a class thus it will be null)

            Product product = _context.Products
                                .Where(x => x.ProductID == productid)
                                .FirstOrDefault();

            return product;
        }


        #endregion

        #region Add, Update, Delete

        // ADD
        // Adding a record to your database may require nyou to verify the data does not already exist on the database.
        // This can be done using a Linq query and a given set of verification fields.
        // Example: for this product insertion we will verify that there is no product record on the database
        // with the same product name and quantity per unit from the same supplier. If so, throw an exception.

        // you MUST know whether the table has an identity PrimaryKey or not.
        // if the table PrimaryKey is NOT an identity then you MUST ensure 
        // that the incoming instance of the record/row has a primary key value.
        // If the table primary key is an identity, then the database will 
        // generate that value and make it assessable to you AFTER the data has been 
        // committed.

        //Product pkey is an identity attribute
        // this method optionally sends the new identity value back to the 
        // webpage (PageModel call statement)

        public int Product_AddProduct(Product item)
        {
            // this is an optional sample of validation of incoming data.
            Product exists = _context.Products
                            .Where(x => x.ProductName.Equals(item.ProductName) &&
                                        x.QuantityPerUnit.Equals(item.QuantityPerUnit) &&
                                        x.SupplierID == item.SupplierID)
                            .FirstOrDefault();
            if (exists != null)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier is already on file.");
            }

            // stage the data in local memory to be submitted to the database for storage
            // a) what DbSet
            // b) the action
            // c) indicate the data involved

            // IMPORTANT: the data is in LOCAL memory; it has NOT!!!! yet been sent to
            // the database.
            // Therefore at this time, there is NO!!!! primary key value (except
            // for the system default (numerics -> 0)
            
            _context.Products.Add(item);

            // commit the LOCAL data to the database
            _context.SaveChanges();

            //After the commit, your Primary Key value will NOW be available.
            return item.ProductID;
        }

        //UPDATE
        //update can also have design considerations
        //update may request that you check the record of interest is still on the database

        public int Product_UpdateProduct(Product item)
        {
            //for an update, you MUST have the pkey value on your instance
            //if not, it will not work.

            //This is one way to do it. This way returns an instance (object)
            /*            Product exists = _context.Products
                                        .Where(x => x.ProductID == item.ProductID)
                                        .FirstOrDefault();*/
            //Another way to do it... this does the search but returns only a boolean of success
            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);

            //DEPENDING on which technique you use, your error test will be different.
            //if (exists == null) {...}
            if (!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier is not on file.");
            }

            //stage the update
            EntityEntry<Product> updating = _context.Entry(item);
            //flag the entity to be modified
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //during the commit, SaveChanges() returns the number of rows affected
            return _context.SaveChanges();
        }

        //DELETE (disable)
        public int Product_DeleteProduct(Product item)
        {
            //for an delete, you MUST have the pkey value on your instance
            //if not, it probably will not work as expected.

            //This is one way to do it. This way returns an instance (object)
            /*            Product exists = _context.Products
                                        .Where(x => x.ProductID == item.ProductID)
                                        .FirstOrDefault();*/
            //Another way to do it... this does the search but returns only a boolean of success
            bool exists = _context.Products.Any(x => x.ProductID == item.ProductID);

            //DEPENDING on which technique you use, your error test will be different.
            //if (exists == null) {...}
            if (!exists)
            {
                throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit} from the selected supplier is not on file.");
            }

            // removing a record from your database may be
            // a) physical act
            // or a
            // b) logical act

            // a) if you wish the record to be 'physically removed' from the database,
            // you will use the staging of .Deleted
            // if the record being removed from the database is a "parent" record
            // (referenced in a foreign key), the delete WILL FAIL in a relational database IF 
            // there are existing "children" of the record.
            // sql "Parent records cannot be deleted if children exist"
            // the decision to automatically remove "children" is a system design decision.

            //stage the physical delete
            EntityEntry<Product> deleting = _context.Entry(item);

            //flag the entity to be deleted
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            // b) Logical delete
            // this is where you do not or cannot physically remove a record from the database.
            // this decision is based on existing best practice business rules OR
            // set by government regulations
            // this type of delete is done so the "flagged" data is NOT used in 
            // daily processing.

            // this type of delete will actually be an update to the attribute (property) of the record.
            // Look for attributes such as "Active", "Discontinued", a special date such as "ReleaseDate"

            // Product is a logical delete (Discontinued = true;)

            //stage the logical delete
            item.Discontinued = true;
            EntityEntry<Product> updating = _context.Entry(item);

            //flag the entity to be deleted
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //during the commit, SaveChanges() returns the number of rows affected
            return _context.SaveChanges();
        }

        #endregion
    }
}
