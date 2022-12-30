using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;

namespace TowerOfDaedalus_WebApp_Arango
{
    internal class ArangoRoleStore : IRoleStore<Roles>, IQueryableRoleStore<Roles>
    {
        public IQueryable<Roles> Roles => throw new NotImplementedException();

        public Task CreateAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Roles> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Roles> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Roles role)
        {
            throw new NotImplementedException();
        }
    }
}
