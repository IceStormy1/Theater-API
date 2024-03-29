﻿using System;
using FluentValidation;
using Theater.Contracts;
using Theater.Contracts.Theater.TheaterWorker;
using Theater.Validation.UserAccount;

namespace Theater.Validation.Theater;

public sealed class TheaterWorkerParametersValidator : AbstractValidator<TheaterWorkerParameters>
{
    /// <param name="userValidator"><see cref="UserValidator"/></param>
    public TheaterWorkerParametersValidator(IValidator<IUser> userValidator)
    {
        Include(userValidator);

        RuleFor(worker => worker.PositionId)
            .NotEqual(Guid.Empty)
            .WithMessage("Указана некорректная должность");

        RuleFor(worker => worker.Description)
            .Description("Описание работника театра");
    }
}