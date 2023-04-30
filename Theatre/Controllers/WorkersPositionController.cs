﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using Theater.Abstractions.WorkersPosition;
using Theater.Common;
using Theater.Contracts;
using Theater.Contracts.Theater;
using Theater.Controllers.BaseControllers;
using Theater.Entities.Theater;

namespace Theater.Controllers
{
    [Route("api")]
    [SwaggerTag("Пользовательские методы для работы с должностями работников театра")]
    public sealed class WorkersPositionController : CrudServiceBaseController<WorkersPositionParameters, WorkersPositionEntity>
    {
        private readonly IWorkersPositionService _workersPositionService;

        public WorkersPositionController(
            IWorkersPositionService workersPositionService,
            IMapper mapper) : base(workersPositionService, mapper)
        {
            _workersPositionService = workersPositionService;
        }

        /// <summary>
        /// Возвращает все должности работников театра
        /// </summary>
        /// <param name="positionType">
        /// Тип должности. Опциональный параметр
        /// </param>
        [HttpGet("positions")]
        [ProducesResponseType(typeof(DocumentCollection<WorkersPositionModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPieceTicketsByDate([FromQuery] PositionType? positionType)
        {
            var tickets = await _workersPositionService.GetWorkerPositions(positionType);

            return Ok(new DocumentCollection<WorkersPositionModel>(tickets));
        }
    }
}