﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Helpers;
using DatingApp.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            return message;
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(x => x.MessageSent)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null),
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var messages = await _context.Messages
                .Include(u=>u.Sender).ThenInclude(p=>p.Photos)
                .Include(u=>u.Recipient).ThenInclude(p=>p.Photos)
                .Where(
                    m=>m.RecipientUsername.ToLower() == currentUserName.ToLower() &&  m.RecipientDeleted == false &&
                    m.SenderUsername.ToLower() == recipientUserName.ToLower() 
                    ||
                    m.RecipientUsername.ToLower() == recipientUserName.ToLower() && m.SenderDeleted == false &&
                    m.SenderUsername.ToLower() == currentUserName.ToLower()
                )
                .OrderBy(m=>m.MessageSent)
                .ToListAsync();
            var unreadMessages = messages.Where(m=>m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach(var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;

                }
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public void RemoveMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
