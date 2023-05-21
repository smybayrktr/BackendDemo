using System;
using Core.Utilities.Results;
using Entities;

namespace Business
{
	public interface IOperationClaimService
	{
		IResult Add(OperationClaim operationClaim);

        IResult Update (OperationClaim operationClaim);

        IResult Delete (OperationClaim operationClaim);

        IDataResult<List<OperationClaim>> GetAll();

        IDataResult<OperationClaim> GetById(int Id);

    }
}

