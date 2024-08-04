﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs.Message;
using DatingApp.Domain.Entities;
using DatingApp.Application.Params;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Params;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Application.Pagination;
using Serilog;

namespace DatingApp.Infrastructure.Data.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MessageRepository(DataContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups
                .Include(x=>x.Connections)
                .Where(x=>x.Connections.Any(c=>c.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            return message;
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups
                .Include(x=>x.Connections)
                .FirstOrDefaultAsync(x=>x.Name == groupName);
        }

        public async Task<IQueryable<Message>> GetMessagesForUser(IParams messageParams)
        {
            var filterParmas = (MessageParams)messageParams;
            var query = _context.Messages
                .OrderByDescending(x => x.MessageSent)
                .AsQueryable();

            query = filterParmas.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == filterParmas.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderUsername == filterParmas.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientUsername == filterParmas.Username && u.RecipientDeleted == false && u.DateRead == null),
            };

            return query;
        }

        public async Task<IQueryable<Message>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            var query = _context.Messages
                .Where(
                    m=>m.RecipientUsername.ToLower() == currentUserName.ToLower() &&  m.RecipientDeleted == false &&
                    m.SenderUsername.ToLower() == recipientUserName.ToLower() 
                    ||
                    m.RecipientUsername.ToLower() == recipientUserName.ToLower() && m.SenderDeleted == false &&
                    m.SenderUsername.ToLower() == currentUserName.ToLower()
                )
                .OrderBy(m=>m.MessageSent)
                .AsQueryable();

            var unreadMessages = query.Where(m=>m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

            if (unreadMessages.Any())
            {
                foreach(var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;

                }
            }

            return query;
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public void RemoveMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

    }
}
