using AutoMapper;
using DatingApp.Application.DTOs.Message;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Message.Requests;
using DatingApp.Application.Futures.Message.Responses;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Message.Handlers
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMessageCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateMessageResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateMessageResponse();

            var username = request.CreateMessage.Username;
            if (username == request.CreateMessage.RecipientUsername)
            {
                throw new BadRequestExeption("You cannot send message to yourself");
            }

            var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.CreateMessage.RecipientUsername);

            if (recipient == null) throw new NotFoundException();

            var message = new Domain.Entities.Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = request.CreateMessage.Content,
            };

            _unitOfWork.MessageRepository.AddMessage(message);

            if (await _unitOfWork.Complete()) 
            {
                var mapped = _mapper.Map<MessageDto>(message);
                response.Message = mapped;
            }

            throw new BadRequestExeption("Failed to send message");
        }
    }
}
