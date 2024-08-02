using DatingApp.Application.DTOs.Like;
using DatingApp.Application.Futures.Like.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Futures.Like.Requests
{
    public class RemoveLikeCommand : IRequest<RemoveLikeResponse>
    {
    }
}
