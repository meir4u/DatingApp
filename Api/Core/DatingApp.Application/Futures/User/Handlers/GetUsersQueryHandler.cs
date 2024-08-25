using AutoMapper;
using DatingApp.Application.DTOs.Member;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoMapper.QueryableExtensions;

namespace DatingApp.Application.Futures.User.Handlers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersQueryHandler(
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
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

                var pagedUsers =  await PagedList<MemberDto>.CreateAsync(
                                                            users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
                                                            request.Params.PageNumber,
                                                            request.Params.PageSize);

                response.PaginationHeader = new PaginationHeader(pagedUsers.CurrentPage, pagedUsers.PageSize, pagedUsers.TotalCount, pagedUsers.TotalPages);
                response.Users = pagedUsers;

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
