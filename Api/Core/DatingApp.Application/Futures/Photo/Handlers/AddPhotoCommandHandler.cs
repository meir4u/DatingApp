using AutoMapper;
using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.Photo.Responses;
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
    internal class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, AddPhotoResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;

        public AddPhotoCommandHandler(
            IMapper mapper,
            IPhotoService photoService, 
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _photoService = photoService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddPhotoResponse> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
        {
            var response = new AddPhotoResponse();

            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(request.Add.Username);

            if (user == null) throw new NotFoundException();

            var result = await _photoService.AddPhotoAsync(request.Add.File);

            if (result.Error != null) throw new BadRequestExeption(result.Error.Message);


            var photo = new Domain.Entities.Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            user.Photos.Add(photo);

            if (await _unitOfWork.Complete())
            {
                response.Photo = _mapper.Map<PhotoDto>(photo);
                response.Username = user.UserName;
                return response;
            }

            throw new BadRequestExeption("Problem adding photo");
        }
    }
}
