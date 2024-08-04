﻿//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using DatingApp.Api.DTOs;
//using DatingApp.Api.Entities;
//using DatingApp.Api.Helpers;
//using DatingApp.Api.Interfaces;
//using DatingApp.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace DatingApp.Api.Data.Repository
//{
//    public class MessageRepository : IMessageRepository
//    {
//        private readonly DataContext _context;
//        private readonly IMapper _mapper;

//        public MessageRepository(DataContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public void AddGroup(Group group)
//        {
//            _context.Groups.Add(group);
//        }

//        public void AddMessage(Message message)
//        {
//            _context.Messages.Add(message);
//        }

//        public async Task<Connection> GetConnection(string connectionId)
//        {
//            return await _context.Connections.FindAsync(connectionId);
//        }

//        public async Task<Group> GetGroupForConnection(string connectionId)
//        {
//            return await _context.Groups
//                .Include(x=>x.Connections)
//                .Where(x=>x.Connections.Any(c=>c.ConnectionId == connectionId))
//                .FirstOrDefaultAsync();
//        }

//        public async Task<Message> GetMessage(int id)
//        {
//            var message = await _context.Messages.FindAsync(id);
//            return message;
//        }

//        public async Task<Group> GetMessageGroup(string groupName)
//        {
//            return await _context.Groups
//                .Include(x=>x.Connections)
//                .FirstOrDefaultAsync(x=>x.Name == groupName);
//        }

//        public async Task<PagedList<MessageDto>> GetMessagesForUser(IParams messageParams)
//        {
//            var filterParams = (MessageParams)messageParams;
//            var query = _context.Messages
//                .OrderByDescending(x => x.MessageSent)
//                .AsQueryable();

//            query = filterParams.Container switch
//            {
//                "Inbox" => query.Where(u => u.RecipientUsername == filterParams.Username && u.RecipientDeleted == false),
//                "Outbox" => query.Where(u => u.SenderUsername == filterParams.Username && u.SenderDeleted == false),
//                _ => query.Where(u => u.RecipientUsername == filterParams.Username && u.RecipientDeleted == false && u.DateRead == null),
//            };

//            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

//            return await PagedList<MessageDto>.CreateAsync(messages, filterParams.PageNumber, filterParams.PageSize);
//        }

//        public async Task<IQueryable<Message>> GetMessageThread(string currentUserName, string recipientUserName)
//        {
//            var query = _context.Messages
//                .Where(
//                    m=>m.RecipientUsername.ToLower() == currentUserName.ToLower() &&  m.RecipientDeleted == false &&
//                    m.SenderUsername.ToLower() == recipientUserName.ToLower() 
//                    ||
//                    m.RecipientUsername.ToLower() == recipientUserName.ToLower() && m.SenderDeleted == false &&
//                    m.SenderUsername.ToLower() == currentUserName.ToLower()
//                )
//                .OrderBy(m=>m.MessageSent)
//                .AsQueryable();

//            var unreadMessages = query.Where(m=>m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

//            if (unreadMessages.Any())
//            {
//                foreach(var message in unreadMessages)
//                {
//                    message.DateRead = DateTime.UtcNow;

//                }
//            }
//            return query;
//        }

//        public void RemoveConnection(Connection connection)
//        {
//            _context.Connections.Remove(connection);
//        }

//        public void RemoveMessage(Message message)
//        {
//            _context.Messages.Remove(message);
//        }

//    }
//}
