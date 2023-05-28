using System;
using Core.DataAccess.EntityFramework;
using Entities;

namespace DataAccess.EntityFramework
{
    public class EfEmailParameterDal : EfEntityRepositoryBase<EmailParameter, DemoContext>, IEmailParameterDal
    {
    }
}

