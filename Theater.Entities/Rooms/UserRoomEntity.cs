﻿using System;
using Theater.Common.Enums;
using Theater.Entities.Users;

namespace Theater.Entities.Rooms;

/// <summary>
/// Участник чата
/// </summary>
public sealed class UserRoomEntity : BaseEntity, IHasCreatedAt
{
    public Guid UserId { get; set; }

    /// <inheritdoc cref="UserEntity"/>
    public UserEntity User { get; set; }

    public Guid RoomId { get; set; }
    /// <inheritdoc cref="RoomEntity"/>
    public RoomEntity Room { get; set; }

    /// <summary>
    /// false - пользователь неактивен в чате
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Дата и время присоединения к чату
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc cref="RoomRole"/>
    public RoomRole Role { get; set; }

    // TODO: последнее прочитанное сообщение
}