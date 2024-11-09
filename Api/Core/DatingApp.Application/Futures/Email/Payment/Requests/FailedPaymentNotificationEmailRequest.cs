using DatingApp.Application.Futures.Email.Payment.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Payment.Requests
{
    public class FailedPaymentNotificationEmailRequest : IRequest<FailedPaymentNotificationEmailResponse>
    {
    }
}
