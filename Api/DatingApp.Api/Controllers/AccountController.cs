﻿using AutoMapper;
using DatingApp.Application.DTOs.Account;
using DatingApp.Application.DTOs.Register;
using DatingApp.Application.DTOs.User;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Serilog;
using ILogger = Serilog.ILogger;

namespace DatingApp.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AccountController(
            IMediator mediator,
            ILogger logger
            )
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")] // Post: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                var command = new RegisterCommand()
                {
                    Register = registerDto,
                };
                var result = await _mediator.Send(command);
                return Ok(result.User);
            }
            catch(BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IdentityErrorExeption ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var command = new LoginCommand()
                {
                    Login = loginDto
                };
                var result = await _mediator.Send(command);
                return Ok(result.User);
            }
            catch(NotAuthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
