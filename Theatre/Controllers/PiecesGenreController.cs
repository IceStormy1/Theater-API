﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Theater.Abstractions.PieceGenre;
using Theater.Contracts;
using Theater.Contracts.Theater.PiecesGenre;
using Theater.Contracts.Theater.WorkersPosition;
using Theater.Controllers.BaseControllers;

namespace Theater.Controllers;

[SwaggerTag("Пользовательские методы для работы с жанрами")]
[Route("api/genre")]
public sealed class PiecesGenreController : CrudServiceBaseController<PiecesGenreParameters>
{
    private readonly IPieceGenreService _pieceGenreService;

    public PiecesGenreController(IPieceGenreService pieceGenreService, IMapper mapper) : base(pieceGenreService, mapper)
    {
        _pieceGenreService = pieceGenreService;
    }

    /// <summary>
    /// Возвращает все жанры пьес
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(DocumentCollection<PiecesGenreModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPieceTicketsByDate()
    {
        var tickets = await _pieceGenreService.GetAllGenres();

        return Ok(new DocumentCollection<PiecesGenreModel>(tickets));
    }
}