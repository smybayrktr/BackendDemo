using System;
using Core.Utilities.Results;
using Entities;

namespace Business
{
    public interface IUserOperationClaimService
    {
        Task<IResult> Add(UserOperationClaim userOperationClaim);
        Task<IResult> Update(UserOperationClaim userOperationClaim);
        Task<IResult> Delete(UserOperationClaim userOperationClaim);
        Task<IDataResult<List<UserOperationClaim>>> GetList();
        Task<IDataResult<UserOperationClaim>> GetById(int id);
    }
}

