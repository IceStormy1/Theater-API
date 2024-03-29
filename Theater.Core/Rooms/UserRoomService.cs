﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater.Abstractions;
using Theater.Abstractions.Errors;
using Theater.Abstractions.Message;
using Theater.Abstractions.Rooms;
using Theater.Common;
using Theater.Common.Constants;
using Theater.Common.Enums;
using Theater.Contracts.Rabbit;
using Theater.Contracts.Rooms;
using Theater.Entities.Rooms;
using Theater.Entities.Users;

namespace Theater.Core.Rooms;

public sealed class UserRoomService : IUserRoomService
{
    public const byte MaxUsersInIndividualRoom = 2;

    private readonly IRoomRepository _roomRepository;
    private readonly IUserRoomsRepository _userRoomsRepository;
    private readonly IPublishEndpoint _messageBus;
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageService _messageService;
    private readonly ICrudRepository<UserEntity> _userCrudRepository;

    public UserRoomService(
        IRoomRepository roomRepository, 
        IUserRoomsRepository userRoomsRepository, 
        IPublishEndpoint messageBus, 
        IMessageRepository messageRepository, 
        IMessageService messageService,
        ICrudRepository<UserEntity> userCrudRepository)
    {
        _roomRepository = roomRepository;
        _userRoomsRepository = userRoomsRepository;
        _messageBus = messageBus;
        _messageRepository = messageRepository;
        _messageService = messageService;
        _userCrudRepository = userCrudRepository;
    }

    public async Task<Result> LeaveRoom(Guid userId, Guid roomId)
    {
        var userRoom = await _roomRepository.GetActiveRoomRelationForUser(userId, roomId);

        if (userRoom is null)
            return RoomErrors.RoomNotFoundError;

        await DeleteUserAndPublishMessage(userRoom);

        await AddAndPublishUserLeaveMessage(userRoom, roomId);

        return Result.Successful;
    }

    public async Task<Result> InviteUsersToRoom(Guid userId, Guid roomId, InviteUsersModel inviteUsersModel)
    {
        var userRoom = await _roomRepository.GetActiveRoomRelationForUser(userId, roomId);
        if(userRoom is null)
            return RoomErrors.RoomNotFoundError;

        var roomMembers = await _userRoomsRepository.GetRoomMembers(roomId);
        var totalMembers = roomMembers.Count + inviteUsersModel.InvitedUsersIds.Count;

        if (userRoom.Room.Type == RoomType.Individual && totalMembers > MaxUsersInIndividualRoom)
            return RoomErrors.InvalidTotalMembersError;

        var usersNotInRoom = await _userRoomsRepository.GetUsersNotInRoom(roomId, inviteUsersModel.InvitedUsersIds);

        if (usersNotInRoom.Count == default)
            return Result.Successful;

        var requestUser = await _userCrudRepository.GetByEntityId(userId);

        await AddUsersToRoom(requestUser, userRoom.Room, usersNotInRoom);

        await AddUserInvitedMessages(requestUser, roomId, usersNotInRoom);

        return Result.Successful;
    }

    public async Task<Result> ReadMessage(Guid userId, Guid roomId, Guid messageId)
    {
        var message = await _messageRepository.GetByEntityId(messageId);

        if (message is null)
            return MessageErrors.MessageNotFoundError;

        await _userRoomsRepository.UpdateLastReadMessage(
            roomId: roomId,
            userId: userId,
            messageId: messageId,
            messageTime: DateTime.UtcNow);

        if (message.MessageType != MessageType.SystemText)
        {
            var messageEvent = new MessageReadModel(
                roomId: roomId,
                messageId: messageId,
                messageAuthorId: message.UserId,
                messageTime: DateTime.UtcNow);

            await _messageBus.Publish(messageEvent);
        }

        return Result.Successful;
    }

    private async Task DeleteUserAndPublishMessage(UserRoomEntity userRoom)
    {
        userRoom.IsActive = false;

        await _userRoomsRepository.Update(userRoom);
        await _messageBus.Publish(new UserExitModel { RoomId = userRoom.RoomId, UserId = userRoom.UserId });
    }

    private async Task AddAndPublishUserLeaveMessage(UserRoomEntity userRoom, Guid roomId)
    {
        var message = new MessageEntity(
            userId: userRoom.UserId,
            roomId: roomId,
            text: string.Format(MessageConstants.SystemUserLeaveFromRoom, userRoom.User.FullName),
            type: MessageType.SystemText);

        await _messageRepository.Add(message);
        await _messageService.PublishMessageSent(userRoom.UserId, roomId, message);
    }

    private async Task AddUsersToRoom(UserEntity requestUser, RoomEntity room, IEnumerable<UserEntity> invitedUsers)
    {
        var notAddedUsers = invitedUsers
            .Select(userEntity => new UserRoomEntity
            {
                Role = RoomRole.User,
                RoomId = room.Id,
                UserId = userEntity.Id
            })
            .ToList();

        await _userRoomsRepository.AddRange(notAddedUsers);

        var userJoinedTasks = notAddedUsers.Select(x =>
            _messageBus.Publish(new UserJoinedModel
            {
                RoomId = room.Id, 
                RoomTitle = room.Type == RoomType.Individual ? requestUser.FullName : room.Title,
                UserId = x.UserId
            }));

        await Task.WhenAll(userJoinedTasks);
    }

    private async Task AddUserInvitedMessages(UserEntity requestUser, Guid roomId, IEnumerable<UserEntity> roomMembers)
    {
        var messageEntities = roomMembers
            .Select(x => new MessageEntity(
                userId: requestUser.Id, 
                roomId: roomId, 
                text: string.Format(MessageConstants.SystemSignInMessage, requestUser.FullName, x.FullName), 
                type: MessageType.SystemText))
            .ToList();

        await _messageRepository.AddRange(messageEntities);

        var messageSentTasks = messageEntities.Select(message =>
            _messageService.PublishMessageSent(requestUser.Id, roomId, message));

        await Task.WhenAll(messageSentTasks);
    }
}