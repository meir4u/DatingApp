using DatingApp.Application.Futures.Email.Base;
using DatingApp.Application.Futures.Email.Marketing.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Marketing.Requests
{
    public class ReferralInvitationEmailRequest : BaseEmailRequest, IRequest<ReferralInvitationEmailResponse>
    {
    }
}
