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
            

            PostDocumentResponse<Roles> roleResponse = await db.Document.PostDocumentAsync<Roles>(ArangoSchema.collRoles, role, token: cancellationToken);

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
            

            Roles result = await db.Document.GetDocumentAsync<Roles>(ArangoSchema.collRoles, role.Id);

            return result.NormalizedName;
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
            return role.Id;
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
            return role.Name;
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
            role.NormalizedName = normalizedName;
            await UpdateAsync(role, cancellationToken);
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
            role.Name = roleName;
            await UpdateAsync(role, cancellationToken);
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
            

            if (role._key is null)
            {
                PostDocumentResponse<Roles> postResponse = await db.Document.PostDocumentAsync<Roles>(ArangoSchema.collRoles, role, token: cancellationToken);
            }
            else
            {
                PatchDocumentQuery patchQuery = new PatchDocumentQuery();
                patchQuery.KeepNull = true;

                PatchDocumentResponse<object> patchResponse = await db.Document.PatchDocumentAsync<Roles>($"{ArangoSchema.collRoles}/{role._key}", role, patchQuery, cancellationToken);
            }
            return IdentityResult.Success;
        }
    }
}
