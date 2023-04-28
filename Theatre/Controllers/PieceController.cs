﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Abstractions;
using Theater.Abstractions.Filter;
using Theater.Abstractions.Piece;
using Theater.Contracts;
using Theater.Contracts.Filters;
using Theater.Contracts.Theater;
using Theater.Entities.Theater;
using Theater.Controllers.BaseControllers;

namespace Theater.Controllers;

[ApiController]
public sealed class PieceController : CrudServiceBaseController<PieceParameters, PieceEntity>
{
    private readonly IPieceService _pieceService;
    private readonly IIndexReader<PieceEntity, PieceFilterSettings> _pieceIndexReader;

    public PieceController(
        IPieceService service, 
        IMapper mapper,
        IIndexReader<PieceEntity, PieceFilterSettings> pieceIndexReader) : base(service, mapper)
    {
        _pieceService = service;
        _pieceIndexReader = pieceIndexReader;
    }

    /// <summary>
    /// Получить полную информацию о пьесе по идентификатору
    /// </summary>
    /// <param name="pieceId">Идентификатор пьесы</param>
    /// <response code="200">В случае успешной регистрации</response>
    [HttpGet("{pieceId:guid}")]
    [ProducesResponseType(typeof(PieceModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPieceById([FromRoute] Guid pieceId)
    {
        var piecesResult = await _pieceService.GetPieceById(pieceId);

        return RenderResult(piecesResult);
    }

    /// <summary>
    /// Получить краткую информацию об актуальных пьесах
    /// </summary>
    /// <remarks>
    /// Доступна сортировка по полям:
    /// * name
    /// * genre
    /// </remarks>
    /// <response code="200">В случае успешного запроса</response>
    [HttpGet]
    [ProducesResponseType(typeof(Page<PieceShortInformationModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPiecesShortInformation([FromQuery] PieceFilterParameters filterParameters)
    {
        var pieceFilterSettings = Mapper.Map<PieceFilterSettings>(filterParameters);

        var piecesShortInformation = await _pieceIndexReader.QueryItems(pieceFilterSettings);

        return Ok(Mapper.Map<Page<PieceShortInformationModel>>(piecesShortInformation));
    }
}