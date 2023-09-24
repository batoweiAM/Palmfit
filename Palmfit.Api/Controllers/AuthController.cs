using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppUserRole> _roleManager;
        private readonly IEmailServices _emailServices;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IAuthRepository authRepo,  IEmailServices emailServices,RoleManager<AppUserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authRepo = authRepo;
            _roleManager = roleManager;
            _emailServices = emailServices;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ApiResponse.Failed("Invalid request. Please provide a valid email and password."));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                {
                    return NotFound(ApiResponse.Failed("User not found. Please check your email and try again."));
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Unauthorized(ApiResponse.Failed("Invalid credentials. Please check your email or password and try again."));
                }
                else
                {
                    var token = _authRepo.GenerateJwtToken(user);

                    // Returning the token in the response headers
                    Response.Headers.Add("Authorization", "Bearer " + token);

                    return Ok(ApiResponse.Success("Login successful."));

                }
            }
        }





        // Endpoint to create a new role
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(AppUserRole role)
        {

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string>("Role created successfully."));
            }
            else
            {
                return BadRequest(new ApiResponse<string>("Bad Request. A server error occured"));
            }
        }



        // Endpoint to update an existing role
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(AppUserRole role)
        {
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok(ApiResponse.Success("Role updated successfully."));
            }
            else
            {
                return BadRequest(new ApiResponse<string>("Bad Request. A server error occured"));
            }
        }



        // Endpoint to delete a role by role ID
        [HttpDelete("delete-role/{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok(ApiResponse.Success("Role deleted successfully."));
                }
                else
                {

                    return BadRequest(new ApiResponse<string>("Oops.Something went wrong"));
                }
            }
            else
            {

                return NotFound(new ApiResponse<string>("Role does not exist!"));
            }
        }




        [HttpPost("createPermission")]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionDto permissionDto)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new ApiResponse<string>("Invalid permission name format."));
            }

            var result = await _authRepo.CreatePermissionAsync(permissionDto.Name);

            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string>("Permission created successfully."));
            }
            else
            {
                // Handle the case where creating the permission fails
                return BadRequest(new ApiResponse<string>("Failed to create permission."));
            }
        }

        //Endpoint to create password reset
        [HttpPost("forget-password-reset")]
        public async Task<ActionResult<ApiResponse>> SendPasswordResetEmail(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return ApiResponse.Failed("Invalid email address.");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetUrl = "https://your-app.com/reset-password?token=" + token;
                var emailBody = $"Click the link below to reset your password: {passwordResetUrl}";
                await _emailServices.SendEmailAsync(loginDto.Email, "password Reset", emailBody);
                return ApiResponse.Success("password reset email sent successfully.");

            }
            catch (Exception ex)
            {
                return ApiResponse.Failed(null, "An error occurred during password reset.", new List<string> { ex.Message });
            }
        }


        [HttpPost("password-reset")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPassDTO reset)
        {
            var update = await _authRepo.UpdatePasswordAsync(reset.Email, reset.OldPassword, reset.NewPassword);
            if (!update)
            {
                return BadRequest(ApiResponse<string>.Failed(null, "Incorrect Credentials, try again"));
            }
            return Ok(ApiResponse<string>.Success(null, "Password Reset Successful"));
        }


        [HttpPost("Validate-OTP")]
        public async Task<IActionResult> ValidateOTP([FromBody] OtpDto otpFromUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.Failed(null, "Invalid OTP, check and try again"));
            }
            var userOTP = await _authRepo.FindMatchingValidOTP(otpFromUser.Otp);
            if (userOTP == null)
            {
                return BadRequest(ApiResponse<string>.Failed(null, "Invalid OTP, check and try again"));
            }

            await _authRepo.UpdateVerifiedStatus(userOTP.Email);
            return Ok(ApiResponse<string>.Success(null, "Validation Successfully."));
        }



        // Endpoint to get all permissions
        [HttpGet("get-all-permissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _authRepo.GetAllPermissionsAsync();
            return Ok(ApiResponse.Success(permissions));
        }

        [HttpPost("sendotp")]
        public async Task<IActionResult> SendOTP([FromBody] EmailDto emailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.Failed(null, "Invalid email format"));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(emailDto.Email);
                if (user == null)
                {
                    return NotFound(ApiResponse<string>.Failed(null, "User not Found"));
                }
                else
                {
                    var feedBack = _authRepo.SendOTPByEmail(emailDto.Email);
                    return Ok(ApiResponse<string>.Success(feedBack));
                }
            }
        }

        // Endpoint to get permissions by role ID
        [HttpGet("get-permissions-by-role/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId(string roleId)
        {
            var permissions = await _authRepo.GetPermissionsByRoleNameAsync(roleId);
            return Ok(ApiResponse.Success(permissions));
        }


       

        [HttpPost("Sign-Out")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(ApiResponse.Success("Sign out successful"));
        }




        // Endpoint to assign a permission to a role
        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermissionToRole(string roleName, string permissionName)
        {
            try
            {
                await _authRepo.AssignPermissionToRoleAsync(roleName, permissionName);
                return Ok(ApiResponse.Success("Permission assigned to role successfully."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse.Failed(null, "Permission assignment failed.", new List<string> { ex.Message }));
            }
        }




        // Endpoint to remove a permission from a role
        [HttpDelete("remove-permission")]
        public async Task<IActionResult> RemovePermissionFromRole(string roleId, string permissionId)
        {

            var result = await _authRepo.RemovePermissionFromRoleAsync(roleId, permissionId);
            if (result.Succeeded)
            {
                return Ok(ApiResponse.Success("Permission removed from role successfully."));
            }
            else
            {
                return BadRequest(ApiResponse.Failed(null, "Permission removal failed."));
            }
        }



        //api-to-get-email-verification-status
        [HttpGet("email-verification-status/{userId}")]
        public async Task<IActionResult> IsEmailVerified(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("userId", "User ID is required.");
                return BadRequest(ApiResponse.Failed(null, "Invalid request."));
            }

            try
            {
                var isEmailVerified = await _authRepo.IsEmailVerifiedAsync(userId);
                return Ok(ApiResponse.Success(isEmailVerified));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Failed(null, "An error occurred while checking email verification status.", new List<string> { ex.Message }));
            }
        }
    }
}



