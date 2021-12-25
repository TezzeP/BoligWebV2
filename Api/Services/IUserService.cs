using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Api.Services
{
    public interface IUserService
    {
        Task<UserManagerRespose> RegisterUserAsync(RegisterViewModel model);


    }
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        public UserService (UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserManagerRespose> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register Model is Null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerRespose
                {
                    Message = "Confirm password does not mach the password",
                    IsSucccess = false,
                };
            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                return new UserManagerRespose
                {
                    Message = "user created successfully",
                    IsSucccess = true,
                };

            }
            return new UserManagerRespose
            {
                    Message = "user did not create",
                    IsSucccess = false,
                    Errors = result.Errors.Select(e=> e.Description)
                };
        }

    }
} 
