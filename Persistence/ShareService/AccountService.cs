using Application.DTOs;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Persistence.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.ShareService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
