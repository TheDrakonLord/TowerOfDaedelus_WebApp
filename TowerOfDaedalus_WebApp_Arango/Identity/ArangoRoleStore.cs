using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;

namespace TowerOfDaedalus_WebApp_Arango.Identity
{
    public class ArangoRoleStore : IRoleStore<Roles>, IQueryableRoleStore<Roles>
    {
        /// <summary>
        /// a navigation property for hte roles the store contains
        /// </summary>
        public IQueryable<Roles> Roles => throw new NotImplementedException();

        /// <summary>
        /// creates a new role in a store as an asynchronous operation
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task CreateAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// deletes a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// dispose the stores
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the role who has the specified ID as an asynchornous operation
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Roles> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Roles> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateAsync(Roles role)
        {
            throw new NotImplementedException();
        }
    }
}
