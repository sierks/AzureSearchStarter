using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureSearchStarter.DataAccess
{
    public class AzureDataSourceContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<AzureSearchStarter.Models.SuperHeroModel> DCSuperHeroes { get; set; }
    }

    public class AzureDataSourceConfiguration : System.Data.Entity.DbConfiguration
    {
        public AzureDataSourceConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new System.Data.Entity.SqlServer.SqlAzureExecutionStrategy(2, new TimeSpan(0, 0, 30)));
        }
    }
}