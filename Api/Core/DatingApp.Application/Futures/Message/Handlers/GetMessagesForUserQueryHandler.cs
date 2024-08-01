using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs.Message;
using DatingApp.Application.Futures.Message.Requests;
using DatingApp.Application.Futures.Message.Responses;
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

namespace DatingApp.Application.Futures.Message.Handlers
{
    public class GetMessagesForUserQueryHandler : IRequestHandler<GetMessagesForUserQuery, GetMessagesForUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMessagesForUserQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetMessagesForUserResponse> Handle(GetMessagesForUserQuery request, CancellationToken cancellationToken)
        {
            var response = new GetMessagesForUserResponse();
            
            var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(request.Params);

            var mappedMessages = messages.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            var pagedList =  await PagedList<MessageDto>.CreateAsync(mappedMessages, request.Params.PageNumber, request.Params.PageSize);

            response.PaginationHeader = new PaginationHeader(pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);

            return response;
        }
    }
}
