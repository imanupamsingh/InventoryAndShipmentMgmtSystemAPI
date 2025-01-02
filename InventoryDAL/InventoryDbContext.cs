using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;

namespace InventoryDAL
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext()
        {

        }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {

        }

        [Obsolete]
        public object FromSql(string v, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
