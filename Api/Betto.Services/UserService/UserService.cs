﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Betto.DataAccessLayer.Repositories;
using Betto.Helpers;
using Betto.Model.ViewModels;
using Betto.Model.Entities;
using Betto.Model.Models;
using Betto.Model.WriteModels;
using Betto.Resources.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Betto.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRegistrationValidator _registrationValidator;
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        public UserService(IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            IRegistrationValidator registrationValidator,
            IStringLocalizer<ErrorMessages> localizer)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _registrationValidator = registrationValidator;
            _localizer = localizer;
        }

        public async Task<RequestResponseModel<WebTokenViewModel>> AuthenticateUserAsync(LoginWriteModel loginModel)
        {
            var errors = await CheckLoginCredentials(loginModel);

            if (errors.Any())
            {
                return new RequestResponseModel<WebTokenViewModel>(StatusCodes.Status400BadRequest, 
                    errors, 
                    null);
            }

            var authenticationToken = _tokenGenerator.GenerateToken(loginModel.Username);

            return new RequestResponseModel<WebTokenViewModel>(StatusCodes.Status200OK,
                Enumerable.Empty<ErrorViewModel>(),
                new WebTokenViewModel
                {
                    AuthenticationToken = authenticationToken,
                    Username = loginModel.Username
                });
        }

        public async Task<RequestResponseModel<UserViewModel>> SignUpAsync(RegistrationWriteModel signUpData)
        {
            var errors = await CheckSignUpDataBeforeRegisteringAsync(signUpData);

            if (errors.Any())
            {
                return new RequestResponseModel<UserViewModel>(StatusCodes.Status400BadRequest, 
                    errors, 
                    null);
            }

            var passwordHash = _passwordHasher.EncodePassword(signUpData.Password);

            var registeredUser = await _userRepository.SignUpAsync(new UserEntity
            {
                MailAddress = signUpData.MailAddress,
                Username = signUpData.Username,
                PasswordHash = passwordHash
            });

            if (registeredUser == null)
            {
                return new RequestResponseModel<UserViewModel>(StatusCodes.Status400BadRequest,
                    new List<ErrorViewModel>
                    {
                        new ErrorViewModel
                        {
                            Message = _localizer["UserCreationErrorMessage"]
                                .Value
                        }
                    },
                    null);
            }

            await _userRepository.SaveChangesAsync();
            var result = (UserViewModel) registeredUser;

            return new RequestResponseModel<UserViewModel>(StatusCodes.Status201Created, 
                Enumerable.Empty<ErrorViewModel>(), 
                result);
        }

        public async Task<bool> CheckIsUsernameAlreadyTakenAsync(string username) =>
            await _userRepository.GetUserByUsernameAsync(username) != null;

        private async Task<ICollection<ErrorViewModel>> CheckSignUpDataBeforeRegisteringAsync(
            RegistrationWriteModel signUpModel)
        {
            var errors = _registrationValidator.ValidateRegistrationModel(signUpModel);

            if (!errors.Any())
            {
                await CheckUsernameBeforeSignUpAsync(signUpModel.Username, errors);
                await CheckMailAddressBeforeSignUpAsync(signUpModel.MailAddress, errors);
            }

            return errors;
        }

        private async Task<ICollection<ErrorViewModel>> CheckLoginCredentials(LoginWriteModel loginModel)
        {
            var errors = new List<ErrorViewModel>();

            var doesUserExist = await CheckDoesTheUserExistAsync(loginModel.Username, errors);

            if (doesUserExist)
            {
                await VerifyUserPasswordAsync(loginModel, errors);
            }

            return errors;
        }

        private async Task CheckUsernameBeforeSignUpAsync(string username, ICollection<ErrorViewModel> errors)
        {
            var doesExist = await CheckIsUsernameAlreadyTakenAsync(username);

            if (doesExist)
            {
                errors.Add(new ErrorViewModel
                {
                    Message = _localizer["UsernameAlreadyTakenErrorMessage",
                            username]
                        .Value
                });
            }
        }

        private async Task CheckMailAddressBeforeSignUpAsync(string mailAddress, ICollection<ErrorViewModel> errors)
        {
            var isMailTaken = await CheckIsMailAlreadyTakenAsync(mailAddress);

            if (isMailTaken)
            {
                errors.Add(new ErrorViewModel
                {
                    Message = _localizer["MailAddressAlreadyTakenErrorMessage",
                            mailAddress]
                        .Value
                });
            }
        }

        private async Task<bool> CheckDoesTheUserExistAsync(string username, ICollection<ErrorViewModel> errors)
        {
            var doesExist = await CheckIsUsernameAlreadyTakenAsync(username);

            if (!doesExist)
            {
                errors.Add(new ErrorViewModel
                {
                    Message = _localizer["UserNotFoundErrorMessage", 
                        username]
                        .Value
                });
            }

            return doesExist;
        }

        private async Task VerifyUserPasswordAsync(LoginWriteModel loginModel, ICollection<ErrorViewModel> errors)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginModel.Username);
            var authenticated = _passwordHasher.VerifyPassword(user.PasswordHash, loginModel.Password);

            if (!authenticated)
            {
                errors.Add(new ErrorViewModel
                {
                    Message = _localizer["IncorrectPasswordErrorMessage"]
                        .Value
                });
            }
        }

        private async Task<bool> CheckIsMailAlreadyTakenAsync(string mailAddress) =>
            await _userRepository.GetUserByMailAsync(mailAddress) != null;
    }
}
