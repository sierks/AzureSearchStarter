using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureSearchStarter.DataAccess
{
    public class LocalDataSourceContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<AzureSearchStarter.Models.SuperHeroModel> MarvelSuperHeroes { get; set; }
    }
}