using System;
using Core.DataAccess.EntityFramework;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DemoContext>, IUserDal
    {
        public async Task<List<OperationClaim>> GetUserOperatinonClaims(int userId)
        {
            using (var context = new DemoContext())
            {
                var result = from userOperationClaim in context.UserOperationClaims.Where(p => p.UserId == userId)
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return await result.OrderBy(p => p.Name).ToListAsync();
            }
        }
    }
}

