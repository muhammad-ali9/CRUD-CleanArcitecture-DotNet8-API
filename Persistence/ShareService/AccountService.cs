using Application.DTOs;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Persistence.IdentityModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.ShareService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<ApiResponse<AuthenticationResponse>> UserLogin(LoginModel loginModel)
        {
            var userAuth = await _userManager.FindByEmailAsync(loginModel.Email);
            if(userAuth == null)
            {
                throw new ApiException($"User is nbot registered with this email {loginModel.Email}");
            }

            var checkpassword = await _userManager.CheckPasswordAsync(userAuth,loginModel.Password);
            if (!checkpassword)
            {
                throw new ApiException($"Password is Incorrect");
            }
            var JwtSecurity = await GenerateToken(userAuth);

            var authenticatedUser = new AuthenticationResponse();

            authenticatedUser.Id = userAuth.Id;
            authenticatedUser.Email = userAuth.Email;
            authenticatedUser.UserName = userAuth.UserName;
            authenticatedUser.isVerified = userAuth.EmailConfirmed;

            var roles = await _userManager.GetRolesAsync(userAuth);
            authenticatedUser.Role = roles.ToList();

            

            authenticatedUser.JwtToken = new JwtSecurityTokenHandler().WriteToken(JwtSecurity);

            return new ApiResponse<AuthenticationResponse>(authenticatedUser);

               
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var dbClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = "1.0.98.168";

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub,  user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString()),
            new Claim("ip", ipAddress)
            }.Union(dbClaims).Union(roleClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
               audience: _config["Jwt:audience"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(120),
             signingCredentials: credentials);

            return token;

        }

        public async Task<ApiResponse<Guid>> Registration(RegistrationModel registrationModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registrationModel.Email);
            if (userExists == null) 
            {
                var userModel = new ApplicationUser();

                userModel.Email = registrationModel.Email;
                userModel.FirstName = registrationModel.FirstName;
                userModel.LastName = registrationModel.LastName;
                userModel.Gender = registrationModel.Gender;
                userModel.UserName = registrationModel.UserName;
                userModel.EmailConfirmed = true;
                userModel.PhoneNumberConfirmed = true;

                var result = await _userManager.CreateAsync(userModel, registrationModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userModel, Roles.Basic.ToString());
                    return new ApiResponse<Guid>(userModel.Id, "User Created Successfully.");
                }
                else
                {
                    throw new ApiException(result.Errors.ToString());
                }

            }

            throw new ApiException($"User already Exist {registrationModel.Email}");
        }
    }
}
