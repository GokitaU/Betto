﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Betto.Model.Models;
using Betto.Model.ViewModels;
using Betto.Model.WriteModels;
using Betto.Services;
using Betto.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Betto.Api.Controllers.UsersController
{
    [ApiController, Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;

        public UsersController(IUserService userService,
            ITicketService ticketService)
        {
            _userService = userService;
            _ticketService = ticketService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<WebTokenViewModel>> AuthenticateUserAsync([FromBody] LoginWriteModel loginData)
        {
            try
            {
                var response = await _userService.AuthenticateUserAsync(loginData);

                return response.StatusCode == StatusCodes.Status200OK
                    ? Ok(response.Result)
                    : StatusCode(response.StatusCode, response.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorViewModel.Factory.NewErrorFromException(e));
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> SignUpAsync([FromBody] SignUpWriteModel signUpData)
        {
            try
            {
                var response = await _userService.SignUpAsync(signUpData);

                return response.StatusCode == StatusCodes.Status201Created
                    ? Ok(response.Result)
                    : StatusCode(response.StatusCode, response.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorViewModel.Factory.NewErrorFromException(e));
            }
        }

        [HttpGet("{userId:int}/tickets"), Authorize]
        public async Task<ActionResult<ICollection<TicketViewModel>>> GetUserTicketsAsync(int userId)
        {
            try
            {
                var response = await _ticketService.GetUserTicketsAsync(userId);

                return response.StatusCode == StatusCodes.Status200OK
                    ? Ok(response.Result)
                    : StatusCode(response.StatusCode, response.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorViewModel.Factory.NewErrorFromException(e));
            }
        }
    }
}
