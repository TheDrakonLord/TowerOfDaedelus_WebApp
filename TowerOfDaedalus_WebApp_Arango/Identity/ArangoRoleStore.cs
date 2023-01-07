using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;

namespace TowerOfDaedalus_WebApp_Arango.Identity
{
    /// <summary>
    /// Custome Role Store class that changes the data provider for Microsoft Identity from SQL to Arango
    /// </summary>
    public class ArangoRoleStore : IRoleStore<Roles>, IQueryableRoleStore<Roles>
    {
        /// <summary>
        /// a navigation property for the roles the store contains
        /// </summary>
        public IQueryable<Roles> Roles => throw new NotImplementedException();

        /// <summary>
        /// creates a new role in a store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role to create in the store</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that represents the IdentityResult of the asynchronous query</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> CreateAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// deletes a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role to delete from the store</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that represents the IdentityResult of the asynchronous query</returns>
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
        /// <param name="roleId">The role ID to look for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that result of the look up</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Roles?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation
        /// </summary>
        /// <param name="normalizedRoleName">The normalized role name to look for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that result of the look up</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Roles?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a role's normalized name as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose normalized name should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that contains the name of the role</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetNormalizedRoleNameAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the ID for a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role">the role whose ID should be returned</param>
        /// <param name="cancellationToken">the CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that contains the ID of the role</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetRoleIdAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the name of a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose name should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that contains the name of the role</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetRoleNameAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set a role's normalized name as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose normalized name should be set</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetNormalizedRoleNameAsync(Roles role, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the name of a role in the store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose name should be set</param>
        /// <param name="roleName">The name of the role</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetRoleNameAsync(Roles role, string? roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to update in the store</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that represents the IdentityResult of the asynchronous query</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> UpdateAsync(Roles role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
