using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.Photo.Responses;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Photo.Handlers
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, DeletePhotoResponse>
    {
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePhotoCommandHandler(
            IPhotoService photoService,
            IUnitOfWork unitOfWork)
        {
            _photoService = photoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeletePhotoResponse> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var response = new DeletePhotoResponse();

            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.Delete.Username);

            //var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(request.Delete.PhotoId);

            if (photo == null) throw new NotFoundException();
            if (photo.IsMain) throw new BadRequestExeption("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) throw new BadRequestExeption(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _unitOfWork.Complete())
            {
                return response;
            };

            throw new BadRequestExeption("Problem deleting photo");
        }
    }
}
