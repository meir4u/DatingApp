using DatingApp.Application.Futures.Email.Account.Requests;
using DatingApp.Application.Futures.Email.Account.Responses;
using DatingApp.Domain.Services;
using MediatR;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace DatingApp.Application.Futures.Email.Account.Handlers
{
    /// <summary>
    /// Notifies users when a new device or browser logs into their account.
    /// </summary>
    public class DeviceLoginNotificationEmailHandler : IRequestHandler<DeviceLoginNotificationEmailRequest, DeviceLoginNotificationEmailResponse>
    {
        private readonly IEnhancedEmailService _enhancedEmailService;

        public DeviceLoginNotificationEmailHandler(IEnhancedEmailService enhancedEmailService)
        {
            _enhancedEmailService = enhancedEmailService;
        }
        public Task<DeviceLoginNotificationEmailResponse> Handle(DeviceLoginNotificationEmailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
