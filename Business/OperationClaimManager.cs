using System;
using DataAccess;
using Entities;

namespace Business
{
	public class OperationClaimManager: IOperationClaimService
	{
        private readonly IOperationClaimDal _operationClaimDal;

		public OperationClaimManager(IOperationClaimDal operationClaimDal)
		{
            _operationClaimDal = operationClaimDal;
		}

        public void Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
        }
    }
}

