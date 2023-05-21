using System;
using Core.Utilities.Results;
using Entities;

namespace Business
{
	public interface IUserOperationClaimService
	{
        IResult Add(UserOperationClaim userOperationClaim);

        IResult Update(UserOperationClaim userOperationClaim);

        IResult Delete(UserOperationClaim userOperationClaim);

        IDataResult<List<UserOperationClaim>> GetAll();

        IDataResult<UserOperationClaim> GetById(int Id);
    }
}

