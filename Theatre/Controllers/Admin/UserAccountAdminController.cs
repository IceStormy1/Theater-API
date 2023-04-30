﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Theater.Abstractions;
using Theater.Abstractions.Filter;
using Theater.Abstractions.UserAccount;
using Theater.Contracts;
using Theater.Contracts.Filters;
using Theater.Contracts.UserAccount;
using Theater.Controllers.BaseControllers;
using Theater.Entities.Authorization;

namespace Theater.Controllers.Admin
{
    [Route("api/admin/user")]
    [SwaggerTag("Админ. Методы для работы с пользователями")]
    public class UserAccountAdminController : AdminBaseController<UserParameters, UserEntity>
    {
        private readonly IIndexReader<UserModel, UserEntity, UserAccountFilterSettings> _userIndexReader;
        public UserAccountAdminController(
            IUserAccountService service,
            IMapper mapper,
            IIndexReader<UserModel, UserEntity, UserAccountFilterSettings> userIndexReader) : base(service, mapper)
        {
            _userIndexReader = userIndexReader;
        }

        /// <summary>
        /// Возвращает пользователей по параметрам запроса
        /// </summary>
        ///<remarks>
        /// Доступна сортировка по полям:
        /// * firstname
        /// * middlename
        /// * lastname
        /// </remarks>
        [HttpGet("parameters")]
        [ProducesResponseType(typeof(Page<UserShortItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersByParameters([FromQuery] UserAccountFilterParameters filterParameters)
        {
            var filterSettings = Mapper.Map<UserAccountFilterSettings>(filterParameters);

            var users = await _userIndexReader.QueryItems(filterSettings);

            return Ok(Mapper.Map<Page<UserShortItem>>(users));
        }
    }
}