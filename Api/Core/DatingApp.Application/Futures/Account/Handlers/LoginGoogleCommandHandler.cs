using DatingApp.Application.DTOs.User;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Account.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Handlers
{
    public class LoginGoogleCommandHandler : IRequestHandler<LoginGoogleCommand, LoginGoogleResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationUserService _authenticationService;

        public LoginGoogleCommandHandler(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IAuthenticationUserService authenticationService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }
        public async Task<LoginGoogleResponse> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginGoogleResponse();

            var googleData = await _authenticationService.AuthenticateWithGoogleAsync(request.Login.Code);

            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(googleData.Email);

            if (user == null)
            {
                throw new NotAuthorizedException("Invalid username");
            }

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
