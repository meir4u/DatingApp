using AutoMapper;
using DatingApp.Api.Extensions;
using DatingApp.Application.DTOs.Message;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Message.Requests;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DatingApp.Api.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public MessagesController(
            IMediator mediator,
            ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var command = new CreateMessageCommand()
            {
                CreateMessage = createMessageDto,
            };
            var result = await _mediator.Send(command);
            return Ok(result.Message);
        }

        [HttpGet]
        public async Task<ActionResult<MessageDto>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();
            var command = new GetMessagesForUserQuery()
            {
                Params = messageParams
            };
            var result = await _mediator.Send(command);
            Response.AddPaginationHeader(result.PaginationHeader);

            return Ok(result.Messages);
        }

        

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var command = new DeleteMessageCommand()
            {
                DeleteMessage = new DeleteMessageDto()
                {
                    MessageId = id,
                    Username = User.GetUsername()
                },
            };
            var result = await _mediator.Send(command);
            return Ok();
        }
    }
}
