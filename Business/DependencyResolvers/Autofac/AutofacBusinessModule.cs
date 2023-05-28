using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Interceptors;
using Core.Utilities.Security.JWT;
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

            builder.RegisterType<EmailParameterManager>().As<IEmailParameterService>();
            builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();


            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();


            //Aspect kullanabilmemiz için yazarız.
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() //Autofac.Extras.DynamicProxy ile gelir
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

