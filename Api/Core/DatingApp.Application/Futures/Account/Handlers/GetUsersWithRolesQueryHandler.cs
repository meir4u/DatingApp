using DatingApp.Application.DTOs.Account;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Account.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
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
    public class GetUsersWithRolesQueryHandler : IRequestHandler<GetUsersWithRolesQuery, GetUsersWithRolesResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersWithRolesQueryHandler(
            UserManager<AppUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUsersWithRolesResponse> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
        {
            var response = new GetUsersWithRolesResponse();

            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new GetUserWithRolesDto
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();
            response.UserWithRoles = users;

            return response;
        }
    }
}
