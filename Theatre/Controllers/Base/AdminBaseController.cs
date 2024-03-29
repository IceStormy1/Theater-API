﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Theater.Abstractions;
using Theater.Abstractions.UserAccount;
using Theater.Attributes;
using Theater.Contracts;

namespace Theater.Controllers.Base;

/// <summary>
/// Базовый контроллер в админке с реализацией CRUD. Путь по умолчанию: <c>api/admin</c>.
/// </summary>
[RoleAuthorize(roles: nameof(UserRole.Admin))]
[Route("api/admin")] 
[ApiController]
public class AdminBaseController<TParameters> : CrudServiceBaseController<TParameters>
    where TParameters : class
{
    public AdminBaseController(
        ICrudService<TParameters> service,
        IMapper mapper,
        IUserAccountService userAccountService
        ) : base(service, mapper, userAccountService)
    {
    }

    /// <summary>
    /// Создать сущность
    /// </summary>
    /// <response code="200">В случае успешного запроса</response>
    /// <response code="400">В случае ошибок валидации</response>
    // [Authorize(Policy = nameof(RoleModel.User.Policies.UserSearch))]
    [HttpPost]
    [ProducesResponseType(typeof(DocumentMeta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Create([FromBody] TParameters parameters)
        => CreateOrUpdate(parameters);

    /// <summary>
    /// Обновить сущность
    /// </summary>
    /// <response code="200">В случае успешного запроса</response>
    /// <response code="400">В случае ошибок валидации</response>
    // [Authorize(Policy = nameof(RoleModel.User.Policies.UserSearch))]
    [HttpPut("{entityId:guid}")]
    [ProducesResponseType(typeof(DocumentMeta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Update([FromBody] TParameters parameters, [FromRoute] Guid entityId)
        => CreateOrUpdate(parameters, entityId);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <response code="200">В случае успешного запроса</response>
    /// <response code="400">В случае ошибок валидации</response>
    // [Authorize(Policy = nameof(RoleModel.User.Policies.UserSearch))]
    [HttpDelete("{entityId:guid}")]
    [ProducesResponseType(typeof(DocumentMeta), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid entityId)
    {
        var deletePieceResult = await Service.Delete(entityId);

        return RenderResult(deletePieceResult);
    }

    /// <summary>
    /// Обновить или создать сущность
    /// </summary>
    /// <param name="parameters">Параметры</param>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <remarks>
    /// Идентификатор <paramref name="entityId"/> указывается при обновлении сущности
    /// </remarks>
    private async Task<IActionResult> CreateOrUpdate(TParameters parameters, Guid? entityId = null)
    {
        var piecesShortInformation = await Service.CreateOrUpdate(parameters, entityId);

        return RenderResult(piecesShortInformation);
    }
}