using DatingApp.Application.Futures.Photo.Requests;
using DatingApp.Application.Futures.Photo.Responses;
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
    public class GetPhotosForModerationCommandHandler : IRequestHandler<GetPhotosForModerationCommand, GetPhotosForModerationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPhotosForModerationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPhotosForModerationResponse> Handle(GetPhotosForModerationCommand request, CancellationToken cancellationToken)
        {
            var response = new GetPhotosForModerationResponse();

            var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
            response.Photos = photos;

            return response;
        }
    }
}
