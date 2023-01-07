using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;

namespace TowerOfDaedalus_WebApp_Arango.Identity
{
    /// <summary>
    /// custom User Store class that changes the data provider for the Microsoft Identity Framework from SQL to Arango
    /// </summary>
    public class ArangoUserStore : IUserStore<Users>, IUserRoleStore<Users>, IUserClaimStore<Users>, IUserPasswordStore<Users>,
        IUserSecurityStampStore<Users>, IUserEmailStore<Users>, IUserPhoneNumberStore<Users>, IQueryableUserStore<Users>, IUserLoginStore<Users>,
        IUserTwoFactorStore<Users>, IUserLockoutStore<Users>
    {
        /// <summary>
        /// A navigation property for the users the store contains
        /// </summary>
        public IQueryable<Users> Users => throw new NotImplementedException();

        /// <summary>
        /// Adds the claims given to the specified user
        /// </summary>
        /// <param name="user">The user to add the claim to</param>
        /// <param name="claims">The claim to add to the user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the login given to the specified user
        /// </summary>
        /// <param name="user">The user to add the login to</param>
        /// <param name="login">The login to add to the user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddLoginAsync(Users user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the given normalizedRolename to the specified user
        /// </summary>
        /// <param name="user">The user to add the role to</param>
        /// <param name="roleName">The role to add</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddToRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified user in the user store
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified user from the user store
        /// </summary>
        /// <param name="user">The user to delete</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose the store
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user, if any, associated with the specified, normalized email address
        /// </summary>
        /// <param name="normalizedEmail">The normalized email address to return the user for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId
        /// </summary>
        /// <param name="userId">The user ID to search for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the user associated with the specified login provider and login provider key
        /// </summary>
        /// <param name="loginProvider">The login provider who provided the providerKey</param>
        /// <param name="providerKey">The key provided by the loginProvider to identify a user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name
        /// </summary>
        /// <param name="normalizedUserName">The normalized user name to search for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the current failed access count for the specified user
        /// </summary>
        /// <param name="user">The user whose failed access count should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<int> GetAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get the claims associated with the specified user as an asynchronous operation
        /// </summary>
        /// <param name="user">The user whose claims should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Claim>> GetClaimsAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the email address for the specified user
        /// </summary>
        /// <param name="user">The user whose email should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetEmailAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified user has been verified.
        /// true if the email address is verified otherwise false
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetEmailConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a flag indicating whether user lockout can be enabled for a specified user
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be returned</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetLockoutEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the last DateTimeOffset a user's last lockout expired, if any. Any time in the past should
        /// indicate a user is not locked out
        /// </summary>
        /// <param name="user">The user whose lockout date should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves the associated logins for the specified user
        /// </summary>
        /// <param name="user">The user whose associated logins to retrieve</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns the normalized email for the specicfied user
        /// </summary>
        /// <param name="user">The user whose email address to retrieve</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetNormalizedEmailAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the normalized username for the specified user
        /// </summary>
        /// <param name="user">The user whose normalized name should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the password hash for a user
        /// </summary>
        /// <param name="user">The user to retrieve the password hash for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the telephone number, if any, for the specified user
        /// </summary>
        /// <param name="user">The user whose telephone number should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetPhoneNumberAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets a flag indicating whether the specified user's telephone number has been confirmed
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether their telephone number is confirmed</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetPhoneNumberConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves the soles the specified user is a member of
        /// </summary>
        /// <param name="user">The user whose roles should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<string>> GetRolesAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get the security stamp for the specified user
        /// </summary>
        /// <param name="user">The user whose security stamp should be set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetSecurityStampAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a flag indicating whether the specified user has two factor authentication enabled
        /// or not, as an asynchronous operation
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetTwoFactorEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the user identifier for the specified user
        /// </summary>
        /// <param name="user">The user whose identifier should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the user name for the specified user
        /// </summary>
        /// <param name="user">The user whose name should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves all users with the specified claim
        /// </summary>
        /// <param name="claim">The claim whose users should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Users>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves al users in the specified role
        /// </summary>
        /// <param name="roleName">The role whose users should be retrieved</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Users>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a flag indicating if the specified user has a password
        /// </summary>
        /// <param name="user">The user to retrieve the password hash for</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// records that a failed access has occurred, incrementing the failed access count
        /// </summary>
        /// <param name="user">The user whose cancellation count should be incremented</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<int> IncrementAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a flag indicating if the specified user is a member of the given normalizedRoleName
        /// </summary>
        /// <param name="user">The user whose role membership should be checked</param>
        /// <param name="roleName">The role to check membership of</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> IsInRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes the claims given from the specified user
        /// </summary>
        /// <param name="user">The user to remove the claims from</param>
        /// <param name="claims">The claim to remove</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RemoveClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes the given normalizedRoleName from the specified user
        /// </summary>
        /// <param name="user">The user to remove the role from</param>
        /// <param name="roleName">The role to remove</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RemoveFromRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
        public Task RemoveLoginAsync(Users user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
        public Task ReplaceClaimAsync(Users user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// reset a user's failed access count
        /// </summary>
        /// <param name="user">The user whose failed access count should be reset</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ResetAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the email address for a user
        /// </summary>
        /// <param name="user">The user whose email should be set</param>
        /// <param name="email">The email to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetEmailAsync(Users user, string? email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
        public Task SetEmailConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// set the flag indicating if the specified user can be locked out
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be set</param>
        /// <param name="enabled">A flag indicating if lock out can be enabled for the specified user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetLockoutEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
        public Task SetLockoutEndDateAsync(Users user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the normalized email for the specified user
        /// </summary>
        /// <param name="user">The user whose email address to set</param>
        /// <param name="normalizedEmail">The normalized email to set for the specified user</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetNormalizedEmailAsync(Users user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the given normalized name for the specified user
        /// </summary>
        /// <param name="user">The user whose name should be set</param>
        /// <param name="normalizedName">The normalized name to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetNormalizedUserNameAsync(Users user, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the password hash for a user
        /// </summary>
        /// <param name="user">The user to set the password hash for</param>
        /// <param name="passwordHash">The password hash to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetPasswordHashAsync(Users user, string? passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the telephone number for the specified user
        /// </summary>
        /// <param name="user">The user whose telephone number should be set</param>
        /// <param name="phoneNumber">The telephone number to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetPhoneNumberAsync(Users user, string? phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets a flag indicating if the specified user's phone number has been confirmed
        /// </summary>
        /// <param name="user">The user whose telephone number confirmation status should be set</param>
        /// <param name="confirmed">A flag indicating whether the user's telephone number has been confirmed</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetPhoneNumberConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the provided security stamp for the specified user
        /// </summary>
        /// <param name="user">The user whose security stamp should be set</param>
        /// <param name="stamp">The security stamp to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetSecurityStampAsync(Users user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
        public Task SetTwoFactorEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the given userName for the specified user
        /// </summary>
        /// <param name="user">The user whose name should be set</param>
        /// <param name="userName">The user name to set</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetUserNameAsync(Users user, string? userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// updates the specified user in the user store
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <param name="cancellationToken">The CancellationToken used to propagate notifications that the operation should be canceled</param>
        /// <returns>The Task that represents the asynchronous operation</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
