﻿using System;
using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetUserOperatinonClaims(int userId);
    }
}

