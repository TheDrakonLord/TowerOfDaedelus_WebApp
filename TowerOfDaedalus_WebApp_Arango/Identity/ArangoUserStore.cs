using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport.Http;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;
using ArangoDBNetStandard.DocumentApi;
using ArangoDBNetStandard.DocumentApi.Models;
using ArangoDBNetStandard.GraphApi;
using ArangoDBNetStandard.GraphApi.Models;
using TowerOfDaedalus_WebApp_Arango.Schema;
using ArangoDBNetStandard.CursorApi.Models;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Collections;

namespace TowerOfDaedalus_WebApp_Arango.Identity
{
    /// <summary>
    /// custom User Store class that changes the data provider for the Microsoft Identity Framework from SQL to Arango
    /// </summary>
    public class ArangoUserStore : IUserStore<Users>, IUserRoleStore<Users>, IUserClaimStore<Users>, IUserPasswordStore<Users>,
        IUserSecurityStampStore<Users>, IUserEmailStore<Users>, IUserPhoneNumberStore<Users>, IUserLoginStore<Users>,
        IUserTwoFactorStore<Users>, IUserLockoutStore<Users>
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
        ~ArangoUserStore()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Adds the claims given to the specified user
        /// </summary>
        /// <param name="user">The user to add the claim to</param>
        /// <param name="claims">The claim to add to the user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await CreateConnection();

            if (user.Id is null)
            {
                //_logger.LogError("a user id must be supplied to add claims");
                return;
            }
            List<Edges> edges = new List<Edges>();

            foreach (Claim item in claims)
            {
                PostDocumentResponse<ArangoClaims> docResponse = await db.Document.PostDocumentAsync<ArangoClaims>(ArangoSchema.collUserClaims, new ArangoClaims(item), token: cancellationToken);
                Edges edge = new Edges($"{ArangoSchema.collUsers}/{user._key}",$"{ArangoSchema.collUserClaims}/{docResponse._key}");
                edge.Type = "Claim";
                edges.Add(edge);
            }

            foreach (Edges item in edges)
            {
                PostEdgeResponse<Edges> edgeResponse = await db.Graph.PostEdgeAsync<Edges>(ArangoSchema.graphPrimary, ArangoSchema.edgeUserClaims, item, token: cancellationToken);

                if (edgeResponse.Error)
                {
                    //_logger.LogError("failed to post edge for user claim");
                }
            }

            return;
        }

        /// <summary>
        /// Adds the login given to the specified user
        /// </summary>
        /// <param name="user">The user to add the login to</param>
        /// <param name="login">The login to add to the user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddLoginAsync(Users user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            await CreateConnection();

            if (user.Id is null)
            {
                //_logger.LogError("a user id must be supplied to add claims");
                return;
            }
            UserLogins newLogin = new UserLogins(login.LoginProvider, login.ProviderKey, login.ProviderDisplayName, user.Id);
            PostDocumentResponse<UserLogins> docResponse = await db.Document.PostDocumentAsync<UserLogins>(ArangoSchema.collUserLogins, newLogin, token: cancellationToken);
            Edges edge = new Edges($"{ArangoSchema.collUsers}/{user._key}",$"{ArangoSchema.collUserLogins}/{docResponse._key}");
            edge.Type = "Login";

            PostEdgeResponse<Edges> edgeResponse = await db.Graph.PostEdgeAsync<Edges>(ArangoSchema.graphPrimary, ArangoSchema.edgeUserLogins, edge, token: cancellationToken);

            if (edgeResponse.Error)
            {
                //_logger.LogError("failed to post edge for user login");
            }

            return;
        }

        /// <summary>
        /// Adds the given normalizedRolename to the specified user
        /// </summary>
        /// <param name="user">The user to add the role to</param>
        /// <param name="roleName">The role to add</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddToRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            await CreateConnection();

            PostCursorBody roleQuery = new PostCursorBody();
            roleQuery.BatchSize = 1;
            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.collRoles);
            qb.filter("Name", roleName);
            qb.limit(1);
            roleQuery.Query = qb.ToString();

            CursorResponse<Roles> queryResponse = await db.Cursor.PostCursorAsync<Roles>(roleQuery, token: cancellationToken);
            Roles queryResult = queryResponse.Result.First();

            if (queryResponse.Error)
            {
                //_logger.LogError("Query to obtain role failed");
            }

            if (queryResponse.Result == null)
            {
                throw new ArgumentNullException("queryResponse.Result");
            }

            Edges edgeA = new Edges($"{ArangoSchema.collUsers}/{user._key}",$"{ArangoSchema.collRoles}/{queryResult._key}");
            edgeA.Type = "UserRole";
            Edges edgeB = new Edges($"{ArangoSchema.collRoles}/{queryResult._key}",$"{ArangoSchema.collUsers}/{user._key}");
            edgeB.Type = "UserRole";

            PostEdgeResponse<Edges> edgeResponseA = await db.Graph.PostEdgeAsync<Edges>(ArangoSchema.graphPrimary, ArangoSchema.edgeUserRoles, edgeA, token: cancellationToken);
            if (edgeResponseA.Error)
            {
                //_logger.LogError("failed to post edge for user role");
            }
            PostEdgeResponse<Edges> edgeResponseB = await db.Graph.PostEdgeAsync<Edges>(ArangoSchema.graphPrimary, ArangoSchema.edgeRoleUsers, edgeB, token: cancellationToken);
            if (edgeResponseB.Error)
            {
                //_logger.LogError("failed to post edge for role user");
            }

            return;
        }

        /// <summary>
        /// Creates the specified user in the user store
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            PostDocumentResponse<Users> userResponse = await db.Document.PostDocumentAsync<Users>(ArangoSchema.collUsers, user, token: cancellationToken);

            return IdentityResult.Success;
        }

        /// <summary>
        /// Deletes the specified user from the user store
        /// </summary>
        /// <param name="user">The user to delete</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            DeleteDocumentResponse<Users> deleteResponse = await db.Document.DeleteDocumentAsync<Users>(ArangoSchema.collUsers, user._key, token: cancellationToken);

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
        /// Gets the user, if any, associated with the specified, normalized email address
        /// </summary>
        /// <param name="normalizedEmail">The normalized email address to return the user for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Users?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.collUsers);
            qb.filter("NormalizedEmail", normalizedEmail);
            qb.limit(1);
            string query = qb.ToString();

            CursorResponse<Users> queryResponse = await db.Cursor.PostCursorAsync<Users>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                Users queryResult = queryResponse.Result.First();
                return queryResult;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId
        /// </summary>
        /// <param name="userId">The user ID to search for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Users?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.collUsers);
            qb.filter("Id", userId);
            qb.limit(1);
            string query = qb.ToString();

            CursorResponse<Users> queryResponse = await db.Cursor.PostCursorAsync<Users>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                Users queryResult = queryResponse.Result.First();
                return queryResult;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the user associated with the specified login provider and login provider key
        /// </summary>
        /// <param name="loginProvider">The login provider who provided the providerKey</param>
        /// <param name="providerKey">The key provided by the loginProvider to identify a user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Users?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder loginQb = new ArangoQueryBuilder(ArangoSchema.collUserLogins);
            loginQb.filter("LoginProvider", loginProvider);
            loginQb.filter("ProviderKey", providerKey);
            loginQb.limit(1);
            string loginQuery = loginQb.ToString();

            CursorResponse<UserLogins> loginResponse = await db.Cursor.PostCursorAsync<UserLogins>(loginQuery, token: cancellationToken);
            if (loginResponse.Result.Any())
            {
                UserLogins loginResult = loginResponse.Result.First();
                ArangoQueryBuilder edgeQb = new ArangoQueryBuilder(ArangoSchema.edgeUserLogins);
                edgeQb.filter("_to", $"{ArangoSchema.collUserLogins}/{loginResult._key}");
                edgeQb.filter("Type", "Login");
                edgeQb.limit(1);
                string edgeQuery = edgeQb.ToString();
                CursorResponse<Edges> edgeResponse = await db.Cursor.PostCursorAsync<Edges>(edgeQuery, token: cancellationToken);
                if (edgeResponse.Result.Any())
                {
                    Edges edgeResult = edgeResponse.Result.First();
                    string[] edgeFrom = edgeResult._from.Split('/');
                    Users result = await db.Document.GetDocumentAsync<Users>(ArangoSchema.collUsers, edgeFrom[1]);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name
        /// </summary>
        /// <param name="normalizedUserName">The normalized user name to search for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Users?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.collUsers);
            qb.filter("NormalizedUserName", normalizedUserName);
            qb.limit(1);
            string query = qb.ToString();

            CursorResponse<Users> queryResponse = await db.Cursor.PostCursorAsync<Users>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                Users queryResult = queryResponse.Result.First();
                return queryResult;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the current failed access count for the specified user
        /// </summary>
        /// <param name="user">The user whose failed access count should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            Users result = await db.Document.GetDocumentAsync<Users>(ArangoSchema.collUsers, user.Id);

            return result.AccessFailedCount;
        }

        /// <summary>
        /// get the claims associated with the specified user as an asynchronous operation
        /// </summary>
        /// <param name="user">The user whose claims should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<Claim>> GetClaimsAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            List<Claim> result = new List<Claim>();
            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.edgeUserClaims);
            qb.filter("_from", $"{ArangoSchema.collUsers}/{user._key}");
            qb.filter("Type", "Claim");
            string query = qb.ToString();
            CursorResponse<Edges> queryResponse = await db.Cursor.PostCursorAsync<Edges>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                foreach (Edges item in queryResponse.Result)
                {
                    string[] edgeTo = item._to.Split('/');
                    ArangoClaims itemClaim = await db.Document.GetDocumentAsync<ArangoClaims>(ArangoSchema.collUserClaims, edgeTo[1]);
                    result.Add(itemClaim.getClaim());
                }
                bool hasMore = queryResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(queryResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {

                        string[] edgeTo = item._to.Split('/');
                        ArangoClaims itemClaim = await db.Document.GetDocumentAsync<ArangoClaims>(ArangoSchema.collUserClaims, edgeTo[1]);
                        result.Add(itemClaim.getClaim());
                    }
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// Gets the email address for the specified user
        /// </summary>
        /// <param name="user">The user whose email should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetEmailAsync(Users user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified user has been verified.
        /// true if the email address is verified otherwise false
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> GetEmailConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            return user.EmailConfirmed;
        }

        /// <summary>
        /// Retrieves a flag indicating whether user lockout can be enabled for a specified user
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> GetLockoutEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            return user.LockoutEnabled;
        }

        /// <summary>
        /// Gets the last DateTimeOffset a user's last lockout expired, if any. Any time in the past should
        /// indicate a user is not locked out
        /// </summary>
        /// <param name="user">The user whose lockout date should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(Users user, CancellationToken cancellationToken)
        {
            return user.LockoutEnd;
        }

        /// <summary>
        /// retrieves the associated logins for the specified user
        /// </summary>
        /// <param name="user">The user whose associated logins to retrieve</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            List<UserLoginInfo> result = new List<UserLoginInfo>();
            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.edgeUserLogins);
            qb.filter("_from", $"{ArangoSchema.collUsers}/{user._key}");
            qb.filter("Type", "Login");
            string query = qb.ToString();
            CursorResponse<Edges> queryResponse = await db.Cursor.PostCursorAsync<Edges>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                foreach (Edges item in queryResponse.Result)
                {
                    string[] edgeTo = item._to.Split('/');
                    UserLogins login = await db.Document.GetDocumentAsync<UserLogins>(ArangoSchema.collUserLogins, edgeTo[1]);
                    result.Add(login.getUserLoginInfo());
                }
                bool hasMore = queryResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(queryResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        string[] edgeTo = item._to.Split('/');
                        UserLogins login = await db.Document.GetDocumentAsync<UserLogins>(ArangoSchema.collUserLogins, edgeTo[1]);
                        result.Add(login.getUserLoginInfo());
                    }
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// returns the normalized email for the specicfied user
        /// </summary>
        /// <param name="user">The user whose email address to retrieve</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetNormalizedEmailAsync(Users user, CancellationToken cancellationToken)
        {
            return user.NormalizedEmail;
        }

        /// <summary>
        /// gets the normalized username for the specified user
        /// </summary>
        /// <param name="user">The user whose normalized name should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return user.NormalizedUserName;
        }

        /// <summary>
        /// gets the password hash for a user
        /// </summary>
        /// <param name="user">The user to retrieve the password hash for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            return user.PasswordHash;
        }

        /// <summary>
        /// Gets the telephone number, if any, for the specified user
        /// </summary>
        /// <param name="user">The user whose telephone number should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetPhoneNumberAsync(Users user, CancellationToken cancellationToken)
        {
            return user.PhoneNumber;
        }

        /// <summary>
        /// gets a flag indicating whether the specified user's telephone number has been confirmed
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether their telephone number is confirmed</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> GetPhoneNumberConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            return user.PhoneNumberConfirmed;
        }

        /// <summary>
        /// retrieves the soles the specified user is a member of
        /// </summary>
        /// <param name="user">The user whose roles should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<string>> GetRolesAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            List<string> result = new List<string>();
            ArangoQueryBuilder qb = new ArangoQueryBuilder(ArangoSchema.edgeUserRoles);
            qb.filter("_from", $"{ArangoSchema.collUsers}/{user._key}");
            qb.filter("Type", "UserRole");
            string query = qb.ToString();
            CursorResponse<Edges> queryResponse = await db.Cursor.PostCursorAsync<Edges>(query, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                foreach (Edges item in queryResponse.Result)
                {
                    string[] edgeTo = item._to.Split('/');
                    Roles role = await db.Document.GetDocumentAsync<Roles>(ArangoSchema.collRoles, edgeTo[1]);
                    result.Add(role.Name);
                }
                bool hasMore = queryResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(queryResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        string[] edgeTo = item._to.Split('/');
                        Roles role = await db.Document.GetDocumentAsync<Roles>(ArangoSchema.collRoles, edgeTo[1]);
                        result.Add(role.Name);
                    }
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// get the security stamp for the specified user
        /// </summary>
        /// <param name="user">The user whose security stamp should be set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetSecurityStampAsync(Users user, CancellationToken cancellationToken)
        {
            return user.SecurityStamp;
        }

        /// <summary>
        /// returns a flag indicating whether the specified user has two factor authentication enabled
        /// or not, as an asynchronous operation
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> GetTwoFactorEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            return user.TwoFactorEnabled;
        }

        /// <summary>
        /// gets the user identifier for the specified user
        /// </summary>
        /// <param name="user">The user whose identifier should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            return user.Id;
        }

        /// <summary>
        /// gets the user name for the specified user
        /// </summary>
        /// <param name="user">The user whose name should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        /// <summary>
        /// retrieves all users with the specified claim
        /// </summary>
        /// <param name="claim">The claim whose users should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<Users>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder claimQb = new ArangoQueryBuilder(ArangoSchema.collUserClaims);
            claimQb.filter("Value", claim.Value);
            claimQb.filter("Type", claim.Type);
            claimQb.filter("Issuer", claim.Issuer);
            claimQb.limit(1);
            string claimQuery = claimQb.ToString();
            CursorResponse<ArangoClaims> claimResponse = await db.Cursor.PostCursorAsync<ArangoClaims>(claimQuery, token: cancellationToken);
            string claimKey = claimResponse.Result.First()._key;

            List<Users> result = new List<Users>();
            ArangoQueryBuilder edgesQb = new ArangoQueryBuilder(ArangoSchema.edgeUserClaims);
            edgesQb.filter("_to", $"{ArangoSchema.collUserClaims}/{claimKey}");
            edgesQb.filter("Type", "Claim");
            string edgesQuery = edgesQb.ToString();
            CursorResponse<Edges> queryResponse = await db.Cursor.PostCursorAsync<Edges>(edgesQuery, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                foreach (Edges item in queryResponse.Result)
                {
                    string[] edgeFrom = item._from.Split('/');
                    Users user = await db.Document.GetDocumentAsync<Users>(ArangoSchema.collUsers, edgeFrom[1]);
                    result.Add(user);
                }
                bool hasMore = queryResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(queryResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        string[] edgeFrom = item._from.Split('/');
                        Users user = await db.Document.GetDocumentAsync<Users>(ArangoSchema.collUsers, edgeFrom[1]);
                        result.Add(user);
                    }
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// retrieves al users in the specified role
        /// </summary>
        /// <param name="roleName">The role whose users should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<Users>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder roleQb = new ArangoQueryBuilder(ArangoSchema.collRoles);
            roleQb.filter("Name", roleName);
            roleQb.limit(1);
            string roleQuery = roleQb.ToString();
            CursorResponse<ArangoClaims> roleResponse = await db.Cursor.PostCursorAsync<ArangoClaims>(roleQuery, token: cancellationToken);
            string roleKey = roleResponse.Result.First()._key;

            List<Users> result = new List<Users>();
            ArangoQueryBuilder edgesQb = new ArangoQueryBuilder(ArangoSchema.edgeRoleUsers);
            edgesQb.filter("_from", $"{ArangoSchema.collRoles}/{roleKey}");
            edgesQb.filter("Type", "UserRole");
            string edgesQuery = edgesQb.ToString();
            CursorResponse<Edges> queryResponse = await db.Cursor.PostCursorAsync<Edges>(edgesQuery, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                foreach (Edges item in queryResponse.Result)
                {
                    string[] edgeTo = item._to.Split('/');
                    Users user = await db.Document.GetDocumentAsync<Users>(ArangoSchema.collUsers, edgeTo[1]);
                    result.Add(user);
                }
                bool hasMore = queryResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(queryResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        string[] edgeTo = item._to.Split('/');
                        Users user = await db.Document.GetDocumentAsync<Users>(ArangoSchema.collUsers, edgeTo[1]);
                        result.Add(user);
                    }
                }
                return result;
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// returns a flag indicating if the specified user has a password
        /// </summary>
        /// <param name="user">The user to retrieve the password hash for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            if (user.PasswordHash is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// records that a failed access has occurred, incrementing the failed access count
        /// </summary>
        /// <param name="user">The user whose cancellation count should be incremented</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> IncrementAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount++;
            return user.AccessFailedCount;
        }

        /// <summary>
        /// returns a flag indicating if the specified user is a member of the given normalizedRoleName
        /// </summary>
        /// <param name="user">The user whose role membership should be checked</param>
        /// <param name="roleName">The role to check membership of</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> IsInRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder roleQb = new ArangoQueryBuilder(ArangoSchema.collRoles);
            roleQb.filter("Name", roleName);
            roleQb.limit(1);
            string roleQuery = roleQb.ToString();
            CursorResponse<ArangoClaims> roleResponse = await db.Cursor.PostCursorAsync<ArangoClaims>(roleQuery, token: cancellationToken);
            string roleKey = roleResponse.Result.First()._key;

            ArangoQueryBuilder edgesQb = new ArangoQueryBuilder(ArangoSchema.edgeRoleUsers);
            edgesQb.filter("_from", $"{ArangoSchema.collRoles}/{roleKey}");
            edgesQb.filter("_to", $"{ArangoSchema.collUsers}/{user.Id}");
            edgesQb.filter("Type", "UserRole");
            string edgesQuery = edgesQb.ToString();
            CursorResponse<Edges> queryResponse = await db.Cursor.PostCursorAsync<Edges>(edgesQuery, token: cancellationToken);
            if (queryResponse.Result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// removes the claims given from the specified user
        /// </summary>
        /// <param name="user">The user to remove the claims from</param>
        /// <param name="claims">The claim to remove</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RemoveClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder edgeQb = new ArangoQueryBuilder(ArangoSchema.edgeUserClaims);
            edgeQb.filter("_from", $"{ArangoSchema.collUsers}/{user.Id}");
            edgeQb.filter("Type", "Claim");
            string edgeQuery = edgeQb.ToString();
            CursorResponse<Edges> edgeResponse = await db.Cursor.PostCursorAsync<Edges>(edgeQuery, token: cancellationToken);
            List<ArangoClaims> storedClaims = new List<ArangoClaims>();
            List<Edges> edgeList = new List<Edges>();
            if (edgeResponse.Result.Any())
            {
                foreach (Edges item in edgeResponse.Result)
                {
                    string[] edgeTo = item._to.Split('/');
                    ArangoClaims claim = await db.Document.GetDocumentAsync<ArangoClaims>(ArangoSchema.collUserClaims, edgeTo[1]);
                    storedClaims.Add(claim);
                    edgeList.Add(item);
                }
                bool hasMore = edgeResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(edgeResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        string[] edgeTo = item._to.Split('/');
                        ArangoClaims claim = await db.Document.GetDocumentAsync<ArangoClaims>(ArangoSchema.collUserClaims, edgeTo[1]);
                        storedClaims.Add(claim);
                        edgeList.Add(item);
                    }
                }

                List<string> claimsToDelete = new List<string>();
                List<string> edgesToDelete = new List<string>();
                foreach (Claim claim in claims)
                {
                    foreach (ArangoClaims storedClaim in storedClaims)
                    {
                        if (claim.Issuer == storedClaim.Issuer &&
                            claim.Type == storedClaim.Type &&
                            claim.ValueType == storedClaim.ValueType &&
                            claim.Value == storedClaim.Value)
                        {
                            claimsToDelete.Add(storedClaim._key);
                            foreach (Edges item in edgeList)
                            {
                                if (item._to.Contains(storedClaim._key))
                                {
                                    edgesToDelete.Add(item._key);
                                }
                            }
                        }
                    }
                }

                DeleteDocumentsResponse<ArangoClaims> deleteClaimResponse = await db.Document.DeleteDocumentsAsync<ArangoClaims>(ArangoSchema.collUserClaims, claimsToDelete, token: cancellationToken);
                DeleteDocumentsResponse<Edges> deleteEdgeResponse = await db.Document.DeleteDocumentsAsync<Edges>(ArangoSchema.edgeUserClaims, edgesToDelete, token: cancellationToken);
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }

        }

        /// <summary>
        /// removes the given normalizedRoleName from the specified user
        /// </summary>
        /// <param name="user">The user to remove the role from</param>
        /// <param name="roleName">The role to remove</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RemoveFromRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder roleQb = new ArangoQueryBuilder(ArangoSchema.collRoles);
            roleQb.filter("Name", roleName);
            roleQb.limit(1);
            string roleQuery = roleQb.ToString();
            CursorResponse<Roles> roleResponse = await db.Cursor.PostCursorAsync<Roles>(roleQuery, token: cancellationToken);
            string roleKey = roleResponse.Result.First()._key;

            ArangoQueryBuilder edgeQb = new ArangoQueryBuilder(ArangoSchema.edgeRoleUsers);
            edgeQb.filter("_from", $"{ArangoSchema.collRoles}/{roleKey}");
            edgeQb.filter("_to", $"{ArangoSchema.collUsers}/{user._key}");
            string edgeQuery = edgeQb.ToString();
            List<string> edgesToDelete = new List<string>();
            CursorResponse<Edges> edgeResponse = await db.Cursor.PostCursorAsync<Edges>(edgeQuery, token: cancellationToken);
            if (edgeResponse.Result.Any())
            {
                foreach (Edges item in edgeResponse.Result)
                {
                    edgesToDelete.Add(item._key);
                }
                bool hasMore = edgeResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(edgeResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        edgesToDelete.Add(item._key);
                    }
                }

                DeleteDocumentsResponse<Edges> deleteEdgeResponse = await db.Document.DeleteDocumentsAsync<Edges>(ArangoSchema.edgeRoleUsers, edgesToDelete, token: cancellationToken);
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// removes the loginProvider given from the specified user
        /// </summary>
        /// <param name="user">The user to remove the login from</param>
        /// <param name="loginProvider">The login to remove from the user</param>
        /// <param name="providerKey">The key provided by the loginProvider to identify a user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RemoveLoginAsync(Users user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder edgeQb = new ArangoQueryBuilder(ArangoSchema.collUserLogins);
            edgeQb.filter("_from", $"{ArangoSchema.collUsers}/{user._key}");
            edgeQb.filter("Type", "Login");
            string edgeQuery = edgeQb.ToString();
            List<UserLogins> loginList = new List<UserLogins>();
            List<Edges> edgeList = new List<Edges>();
            CursorResponse<Edges> edgeResponse = await db.Cursor.PostCursorAsync<Edges>(edgeQuery, token: cancellationToken);
            if (edgeResponse.Result.Any())
            {
                foreach (Edges item in edgeResponse.Result)
                {
                    string[] edgeTo = item._to.Split('/');
                    UserLogins login = await db.Document.GetDocumentAsync<UserLogins>(ArangoSchema.collUserLogins, edgeTo[1]);
                    loginList.Add(login);
                    edgeList.Add(item);
                }
                bool hasMore = edgeResponse.HasMore;
                while (hasMore)
                {
                    PutCursorResponse<Edges> cursorResponse = await db.Cursor.PutCursorAsync<Edges>(edgeResponse.Id, token: cancellationToken);
                    hasMore = cursorResponse.HasMore;

                    foreach (Edges item in cursorResponse.Result)
                    {
                        string[] edgeTo = item._to.Split('/');
                        UserLogins login = await db.Document.GetDocumentAsync<UserLogins>(ArangoSchema.collUserLogins, edgeTo[1]);
                        loginList.Add(login);
                        edgeList.Add(item);
                    }
                }

                List<string> loginsToDelete = new List<string>();
                List<string> edgesToDelete = new List<string>();
                foreach (UserLogins login in loginList)
                {
                    if (login.LoginProvider == loginProvider &&
                        login.ProviderKey == providerKey)
                    {
                        loginsToDelete.Add(login._key);
                        foreach (Edges edge in edgeList)
                        {
                            if (edge._to == $"{ArangoSchema.collUserLogins}/{login._key}")
                            {
                                edgesToDelete.Add(edge._key);
                            }
                        }
                    }
                }

                DeleteDocumentsResponse<UserLogins> deleteLoginResponse = await db.Document.DeleteDocumentsAsync<UserLogins>(ArangoSchema.collUserLogins, loginsToDelete, token: cancellationToken);
                DeleteDocumentsResponse<Edges> deleteEdgeResponse = await db.Document.DeleteDocumentsAsync<Edges>(ArangoSchema.edgeUserLogins, edgesToDelete, token: cancellationToken);
            }
            else
            {
                throw new ArgumentNullException("queryResponse.Result");
            }
        }

        /// <summary>
        /// Replaces the claim on the specified user, with the newClaim
        /// </summary>
        /// <param name="user">The user to replace the claim on</param>
        /// <param name="claim">The claim replace</param>
        /// <param name="newClaim">The new claim replacing the claim</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ReplaceClaimAsync(Users user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            await CreateConnection();

            ArangoQueryBuilder claimQb = new ArangoQueryBuilder(ArangoSchema.collUserClaims);
            claimQb.filter("Issuer", claim.Issuer);
            claimQb.filter("Value", claim.Value);
            claimQb.filter("Type", claim.Type);
            claimQb.filter("ValueType", claim.ValueType);
            claimQb.limit(1);
            string claimQuery = claimQb.ToString();
            CursorResponse<ArangoClaims> claimResponse = await db.Cursor.PostCursorAsync<ArangoClaims>(claimQuery, token: cancellationToken);
            if (claimResponse.Result.Any())
            {
                ArangoClaims oldCLaim = claimResponse.Result.First();
                ArangoClaims replacementClaim = new ArangoClaims(newClaim);
                replacementClaim._key = oldCLaim._key;
                PutDocumentResponse<ArangoClaims> putDocumentResponse = await db.Document.PutDocumentAsync<ArangoClaims>(oldCLaim._key, replacementClaim, token: cancellationToken);
            }
            else
            {
                throw new ArgumentNullException("claimResponse.Result");
            }
        }

        /// <summary>
        /// reset a user's failed access count
        /// </summary>
        /// <param name="user">The user whose failed access count should be reset</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ResetAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount = 0;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the email address for a user
        /// </summary>
        /// <param name="user">The user whose email should be set</param>
        /// <param name="email">The email to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetEmailAsync(Users user, string? email, CancellationToken cancellationToken)
        {
            user.Email = email;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the flag indicating whether the specified user's email address has been confirmed or
        /// not
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be set</param>
        /// <param name="confirmed">A flag indicating if the email address has been confirmed, true if the address is confirmed otherwise false</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetEmailConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// set the flag indicating if the specified user can be locked out
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be set</param>
        /// <param name="enabled">A flag indicating if lock out can be enabled for the specified user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetLockoutEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// locks out a user until the specified end date has passed. Setting a end date in the past
        /// immediiately unlocks a user
        /// </summary>
        /// <param name="user">The user whose lockout date should be set</param>
        /// <param name="lockoutEnd">The DateTimeOffset after which the user's lockout should end</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetLockoutEndDateAsync(Users user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the normalized email for the specified user
        /// </summary>
        /// <param name="user">The user whose email address to set</param>
        /// <param name="normalizedEmail">The normalized email to set for the specified user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetNormalizedEmailAsync(Users user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the given normalized name for the specified user
        /// </summary>
        /// <param name="user">The user whose name should be set</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetNormalizedUserNameAsync(Users user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// Sets the password hash for a user
        /// </summary>
        /// <param name="user">The user to set the password hash for</param>
        /// <param name="passwordHash">The password hash to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetPasswordHashAsync(Users user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the telephone number for the specified user
        /// </summary>
        /// <param name="user">The user whose telephone number should be set</param>
        /// <param name="phoneNumber">The telephone number to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetPhoneNumberAsync(Users user, string? phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets a flag indicating if the specified user's phone number has been confirmed
        /// </summary>
        /// <param name="user">The user whose telephone number confirmation status should be set</param>
        /// <param name="confirmed">A flag indicating whether the user's telephone number has been confirmed</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetPhoneNumberConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the provided security stamp for the specified user
        /// </summary>
        /// <param name="user">The user whose security stamp should be set</param>
        /// <param name="stamp">The security stamp to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetSecurityStampAsync(Users user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets a flag indicating whether the specified user has two factor authentication enabled or
        /// not, as an asynchronous operation
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set</param>
        /// <param name="enabled">A flag indicating whether the specified user has two factor authentication enabled</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetTwoFactorEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// sets the given userName for the specified user
        /// </summary>
        /// <param name="user">The user whose name should be set</param>
        /// <param name="userName">The user name to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SetUserNameAsync(Users user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            await UpdateAsync(user, cancellationToken);
        }

        /// <summary>
        /// updates the specified user in the user store
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            await CreateConnection();

            // TODO obtain any Users that match the username of the provided user and update that record instead
            // TODO no need for post method as record will already have been created

            if (user._key is null)
            {
                PostDocumentResponse<Users> postResponse = await db.Document.PostDocumentAsync<Users>(ArangoSchema.collUsers, user, token: cancellationToken);
            }
            else
            {
                PatchDocumentQuery patchQuery = new PatchDocumentQuery();
                patchQuery.KeepNull = true;

                PatchDocumentResponse<object> patchResponse = await db.Document.PatchDocumentAsync<Users>($"{ArangoSchema.collUsers}/{user._key}", user, patchQuery, cancellationToken);
            }
            return IdentityResult.Success;
        }
    }
}
