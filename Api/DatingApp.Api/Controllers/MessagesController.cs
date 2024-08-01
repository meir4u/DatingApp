using AutoMapper;
using DatingApp.Api.Data;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using DatingApp.Application.DTOs.Message;
using DatingApp.Application.Exceptions.Responses;
using DatingApp.Application.Futures.Account.Requests;
using DatingApp.Application.Futures.Message.Requests;
using DatingApp.Application.Pagination;
using DatingApp.Application.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MessagesController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            try
            {
                var command = new CreateMessageCommand()
                {
                    CreateMessage = createMessageDto,
                };
                var result = await _mediator.Send(command);
                return Ok(result.Message);
            }
            catch(NotFoundException ex)
            {
                return NotFound();
            }
            catch (BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            //var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            //if (recipient == null) return NotFound();

            //var message = new Message
            //{
            //    Sender = sender,
            //    Recipient = recipient,
            //    SenderUsername = sender.UserName,
            //    RecipientUsername = recipient.UserName,
            //    Content = createMessageDto.Content,
            //};

            //_unitOfWork.MessageRepository.AddMessage(message);

            //if( await _unitOfWork.Complete()) return Ok(_mapper.Map<MessageDto>(message));

            //return BadRequest("Failed to send message");
        }

        [HttpGet]
        public async Task<ActionResult<MessageDto>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            try
            {
                var command = new GetMessagesForUserQuery()
                {
                    Params = messageParams
                };
                var result = await _mediator.Send(command);
                Response.AddPaginationHeader(result.PaginationHeader);

                return Ok(result.Messages);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            ////////////////////
            //messageParams.Username = User.GetUsername();

            //var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);

            //Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));

            //return Ok(messages);
        }

        

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            try
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
            catch(NotAuthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (BadRequestExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var message = await _unitOfWork.MessageRepository.GetMessage(id);

            //if(message.SenderUsername != username && message.RecipientUsername != username) 
            //{
            //    //user not parth of the convirsation.
            //    return Unauthorized();
            //}

            //if(message.SenderUsername == username) message.SenderDeleted = true;
            //if(message.RecipientUsername == username) message.RecipientDeleted = true;

            //if(message.SenderDeleted && message.RecipientDeleted)
            //{
            //    _unitOfWork.MessageRepository.RemoveMessage(message);
            //}

            //if (await _unitOfWork.Complete()) return Ok();

            //return BadRequest("Problem deleting the message");
        }
    }
}
