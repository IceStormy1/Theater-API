﻿using System;
using System.Threading.Tasks;
using Theater.Common;
using Theater.Contracts;
using Theater.Entities;

namespace Theater.Abstractions
{
    public interface ICrudService<TModel, TEntity> 
        where TEntity : class, IEntity 
        where TModel : class
    {
        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns></returns>
        Task<WriteResult<TModel>> GetById(Guid id);

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="model">Добавляемая/редактируемая сущность</param>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <remarks>
        /// Идентификатор<paramref name="entityId"/> заполняется при редактировании сущности
        /// </remarks>
        /// <returns></returns>
        Task<WriteResult<DocumentMeta>> CreateOrUpdate(TModel model, Guid? entityId);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности</param>
        /// <returns></returns>
        Task<WriteResult> Delete(Guid id);
    }
}