﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using Theater.Abstractions.Ticket;
using Theater.Abstractions.UserAccount;
using Theater.Contracts;
using Theater.Contracts.Theater.PiecesTicket;
using Theater.Controllers.Base;

namespace Theater.Controllers.Admin;

[Route("api/admin/piece/{pieceId:guid}/ticket")]
[SwaggerTag("Админ. Методы для работы с билетами пьес")]
public sealed class PieceTicketAdminController : CrudServiceBaseController<PiecesTicketParameters>
{
    private readonly IPieceTicketService _pieceTicketService;

    public PieceTicketAdminController(
        IPieceTicketService pieceTicketService, 
        IMapper mapper,
        IUserAccountService userAccountService
        ) : base(pieceTicketService, mapper, userAccountService)
    {
        _pieceTicketService = pieceTicketService;
    }

    /// <summary>
    /// Добавить билеты для пьесы
    /// </summary>
    /// <response code="200">В случае успешного запроса</response>
    /// <response code="400">В случае ошибок валидации</response>
    // [Authorize(Policy = nameof(RoleModel.User.Policies.UserSearch))]
    [HttpPost]
    [ProducesResponseType(typeof(DocumentMeta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromRoute] Guid pieceId,
        [FromBody] PieceTicketCreateParameters parameters)
    {
        var result = await _pieceTicketService.CreateTickets(pieceId, parameters);

        return RenderResult(result);
    }

    /// <summary>
    /// Обновить выбранные билеты для пьесы
    /// </summary>
    /// <response code="200">В случае успешного запроса</response>
    /// <response code="400">В случае ошибок валидации</response>
    // [Authorize(Policy = nameof(RoleModel.User.Policies.UserSearch))]
    [HttpPut]
    [ProducesResponseType(typeof(DocumentMeta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTickets(
        [FromRoute] Guid pieceId,
        [FromBody] PieceTicketUpdateParameters parameters)
    {
        var result = await _pieceTicketService.UpdateTickets(pieceId, parameters);

        return RenderResult(result);
    }
}