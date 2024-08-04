using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Account.Handlers
{
    public class UserLastActiveUpdateCommandHandler : IRequestHandler<UserLastActiveUpdateCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserLastActiveUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UserLastActiveUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.UpdateUserLastActive(request.UserLastActiveUpdate.UserId);

            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}
