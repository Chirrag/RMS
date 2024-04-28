using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QScores.Domain.QScoresAppsModels;
using QScores.Domain.QScoresAppsModels.ViewModels;
using QScores.Infrastructure.Context;

namespace QScoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QscoresAppsContext _qscoresAppsDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(QscoresAppsContext qscoresAppsDbContext, UserManager<IdentityUser> userManager)
        {
            _qscoresAppsDbContext = qscoresAppsDbContext;
            _userManager = userManager;
        }

        [Route("GetUserInfo")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            if (User?.Identity?.Name == null)
            {
                return BadRequest(new { message = "User identity not found!" });
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound(new { message = "User not found!" });
            }
            var userInfo = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email);

            return Ok(userInfo);
        }


        [Route("SetUserImage")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetUserImage([FromBody] BinaryImage data)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound(new { message = "User not found!" });
                }

                var userInfo = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email);

                if (userInfo != null && data?.binaryData != null && data.binaryData.Length > 0)
                {
                    userInfo.Picture = data.binaryData;
                    await _qscoresAppsDbContext.SaveChangesAsync();
                }
                else
                {
                    return BadRequest(new { message = "No image file provided." });
                }

                return Ok(new { message = "Image uploaded successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [Route("EditProfile")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile([FromBody] TblAuthentication userDetails)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound(new { message = "User not found!" });
                }

                var userToBeEdited = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email);

                if (userToBeEdited != null && userToBeEdited.FullName == userDetails.FullName && userToBeEdited.OfficePhone == userDetails.OfficePhone && userToBeEdited.CellPhone == userDetails.CellPhone)
                {
                    return BadRequest(new { message = "No changes are done by the user. Please do the changes first." });
                }

                if (userDetails.FullName == null)
                {
                    return BadRequest(new { message = "FullName cannot be empty!" });
                }

                if (userToBeEdited != null && userDetails != null)
                {
                    userToBeEdited.FullName = userDetails.FullName;
                    userToBeEdited.OfficePhone = !string.IsNullOrEmpty(userDetails.OfficePhone) ? userDetails.OfficePhone : null;
                    userToBeEdited.CellPhone = !string.IsNullOrEmpty(userDetails.CellPhone) ? userDetails.CellPhone : null;
                    user.PhoneNumber = userDetails.CellPhone;
                    await _userManager.UpdateAsync(user);

                    _qscoresAppsDbContext.TblAuthentications.Update(userToBeEdited);
                    await _qscoresAppsDbContext.SaveChangesAsync();
                }

                else
                {
                    return BadRequest(new { message = "User does not exist in the records." });
                }

                return Ok(new { message = "Profile updated successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [Route("ChangePassword")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeModel pass)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (pass == null || pass.NewPassword == null)
                {
                    return BadRequest(new { message = "New password cannot be null." });
                }

                if (pass.NewPassword.Length < 3)
                {
                    return BadRequest(new { message = "Password is too short, should consist of at least 3 characters." });
                }

                if (user != null && await _userManager.CheckPasswordAsync(user, pass.OldPassword))
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, pass.NewPassword);
                    user.PasswordHash = newPasswordHash;
                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        return BadRequest(new { errors = result.Errors });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Wrong Old Password" });
                }

                return Ok(new { message = "Password changed successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
