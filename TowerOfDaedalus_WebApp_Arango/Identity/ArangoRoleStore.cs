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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> CreateAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// deletes a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> DeleteAsync(Roles role, CancellationToken cancellationToken)
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Roles?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Roles?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedRoleNameAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetRoleNameAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Roles role, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(Roles role, string? roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> UpdateAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
