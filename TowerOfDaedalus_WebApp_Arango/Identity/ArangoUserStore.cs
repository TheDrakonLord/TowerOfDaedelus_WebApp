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
    public class ArangoUserStore : IUserStore<Users>, IUserRoleStore<Users>, IUserClaimStore<Users>, IUserPasswordStore<Users>,
        IUserSecurityStampStore<Users>, IUserEmailStore<Users>, IUserPhoneNumberStore<Users>, IQueryableUserStore<Users>, IUserLoginStore<Users>,
        IUserTwoFactorStore<Users>, IUserLockoutStore<Users>
    {
        public IQueryable<Users> Users => throw new NotImplementedException();

        /// <summary>
        /// Adds the claims given to the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claims"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the login given to the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddLoginAsync(Users user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the given normalizedRolename to the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddToRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified user in the user store
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified user from the user store
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// <param name="normalizedEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the user associated with the specified login provider and login provider key
        /// </summary>
        /// <param name="loginProvider"></param>
        /// <param name="providerKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name
        /// </summary>
        /// <param name="normalizedUserName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Users?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the current failed access count for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<int> GetAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get the claims associated with the specified user as an asynchronous operation
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Claim>> GetClaimsAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the email address for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetEmailAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified user has been verified.
        /// true if the email address is verified otherwise false
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetEmailConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a flag indicating whether user lockout can be enabled for a specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetLockoutEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the last DateTimeOffset a user's last lockout expired, if any. Any time in the past should
        /// indicate a user is not locked out
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves the associated logins for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns the normalized email for the specicfied user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetNormalizedEmailAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the normalized username for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the telephone number, if any, for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetPhoneNumberAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets a flag indicating whether the specified user's telephone number has been confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetPhoneNumberConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves the soles the specified user is a member of
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<string>> GetRolesAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get the security stamp for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetSecurityStampAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a flag indicating whether the specified user has two factor authentication enabled
        /// or not, as an asynchronous operation
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> GetTwoFactorEnabledAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the user identifier for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the user name for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string?> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves all users with the specified claim
        /// </summary>
        /// <param name="claim"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Users>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// retrieves al users in the specified role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Users>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a flag indicating if the specified user has a password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// records that a failed access has occurred, incrementing the failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<int> IncrementAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a flag indicating if the specified user is a member of the given normalizedRoleName
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> IsInRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes the claims given from the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claims"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RemoveClaimsAsync(Users user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes the given normalizedRoleName from the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RemoveFromRoleAsync(Users user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes the loginProvider given from the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginProvider"></param>
        /// <param name="providerKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RemoveLoginAsync(Users user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replaces the claim on the specified user, with the newClaim
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <param name="newClaim"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ReplaceClaimAsync(Users user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// reset a user's failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ResetAccessFailedCountAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the email address for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetEmailAsync(Users user, string? email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the flag indicating whether the specified user's email address has been confirmed or
        /// not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetEmailConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// set the flag indicating if the specified user can be locked out
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetLockoutEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// locks out a user until the specified end date has passed. Setting a end date in the past
        /// immediiately unlocks a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetLockoutEndDateAsync(Users user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the normalized email for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="normalizedEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetNormalizedEmailAsync(Users user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the given normalized name for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetNormalizedUserNameAsync(Users user, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the password hash for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetPasswordHashAsync(Users user, string? passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the telephone number for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetPhoneNumberAsync(Users user, string? phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets a flag indicating if the specified user's phone number has been confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetPhoneNumberConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the provided security stamp for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetSecurityStampAsync(Users user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets a flag indicating whether the specified user has two factor authentication enabled or
        /// not, as an asynchronous operation
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetTwoFactorEnabledAsync(Users user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// sets the given userName for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SetUserNameAsync(Users user, string? userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// updates the specified user in the user store
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
