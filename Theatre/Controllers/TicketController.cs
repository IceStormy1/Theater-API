﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Abstractions.Ticket;
using Theater.Abstractions.Errors;
using Swashbuckle.AspNetCore.Annotations;
using Theater.Contracts.Theater.PiecesTicket;
using Theater.Controllers.Base;
using Theater.Abstractions.UserAccount;

namespace Theater.Controllers;

[ApiController]
[SwaggerTag("Пользовательские методы для работы с билетами пьес")]
public sealed class TicketController : CrudServiceBaseController<PiecesTicketParameters>
{
    private readonly IPieceTicketService _pieceTicketService;

    public TicketController(
        IPieceTicketService service, 
        IMapper mapper,
        IUserAccountService userAccountService) 
        : base(service, mapper, userAccountService)
    {
        _pieceTicketService = service;
    }

    /// <summary>
    /// Получить билеты указанной пьесы по идентификатору даты пьесы 
    /// </summary>
    /// <param name="pieceId">Идентификатор пьесы</param>
    /// <param name="dateId">Идентификатор даты пьесы</param>
    /// <response code="200">В случае успешной регистрации</response>
    [HttpGet("{pieceId:guid}/date/{dateId:guid}")]
    [ProducesResponseType(typeof(PieceTicketList), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPieceTicketsByDate([FromRoute] Guid pieceId, [FromRoute] Guid dateId)
    {
        var tickets = await _pieceTicketService.GetPieceTicketsByDate(pieceId, dateId);

        return Ok(new PieceTicketList(tickets));
    }

    /// <summary>
    /// Купить билет 
    /// </summary>
    /// <param name="ticketBuyRequest">Тело запроса для покупки билетов</param>
    /// <response code="200">В случае успешной регистрации</response>
    /// <response code="400">В случае ошибок валидации</response>
    [Authorize]
    [HttpPost("buy")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuyTickets([FromBody] PieceTicketBuyRequest ticketBuyRequest)
    {
        var innerUserId = await GetUserId();

        if (!innerUserId.HasValue)
            return RenderResult(UserAccountErrors.Unauthorized);

        var buyTicketResult = await _pieceTicketService.BuyTickets(ticketBuyRequest.TicketIds, innerUserId.Value);

        return RenderResult(buyTicketResult);
    }

    /// <summary>
    /// Забронировать билет
    /// </summary>
    /// <param name="ticketBuyRequest">Тело запроса для бронирования билетов</param>
    /// <response code="200">В случае успешной регистрации</response>
    /// <response code="400">В случае ошибок валидации</response>
    [Authorize]
    [HttpPost("book")]
    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BookTickets([FromBody] PieceTicketBuyRequest ticketBuyRequest)
    {
        var innerUserId = await GetUserId();

        if (!innerUserId.HasValue)
            return RenderResult(UserAccountErrors.Unauthorized);

        var buyTicketResult = await _pieceTicketService.BookTicket(ticketBuyRequest.TicketIds, innerUserId.Value);

        return RenderResult(buyTicketResult);
    }
}