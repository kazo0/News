using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using Autofac;
using News.Services;
using News.ViewModels;
using Xamarin.Forms;

namespace News
{
	public static class Bootstrapper
	{
		public static void Initialize()
		{
			var containerBuilder = new ContainerBuilder();

			containerBuilder.RegisterType<NewsService>().As<INewsService>();
			containerBuilder.RegisterType<MainShell>();
			containerBuilder.RegisterAssemblyTypes(typeof(App).Assembly)
				.Where(x => x.IsSubclassOf(typeof(ViewModel)));

			var container = containerBuilder.Build();
			Resolver.Initialize(container);
		}
	}
}
