﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theater.Abstractions.Authorization.Models;
using Theater.Common;
using Theater.Entities.Authorization;

namespace Theater.Abstractions.UserAccount;

public interface IUserAccountRepository : ICrudRepository<UserEntity>
{
    /// <summary>
    /// Получить пользователя по его никнейму
    /// </summary>
    /// <param name="userName">Никнейм пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    Task<UserEntity> FindUser(string userName, string password);

    /// <summary>
    /// Получить список всех пользователей 
    /// </summary>
    /// <remarks>Возвращает первые 300 пользователей отсортированные по никнейму</remarks>
    /// <returns>Список пользователей</returns>
    /// TODO: Добавить параметры фильтрации (пейджинация)
    Task<IReadOnlyCollection<UserEntity>> GetUsers();

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="userEntity">Данные пользователя</param>
    Task<WriteResult<CreateUserResult>> CreateUser(UserEntity userEntity);

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="userEntity">Данные пользователя</param>
    Task<WriteResult> UpdateUser(UserEntity userEntity);

    /// <summary>
    /// Пополнить баланс пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="replenishmentAmount">Сумма пополнения</param>
    Task<WriteResult> ReplenishBalance(Guid userId, decimal replenishmentAmount);
}