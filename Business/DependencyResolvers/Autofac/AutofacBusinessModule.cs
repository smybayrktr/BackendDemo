using System;
using Autofac;
using DataAccess;
using DataAccess.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
	public class AutofacBusinessModule: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();


            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();


            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
        }
    }
}

