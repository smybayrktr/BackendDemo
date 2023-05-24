using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
	public static class ServiceTool
	{
		//Amacımız program.cs deki Servis yapısına ulaşıp geri döndürmek
		public static IServiceProvider ServiceProvider { get; private set; }

		public static IServiceCollection Create(IServiceCollection services) {

			ServiceProvider = services.BuildServiceProvider();
			return services;

		} //program sc deki Servisimiz

	}
}

