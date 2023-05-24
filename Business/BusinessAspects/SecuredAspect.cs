using System;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects
{
	public class SecuredAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        //İstek yapıyoruz ya her istek için bir thread oluşur.

        public SecuredAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }


        //Rolleri veriyoruz
        public SecuredAspect(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //Injection alt yapımızı okur

        }

        //Kullanıcının rollerini gezer claim içinde ilgili rol varsa metotu çalıştırmaya devam et. 
        protected override void OnBefore(IInvocation invocation)
        {
            if (_roles != null)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role))
                    {
                        return;
                    }
                }
                throw new Exception(Messages.AuthorizationDenied);
            }
            else
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims;
                if (claims.Count()>0)
                {
                    return;
                }
                throw new Exception(Messages.AuthorizationDenied);
            }
        }
    }
}

