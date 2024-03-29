﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theater.Abstractions.Errors;
using Theater.Abstractions.Message;
using Theater.Abstractions.UserAccount;
using Theater.Contracts.Filters;
using Theater.Contracts.Messages;
using Theater.Controllers.Base;

namespace Theater.Controllers.Rooms;

[SwaggerTag("Пользовательские методы для работы с сообщениями чата")]
[Authorize]
[Route("api/rooms/{roomId:guid}")]
public sealed class MessageController : BaseController
{
    private readonly IMessageService _messageService;

    public MessageController(
        IMapper mapper,
        IMessageService messageService,
        IUserAccountService userAccountService) : base(mapper, userAccountService)
    {
        _messageService = messageService;
    }

    /// <summary>
    /// Отправка нового сообщения
    /// </summary>
    /// <param name="roomId">Идентификатор активной комнаты</param>
    /// <param name="newMessage">Тело нового сообщения</param>
    [HttpPost("message")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> SendMessage([FromRoute] Guid roomId, [FromBody] NewMessageModel newMessage)
    {
        var innerUserId = await GetUserId();

        return !innerUserId.HasValue
            ? RenderResult(UserAccountErrors.Unauthorized)
            : RenderResult(await _messageService.SendMessage(roomId, innerUserId.Value, newMessage));
    }

    /// <summary>
    /// Получение списка сообщений чата с пагинацией
    /// </summary>
    /// <param name="roomId">Идентификатор активной комнаты</param>
    /// <param name="filter">Параметры пагинации</param>
    [HttpGet("messages")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MessageModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetMessages([FromRoute] Guid roomId, [FromQuery] MessageFilterParameters filter)
    {
        var innerUserId = await GetUserId();

        return !innerUserId.HasValue
            ? RenderResult(UserAccountErrors.Unauthorized)
            : RenderResult(await _messageService.GetMessages(roomId, innerUserId.Value, filter));
    }
}