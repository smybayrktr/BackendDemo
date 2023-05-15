using System;
using Core.DataAccess.EntityFramework;
using Entities;

namespace DataAccess.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DemoContext> , IUserDal
    {
        
    }
}

