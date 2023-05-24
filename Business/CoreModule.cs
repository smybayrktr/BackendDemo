using System;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
	public class CoreModule: ICoreModule
	{
	
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}

