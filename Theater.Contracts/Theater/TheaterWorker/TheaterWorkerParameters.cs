﻿using System;
using Theater.Contracts.FileStorage;
using Theater.Contracts.UserAccount;

namespace Theater.Contracts.Theater.TheaterWorker;

public class TheaterWorkerParameters : IUser
{
    /// <summary>
    /// Имя 
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия 
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public GenderType Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Описание работника
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор должности работника театра
    /// </summary>
    public Guid PositionId { get; set; }

    /// <summary>
    /// Основная фотография пьесы
    /// </summary>
    public StorageFileListItem MainPhoto { get; set; }
}