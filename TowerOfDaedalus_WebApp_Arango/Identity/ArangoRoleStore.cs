using ArangoDBNetStandard.Transport.Http;
using ArangoDBNetStandard;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.GraphApi;
using ArangoDBNetStandard.GraphApi.Models;
using TowerOfDaedalus_WebApp_Arango.Schema;
using ArangoDBNetStandard.CursorApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;
using Microsoft.Extensions.Logging;

namespace TowerOfDaedalus_WebApp_Arango.Identity
{
    /// <summary>
    /// Custome Role Store class that changes the data provider for Microsoft Identity from SQL to Arango
    /// </summary>
    public class ArangoRoleStore : IRoleStore<Roles>
    {
        private HttpApiTransport? transport;
        private static ILogger<ArangoRoleStore> _logger;
        private ArangoDBClient? db;
        private bool disposed_ = false;
        private bool created_ = false;

        /// <summary>
        /// 
        /// </summary>
        private async Task CreateConnection()
        {
            if (!created_)
            {
                transport = HttpApiTransport.UsingBasicAuth(new Uri(ArangoDbContext.getUrl()), ArangoDbContext.getDbName(), ArangoDbContext.getNewUsername(), ArangoDbContext.getNewPass());
                db = new ArangoDBClient(transport);
                created_ = true;
            }
        }

        private async Task<Roles?> getExistingRole(Roles role, CancellationToken cancellationToken)
        {
            _logger.LogDebug("getExistingRole || obtaining existing role");
            ArangoQueryBuilder existingQB = new ArangoQueryBuilder(ArangoSchema.collRoles);
            existingQB.filter("Name", role.Name);
            existingQB.limit(1);
            string query = existingQB.ToString();
            _logger.LogDebug("getExistingRole || running query: {query}", query);
            CursorResponse<Roles> queryResponse = await db.Cursor.PostCursorAsync<Roles>(query, token: cancellationToken);

            if (queryResponse.Result.Any())
            {
                _logger.LogDebug("getExistingRole || found the requested role");
                Roles existingRole = queryResponse.Result.First();
                return existingRole;
            }
            else
            {
                _logger.LogDebug("getExistingUser || Could not find a role with the naem {name}", role.Name);
                return null;
            }
        }

        private async Task<string> getExistingRoleKey(Roles role, CancellationToken cancellationToken)
        {
            _logger.LogDebug("getExistingUserKey || getting _key from existing role");
            Roles? existingRole = await getExistingRole(role, cancellationToken);
            if (existingRole == null)
            {
                _logger.LogError("could not find an existing role when we need one");
                throw new ArgumentNullException(nameof(existingRole));
            }
            _logger.LogDebug("getExistingRoleKey || returning key");
            return existingRole._key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ArangoRoleStore(ILogger<ArangoRoleStore> logger)
        {
            _logger = logger;
            CreateConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        ~ArangoRoleStore()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// creates a new role in a store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role to create in the store</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that represents the IdentityResult of the asynchronous query</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> CreateAsync(Roles role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                _logger.LogError("A role must be supplied to create");
                return IdentityResult.Failed();
            }

            Roles? existingRole = await getExistingRole(role, cancellationToken);

            if (existingRole != null)
            {
                PatchDocumentQuery patchQuery = new PatchDocumentQuery();
                patchQuery.KeepNull = true;

                PatchDocumentResponse<object> patchRespone = await db.Document.PatchDocumentAsync<Roles>($"{ArangoSchema.collRoles}/{existingRole._key}", role, patchQuery, cancellationToken);
            }
            else
            {
                PostDocumentResponse<Roles> roleResponse = await db.Document.PostDocumentAsync<Roles>(ArangoSchema.collRoles, role, token:  cancellationToken);
            }

            return IdentityResult.Success;
        }

        /// <summary>
        /// deletes a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role to delete from the store</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that represents the IdentityResult of the asynchronous query</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> DeleteAsync(Roles role, CancellationToken cancellationToken)
        {
            if (role is null)
            {
                _logger.LogError("a role must be supplied to delete");
                return IdentityResult.Failed();
            }

            role._key = await getExistingRoleKey(role, cancellationToken);

            DeleteDocumentResponse<Roles> deleteResponse = await db.Document.DeleteDocumentAsync<Roles>(ArangoSchema.collRoles, role._key, token: cancellationToken);

            return IdentityResult.Success;
        }

        /// <summary>
        /// Dispose the store
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed_)
            {
                if (disposing && created_)
                {
                    db.Dispose();
                    transport.Dispose();
                    created_ = false;
                }

                disposed_ = true;
            }
        }

        /// <summary>
        /// Finds the role who has the specified ID as an asynchornous operation
        /// </summary>
        /// <param name="roleId">The role ID to look for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that result of the look up</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Roles?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.collRoles);
            qb.filter("Id", roleId);
            qb.limit(1);
            string query = qb.ToString();

            CursorResponse<Roles> queryResponse = await db.Cursor.PostCursorAsync<Roles>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                Roles queryResult = queryResponse.Result.First();
                return queryResult;
            }
            else
            {
                _logger.LogDebug($"could not find any roles with the id {roleId}");
                return null;
            }
        }

        /// <summary>
        /// Finds the role who has the specified normalized name as an asynchronous operation
        /// </summary>
        /// <param name="normalizedRoleName">The normalized role name to look for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that result of the look up</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Roles?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.collRoles);
            qb.filter("NormalizedName", normalizedRoleName);
            qb.limit(1);
            string query = qb.ToString();

            CursorResponse<Roles> queryResponse = await db.Cursor.PostCursorAsync<Roles>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                Roles queryResult = queryResponse.Result.First();
                return queryResult;
            }
            else
            {
                _logger.LogDebug($"could not find any roles with the rolename {normalizedRoleName}");
                return null;
            }
        }

        /// <summary>
        /// Get a role's normalized name as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose normalized name should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that contains the name of the role</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetNormalizedRoleNameAsync(Roles role, CancellationToken cancellationToken)
        {
            if (role is null)
            {
                _logger.LogError("a role must be supplied to retrieve");
                throw new ArgumentNullException(nameof(role));
            }

            Roles? existingRole = await getExistingRole(role, cancellationToken);

            if (existingRole != null)
            {
                return existingRole.NormalizedName;
            }
            else
            {
                _logger.LogError("An existing role was not found");
                return null;
            }    
        }

        /// <summary>
        /// Gets the ID for a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role">the role whose ID should be returned</param>
        /// <param name="cancellationToken">the CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that contains the ID of the role</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetRoleIdAsync(Roles role, CancellationToken cancellationToken)
        {
            if (role is null)
            {
                _logger.LogError("a role must be supplied to retrieve id");
                throw new ArgumentNullException(nameof(role));
            }

            Roles? existingRole = await getExistingRole(role, cancellationToken);

            if (existingRole != null)
            {
                return existingRole.Id;
            }
            else if (role.Id != null)
            {
                return role.Id;
            }
            else
            {
                _logger.LogError("An existing role was not found");
                throw new ArgumentNullException(nameof(existingRole));
            }
        }

        /// <summary>
        /// Gets the name of a role from the store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose name should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that contains the name of the role</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetRoleNameAsync(Roles role, CancellationToken cancellationToken)
        {
            if (role is null)
            {
                _logger.LogError("a role must be supplied to retrieve name");
                throw new ArgumentNullException(nameof(role));
            }

            Roles? existingRole = await getExistingRole(role, cancellationToken);

            if (existingRole != null)
            {
                return existingRole.Name;
            }
            else
            {
                _logger.LogError("An existing role was not found");
                return null;
            }
        }

        /// <summary>
        /// Set a role's normalized name as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose normalized name should be set</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetNormalizedRoleNameAsync(Roles role, string? normalizedName, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                role.NormalizedName = normalizedName;
                await UpdateAsync(role, cancellationToken);
            }
            else
            {
                _logger.LogError("The role parameter cannot be null");
                throw new ArgumentNullException(nameof(role));
            }
        }

        /// <summary>
        /// Sets the name of a role in the store as an asynchronous operation
        /// </summary>
        /// <param name="role">The role whose name should be set</param>
        /// <param name="roleName">The name of the role</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetRoleNameAsync(Roles role, string? roleName, CancellationToken cancellationToken)
        {
            if (role != null)
            {
                role.Name = roleName;
                await UpdateAsync(role, cancellationToken);
            }
            else
            {
                _logger.LogError("The role parameter cannot be null");
                throw new ArgumentNullException(nameof(role));
            }
        }

        /// <summary>
        /// Updates a role in a store as an asynchronous operation.
        /// </summary>
        /// <param name="role">The role to update in the store</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>A Task that represents the IdentityResult of the asynchronous query</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> UpdateAsync(Roles role, CancellationToken cancellationToken)
        {
            Roles? existingRole = await getExistingRole(role, cancellationToken);

            if (existingRole != null)
            {
                PatchDocumentQuery patchQuery = new PatchDocumentQuery();
                patchQuery.KeepNull = true;

                PatchDocumentResponse<object> patchResponse = await db.Document.PatchDocumentAsync<Roles>($"{ArangoSchema.collRoles}/{existingRole._key}", role, patchQuery, cancellationToken);
            }
            else
            {
                PostDocumentResponse<Roles> postResponse = await db.Document.PostDocumentAsync<Roles>(ArangoSchema.collRoles, role, token: cancellationToken);
            }
            return IdentityResult.Success;
        }
    }
}
