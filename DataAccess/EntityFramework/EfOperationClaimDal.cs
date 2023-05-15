using System;
using Core.DataAccess.EntityFramework;
using Entities;

namespace DataAccess.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, DemoContext>, IOperationClaimDal
    {
        
    }
}

