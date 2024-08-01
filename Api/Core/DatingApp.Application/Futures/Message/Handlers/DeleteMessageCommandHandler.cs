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
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, DeleteMessageResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteMessageResponse> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteMessageResponse();

            var username = request.DeleteMessage.Username;

            var message = await _unitOfWork.MessageRepository.GetMessage(request.DeleteMessage.MessageId);

            if (message.SenderUsername != username && message.RecipientUsername != username)
            {
                //user not parth of the convirsation.
                throw new NotAuthorizedException("User not parth of the convirsation");
            }

            if (message.SenderUsername == username) message.SenderDeleted = true;
            if (message.RecipientUsername == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _unitOfWork.MessageRepository.RemoveMessage(message);
            }

            if (await _unitOfWork.Complete()) return response;

            throw new BadRequestExeption("Problem deleting the message");
        }
    }
}
