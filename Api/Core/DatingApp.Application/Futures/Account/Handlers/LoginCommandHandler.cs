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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginResponse();

            var user = await _userManager.Users
                                .Include(p => p.Photos)
                                .SingleOrDefaultAsync(x => x.UserName.ToLower() == request.Login.Username.ToLower());
            if (user == null)
            {
                throw new NotAuthorizedException("Invalid username");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Login.Password);
            if (!result) throw new NotAuthorizedException("Invalid password");

            response.User = new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender,
            };

            return response;
        }
    }
}
