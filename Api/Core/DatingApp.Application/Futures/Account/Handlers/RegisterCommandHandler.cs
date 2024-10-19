using AutoMapper;
using DatingApp.Application.DTOs.Register;
using DatingApp.Application.DTOs.User;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Account.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(
            UserManager<AppUser> userManager,
            IMapper mapper,
            ITokenService tokenService, 
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new RegisterResponse();

            if (await userExists(request.Register.Username))
            {
                throw new BadRequestExeption("Username already taken");
            }
            var user = _mapper.Map<AppUser>(request.Register);

            user.UserName = request.Register.Username.ToLower();

            var result = await _userManager.CreateAsync(user, request.Register.Password);

            if (!result.Succeeded) throw new IdentityErrorExeption(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) throw new IdentityErrorExeption(roleResult.Errors);

            response.User = new UserDto
            {
                Username = request.Register.Username,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = request.Register.KnownAs,
                Gender = user.Gender,
                Email = user.Email,
            };

            return response;
        }

        private async Task<bool> userExists(string username)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
            return user != null;
        }
    }
}
