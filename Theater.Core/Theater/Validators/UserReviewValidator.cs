﻿using System;
using System.Threading.Tasks;
using Theater.Abstractions;
using Theater.Abstractions.Authorization.Models;
using Theater.Abstractions.Errors;
using Theater.Abstractions.UserAccount;
using Theater.Common;
using Theater.Contracts.Theater;
using Theater.Entities.Theater;

namespace Theater.Core.Theater.Validators
{
    public sealed class UserReviewValidator : IDocumentValidator<UserReviewParameters>
    {
        private readonly ICrudRepository<UserReviewEntity> _userReviewRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserReviewValidator(
            ICrudRepository<UserReviewEntity> userReviewRepository,
            IUserAccountRepository userAccountRepository)
        {
            _userReviewRepository = userReviewRepository;
            _userAccountRepository = userAccountRepository;
        }

        public async Task<WriteResult> CheckIfCanCreate(UserReviewParameters parameters, Guid? userId = null)
        {
            //TODO: Валидация на существование пьесы
            return WriteResult.Successful;
        }

        public async Task<WriteResult> CheckIfCanUpdate(Guid entityId, UserReviewParameters parameters, Guid? userId = null)
        {
            //TODO: Валидация на существование пьесы
            return WriteResult.Successful;
        }

        public async Task<WriteResult> CheckIfCanDelete(Guid entityId, Guid? userId = null)
        {
            if (!userId.HasValue)
                return UserAccountErrors.Unauthorized;

            var userReviewEntity = await _userReviewRepository.GetByEntityId(entityId);
            var userEntity = await _userAccountRepository.GetByEntityId(userId.Value);

            if(userReviewEntity.UserId != userId.Value && userEntity.RoleId != (int)UserRole.Admin)
                return UserAccountErrors.InsufficientRights;

            return WriteResult.Successful;
        }
    }
}