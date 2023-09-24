using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using Palmfit.Core.Helpers;

namespace Palmfit.Core.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly PalmfitDbContext _palmfitDb;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUserRole> _roleManager;

        public AuthRepository(IConfiguration configuration, RoleManager<AppUserRole> roleManager, PalmfitDbContext palmfitDb, UserManager<AppUser> userManager)  
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _palmfitDb = palmfitDb;
        }


        public async Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public string GenerateJwtToken(AppUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Set a default expiration in minutes if "AccessTokenExpiration" is missing or not a valid numeric value.
            if (!double.TryParse(jwtSettings["AccessTokenExpiration"], out double accessTokenExpirationMinutes))
            {
                accessTokenExpirationMinutes = 30; // Default expiration of 30 minutes.
            }

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string SendOTPByEmail(string email)
        {
            try
            {
                //generating otp
                var generateRan = new RandomNumberGenerator();
                var otp = generateRan.GenerateOTP().ToString();


                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(EmailSettings.SmtpUsername);
                    mailMessage.To.Add(email);
                    mailMessage.Subject = "One Time Password(OTP)";
                    mailMessage.Body = $"Your OTP:{otp}";

                    using (SmtpClient smtpClient = new SmtpClient(EmailSettings.SmtpServer, EmailSettings.SmtpPort))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(EmailSettings.SmtpUsername, EmailSettings.SmtpPassword);
                        smtpClient.Send(mailMessage);
                    }
                }

                // saving otp
                var userOTP = new UserOTP
                {
                    Email = email,
                    OTP = otp,
                    Expiration = DateTime.UtcNow.AddMinutes(10) // set an expiration time for OTP (e.g 5 minutes)
                };
                _palmfitDb.UserOTPs.Add(userOTP);
                _palmfitDb.SaveChanges();

                return $"OTP sent to {email}";
            }
            catch (Exception ex)
            {
                return $"Faild To Send OTP to {email}, Error, {ex.Message}";
            }
        }


        public async Task<UserOTP?> FindMatchingValidOTP(string otpFromUser)
        {
            var currentTime = DateTime.UtcNow;
            var expirationTime = TimeSpan.FromMinutes(5);

            var result = await _palmfitDb.UserOTPs.FirstOrDefaultAsync(otp => otp.OTP == otpFromUser && (currentTime - otp.Expiration) <= expirationTime);

            return result;
        }


        public async Task<IdentityResult> CreatePermissionAsync(string name)
        {
            var permission = new AppUserPermission { Name = name };
            _palmfitDb.AppUserPermissions.Add(permission);

            try
            {
                await _palmfitDb.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur while saving to the database
                return IdentityResult.Failed(new IdentityError { Description = $"Failed to create permission: {ex.Message}" });
            }
        }


        public async Task<ApiResponse<string>> UpdateVerifiedStatus(string email)
        {
            var verifiedUser = await _userManager.FindByEmailAsync(email);

            if (verifiedUser == null)
            {
                return new ApiResponse<string>($"{email} Not Found!");
            }
            verifiedUser.IsVerified = true;

            // Save changes to the database
            var identityResult = await _userManager.UpdateAsync(verifiedUser);
            if (!identityResult.Succeeded)
            {
                return new ApiResponse<string>("Verification Failed.");
            }
            return new ApiResponse<string>("Verified successfully.");
        }


        public async Task<IEnumerable<AppUserPermission>> GetAllPermissionsAsync() 
        { 
            return await _palmfitDb.AppUserPermissions.ToListAsync(); 
        }



        public async Task<IEnumerable<AppUserPermission>> GetPermissionsByRoleNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return Enumerable.Empty<AppUserPermission>();
            }
            else
            {
                // Get the claims associated with the role
                var claims = await _roleManager.GetClaimsAsync(role);
                var permissionNames = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();

                // Find the permissions with matching names
                var permissions = _palmfitDb.AppUserPermissions.Where(p => permissionNames.Contains(p.Name));
                return permissions;
            }
        }




        public async Task AssignPermissionToRoleAsync(string roleName, string permissionName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new InvalidOperationException("Role not found.");
            }
            else
            {
                var permission = await _palmfitDb.AppUserPermissions.FirstOrDefaultAsync(p => p.Name == permissionName);
                if (permission == null)
                {
                    throw new InvalidOperationException("Permission not found.");
                }
                else
                {
                    // Add the new IdentityRoleClaim
                    var claim = new Claim("Permission", permission.Name);
                    var result = await _roleManager.AddClaimAsync(role, claim);

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException("Failed to add permission claim to role.");
                    }
                }
            }
            
        }





        public async Task<IdentityResult> RemovePermissionFromRoleAsync(string roleId, string permissionId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }

            var permission = await _palmfitDb.AppUserPermissions.FindAsync(permissionId);
            if (permission == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Permission not found." });
            }

            // Get the claim associated with the permission and the role
            var claim = (await _roleManager.GetClaimsAsync(role)).FirstOrDefault(c => c.Type == "Permission" && c.Value == permission.Name);
            if (claim != null)
            {
                // Remove the claim from the role
                var result = await _roleManager.RemoveClaimAsync(role, claim);
                if (result.Succeeded)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    // Handle the case where removing the claim fails
                    return IdentityResult.Failed(new IdentityError { Description = "Failed to remove permission from role." });
                }
            }
            return IdentityResult.Failed(new IdentityError { Description = "Permission not assigned to role." });
        }

       
        public async Task<string> IsEmailVerifiedAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var isUserVerified = _palmfitDb.Users.Any(u => u.IsVerified == true);

            string message = (user == null)
                ? "User does not exist"
                : (isUserVerified)
                    ? "User is verified"
                    : "The user has not been verified!";
            return message;
        }



    }
}
