using AutoMapper;
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
        private readonly ILogger _logger;

        public MessageRepository(DataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddGroup(Group group)
        {
            try
            {
                _context.Groups.Add(group);
            }
            catch(Exception  ex)
            {
                _logger.Error(ex, "{@group}", group);
                throw;
            }
            
        }

        public void AddMessage(Message message)
        {
            try
            {
                _context.Messages.Add(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@message}", message);
                throw;
            }
            
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            try
            {
                return await _context.Connections.FindAsync(connectionId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{connectionId}", connectionId);
                throw;
            }
            
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            try
            {
                return await _context.Groups
                .Include(x => x.Connections)
                .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{connectionId}", connectionId);
                throw;
            }
            
        }

        public async Task<Message> GetMessage(int id)
        {
            try
            {
                var message = await _context.Messages.FindAsync(id);
                return message;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{id}", id);
                throw;
            }
            
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            try
            {
                return await _context.Groups
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{groupName}", groupName);
                throw;
            }
            
        }

        public async Task<IQueryable<Message>> GetMessagesForUser(IParams messageParams)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error(ex, "{@messageParams}", messageParams);
                throw;
            }
            
        }

        public async Task<IQueryable<Message>> GetMessageThread(string currentUserName, string recipientUserName)
        {
            try
            {
                var query = _context.Messages
                .Where(
                    m => m.RecipientUsername.ToLower() == currentUserName.ToLower() && m.RecipientDeleted == false &&
                    m.SenderUsername.ToLower() == recipientUserName.ToLower()
                    ||
                    m.RecipientUsername.ToLower() == recipientUserName.ToLower() && m.SenderDeleted == false &&
                    m.SenderUsername.ToLower() == currentUserName.ToLower()
                )
                .OrderBy(m => m.MessageSent)
                .AsQueryable();

                var unreadMessages = query.Where(m => m.DateRead == null && m.RecipientUsername == currentUserName).ToList();

                if (unreadMessages.Any())
                {
                    foreach (var message in unreadMessages)
                    {
                        message.DateRead = DateTime.UtcNow;

                    }
                }

                return query;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{currentUserName} {recipientUserName}", currentUserName, recipientUserName);
                throw;
            }
            
        }

        public void RemoveConnection(Connection connection)
        {
            try
            {
                _context.Connections.Remove(connection);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@connection}", connection);
                throw;
            }
            
        }

        public void RemoveMessage(Message message)
        {
            try
            {
                _context.Messages.Remove(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@message}", message);
                throw;
            }
           
        }

    }
}
