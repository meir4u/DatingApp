using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.User.Requests;
using DatingApp.Application.Futures.User.Responses;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.User.Handlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetUsersResponse();

                var gender = await _unitOfWork.UserRepository.GetUserGender(request.Params.CurrentUsername);

                if (string.IsNullOrEmpty(request.Params.Gender))
                {
                    request.Params.Gender = gender == "male" ? "female" : "male";
                }
                var users = await _unitOfWork.UserRepository.GetMembersAsync(request.Params);

                response.PaginationHeader = new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
                response.Users = users;

                return response;

            }
            catch (Exception ex)
            {
#if DEBUG

                throw new BadRequestExeption(ex.Message);

#else
                
                // In release mode, return a generic BadRequest response
                return BadRequest("An error occurred while processing your request.");
                
#endif
            }
        }
    }
}
