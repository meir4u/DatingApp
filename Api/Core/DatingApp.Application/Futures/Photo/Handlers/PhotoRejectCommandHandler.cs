using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.Photo.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Handlers
{
    public class PhotoRejectCommandHandler : IRequestHandler<PhotoRejectCommand, PhotoRejectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoRejectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PhotoRejectResponse> Handle(PhotoRejectCommand request, CancellationToken cancellationToken)
        {
            var response = new PhotoRejectResponse();
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(request.Reject.PhotoId);

            if (photo == null) throw new NotFoundException();
            _unitOfWork.PhotoRepository.RemovePhoto(photo);
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            return response;
            throw new NotImplementedException();
        }
    }
}
