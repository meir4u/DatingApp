using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Account.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
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
    public class EditRolesCommandHandler : IRequestHandler<EditRolesCommand, EditRolesResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public EditRolesCommandHandler(
            UserManager<AppUser> userManager, 
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<EditRolesResponse> Handle(EditRolesCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(request.EditRoles.Username);

            if (user == null) throw new NotFoundException();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, request.EditRoles.Roles.Except(userRoles));

            if (!result.Succeeded) throw new BadRequestExeption("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(request.EditRoles.Roles));

            if (!result.Succeeded) throw new BadRequestExeption("Failed to remove from roles");

            var response = new EditRolesResponse();
            response.Roles = await _userManager.GetRolesAsync(user);

            return response;
        }
    }
}
