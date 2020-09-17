using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Resources;

namespace RestApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment web;
        private readonly IMapper map;
        private readonly JwtProperty jwtProp;
        public AccountController (

            UserManager<ApplicationUser> _userManager,
            RoleManager<ApplicationRole> _roleManager,
            SignInManager<ApplicationUser> _signInManager,
            IWebHostEnvironment _web,
            IMapper _map,
            IOptions<JwtProperty> _jwtProp
        ) {
            this._signInManager = _signInManager;
            web = _web;
            map = _map;
            jwtProp = _jwtProp.Value;
            this._userManager = _userManager;
            this._roleManager = _roleManager;
        }

        [HttpPost ("Login")]
        public async Task<IActionResult> EmployeeLogin ([FromBody] UserResource param) {

            if (param != null) {
                var user = await _userManager.FindByNameAsync (param.UserName);

                if (user == null) {
                    return NotFound ();
                }

                if (!user.isActive) {
                    return Ok (new { message = "user is not active", body = "", code = 203 });

                }
                var result = await _signInManager.PasswordSignInAsync (user, param.password, false, false);
                if (result.Succeeded) {
                    var token = await GenerateTokenAsync (user);

                    var resource = map.Map<UserResource> (user);
                    var Role = await _userManager.GetRolesAsync (user);
                    resource.Role = map.Map<RoleResource> (await _roleManager.FindByNameAsync (Role.FirstOrDefault ()));
                    resource.Token = token;
                    var response = new { message = "User Login Successfully", body = resource, token = token, code = 200 };
                    return Ok (response);
                }

                return NotFound ();

            }

            return BadRequest ();
        }

        [HttpPost ("Add")]
        public IActionResult Register ([FromBody] UserResource model) {

            // var user = Request.Form.ContainsKey ("user");

            var appUser = map.Map<UserResource, ApplicationUser> (model);

            var result = _userManager.CreateAsync (appUser, appUser.password).GetAwaiter ().GetResult ();
            if (result.Succeeded) {
                _userManager.AddToRoleAsync (appUser, model.Role.Name).GetAwaiter ().GetResult ();
                return Ok (appUser);
            }

            return BadRequest ();
        }

        [HttpPost ("Edit")]
        public IActionResult UpdateUser ([FromBody] UserResource model) {

            var appUser = _userManager.FindByIdAsync (model.Id.ToString ()).GetAwaiter ().GetResult ();
            //  model.isActive = appUser.isActive== model.isActive? appUser.isActive:model.isActive;
            //  model.activeDate =  DateTime.Now;;

            map.Map<UserResource, ApplicationUser> (model, appUser);
            var result = _userManager.UpdateAsync (appUser).GetAwaiter ().GetResult ();
            if (result.Succeeded) {

                // _userManager.AddToRoleAsync (appUser, "Admin");
                return Ok (appUser);
            }
            return BadRequest ();

        }

        [HttpPost ("password/change")]
        public IActionResult ChangePassword (PasswordResetResource password) {

            var user = _userManager.FindByNameAsync (password.UserName)
                .GetAwaiter ().GetResult ();
            if (user != null) {

                var isCorrectPassword = _userManager.CheckPasswordAsync (user, password.password)
                    .GetAwaiter ().GetResult ();

                if (!isCorrectPassword) {
                    return Ok (new { message = "Old password is not valid", code = 205 });
                }

                var result = _userManager.ChangePasswordAsync (user, password.password, password.newPassword)
                    .GetAwaiter ().GetResult ();

                if (result.Succeeded) {
                    user.password = password.newPassword;
                    _userManager.UpdateAsync (user);
                    return Ok (new { message = "password changed successfully", code = 200 });

                }
            }
            return BadRequest ();
        }

        [HttpDelete ("user/delete/{id}")]
        public async Task<IActionResult> DeleteUser (int id) {

            var user = await _userManager.FindByIdAsync (id.ToString ());
            if (user != null) {

                var result = await _userManager.DeleteAsync (user);
                if (result.Succeeded) {
                    return Ok ();
                }
                return NotFound ();
            }
            return BadRequest ();
        }

        [HttpPost ("Role/User")]
        public IActionResult AddRoleToUser (UserResource user) {

            var appUser = _userManager.FindByNameAsync (user.UserName).GetAwaiter ().GetResult ();
            if (appUser != null) {
                if (user.Role != null) {

                    var result = _userManager.AddToRoleAsync (appUser, user.Role.Name).GetAwaiter ().GetResult ();
                    if (result.Succeeded) {
                        return Ok ();
                    } else {
                        return BadRequest ();
                    }
                }
            }

            return NotFound ();
        }

        [HttpGet ("Roles")]
        public IActionResult GetRoles () {

            var roles = map.Map<List<RoleResource>> (_roleManager.Roles.ToList ());
            return Ok (roles);
        }

        [HttpGet ("Users")]
        [Authorize (Roles = "Admin")]
        public IActionResult GetUsersList () {

            var users = map.Map<List<UserResource>> (_userManager.Users.ToList ())
                .Select (p => new UserResource () {
                    UserName = p.UserName,
                        Address = p.Address,
                        Email = p.Email,
                        password = p.password,
                        firstName = p.firstName,
                        lastName = p.lastName,
                        fullName = p.fullName,
                        Gender = p.Gender,
                        isActive = p.isActive,
                        activeDate = p.activeDate,
                        Id = p.Id,
                        RoleName = _userManager.GetRolesAsync (_userManager
                            .FindByIdAsync (p.Id.ToString ()).Result).Result.FirstOrDefault ()
                });
            return Ok (users);
        }

        [HttpGet ("Users/ById/{id}")]
        public async Task<IActionResult> UserById (int id) {

            var user = await _userManager.FindByIdAsync (id.ToString ());
            if (user == null) {
                return NotFound ();
            }
            var userRes = map.Map<ApplicationUser, UserResource> (user);
            return Ok (userRes);
        }

        public async Task<IActionResult> GetRoleUsers (int Id) {
            var role = await _roleManager.FindByIdAsync (Id.ToString ());
            var users = map.Map<List<UserResource>> (_userManager.Users.ToList ());
            if (role != null) {

            }
            return NotFound ();
        }

        [HttpGet ("Logout")]
        public IActionResult Logout () {

            _signInManager.SignOutAsync ().GetAwaiter ().GetResult ();
            return Ok ();
        }

        async Task<string> GenerateTokenAsync (ApplicationUser user) {
            var token = string.Empty;
            var roles = await _userManager.GetRolesAsync (user);

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (jwtProp.Key));
            var credential = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim (JwtRegisteredClaimNames.Jti, user.UserName)
            };

            roles.ToList ().ForEach (p => {

                claims.Add (new Claim (ClaimTypes.Role, p));
            });

            var securityKey = new JwtSecurityToken (
                issuer: jwtProp.Issuer,
                audience: jwtProp.Audience,
                signingCredentials: credential,
                expires: DateTime.Now.AddMinutes (jwtProp.Expirey),
                claims: claims

            );

            token = new JwtSecurityTokenHandler ().WriteToken (securityKey);
            return await Task.Run (() => token);
        }

    }
}