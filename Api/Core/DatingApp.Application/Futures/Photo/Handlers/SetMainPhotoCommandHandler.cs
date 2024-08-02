using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.Photo.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Handlers
{
    public class SetMainPhotoCommandHandler : IRequestHandler<SetMainPhotoCommand, SetMainPhotoResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetMainPhotoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SetMainPhotoResponse> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
        {
            var response = new SetMainPhotoResponse();

            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.SetMainPhoto.Username);

            if (user == null) throw new NotFoundException();

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.SetMainPhoto.PhotoId);

            if (photo == null) throw new NotFoundException();

            if (photo.IsMain) throw new BadRequestExeption("this is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;

            if (await _unitOfWork.Complete()) return response;

            throw new BadRequestExeption("Problem Setting main photo");
        }
    }
}
