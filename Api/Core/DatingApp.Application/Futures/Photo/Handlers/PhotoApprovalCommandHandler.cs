using AutoMapper;
using DatingApp.Application.DTOs.Photo;
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
    public class PhotoApprovalCommandHandler : IRequestHandler<PhotoApprovalCommand, PhotoApprovalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PhotoApprovalCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PhotoApprovalResponse> Handle(PhotoApprovalCommand request, CancellationToken cancellationToken)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhotoById(request.ForApproval.Id);

            if (photo == null) throw new NotFoundException();
            photo.IsApproved = true;

            var user = await _unitOfWork.UserRepository.GetUserPhotoIdAsync(request.ForApproval.Id);

            if (user == null) throw new NotFoundException();

            if (user.Photos.Any() == false) photo.IsMain = true;

            _unitOfWork.PhotoRepository.Update(photo);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();
            var photoDto = _mapper.Map<PhotoForApprovalDto>(photo);
            photoDto.UserName = user.UserName;

            var response = new PhotoApprovalResponse();
            response.Photo = photoDto;
            return response;
        }
    }
}
