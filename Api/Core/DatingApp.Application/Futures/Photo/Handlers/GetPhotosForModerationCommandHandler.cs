using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.Photo.Responses;
using DatingApp.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DatingApp.Application.Futures.Photo.Handlers
{
    public class GetPhotosForModerationCommandHandler : IRequestHandler<GetPhotosForModerationCommand, GetPhotosForModerationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetPhotosForModerationCommandHandler(
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPhotosForModerationResponse> Handle(GetPhotosForModerationCommand request, CancellationToken cancellationToken)
        {
            var response = new GetPhotosForModerationResponse();

            var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
            var data = await photos.ProjectTo<PhotoForApprovalDto>(_mapper.ConfigurationProvider).ToListAsync();
            response.Photos = data;

            return response;
        }
    }
}
