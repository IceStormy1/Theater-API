﻿using Theater.Common;

namespace Theater.Abstractions.UserAccount.Models
{
    public static class UserAccountErrors
    {
        /// <summary>
        /// Пользователь не найден в системе
        /// </summary>
        public static WriteResult NotFound =>
            WriteResult.FromError(ErrorModel.NotFound("account/not-found", "Пользователь не найден в системе"));

        /// <summary>
        /// Для выполнения операции необходимо авторизоваться
        /// </summary>
        public static WriteResult Unauthorized =>
            WriteResult.FromError(ErrorModel.Unauthorized("account/not-found", "Для выполнения операции необходимо авторизоваться"));

        /// <summary>
        /// Указанный пользователь уже существует
        /// </summary>
        public static WriteResult UserAlreadyExist =>
            WriteResult.FromError(ErrorModel.Default("account/user-already-exist", "Указанный пользователь уже существует"));

        /// <summary>
        /// На балансе недостаточно средств
        /// </summary>
        public static WriteResult NotEnoughMoney =>
            WriteResult.FromError(ErrorModel.Default("account/not-enough-money", "На балансе недостаточно средств"));
    }
}