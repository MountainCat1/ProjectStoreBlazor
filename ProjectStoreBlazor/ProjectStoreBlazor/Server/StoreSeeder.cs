using Microsoft.EntityFrameworkCore;
using ProjectStoreBlazor.Server.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjectStoreBlazor.Server
{
    public class StoreSeeder
    {
        private readonly StoreDbContext _dbContext;
        public StoreSeeder( StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (_dbContext.Database.IsRelational())
                {
                    var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                    if (pendingMigrations != null && pendingMigrations.Any())
                    {
                        _dbContext.Database.Migrate();
                    }
                }


                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

               
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                Name = "Seller"
            },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        }

       

    }
}

