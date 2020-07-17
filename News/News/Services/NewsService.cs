using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using News.Models;
using Newtonsoft.Json;

namespace News.Services
{
	public interface INewsService
	{
		Task<NewsResult> GetNews(NewsScope scope);
	}

	public class NewsService : INewsService
	{
		private string Headlines =>
			"https://newsapi.org/v2/top-headlines?" +
			"country=us&" +
			$"apiKey={Settings.NewsApiKey}";
		private string Local =>
			"https://newsapi.org/v2/everything?q=local&" +
			$"apiKey={Settings.NewsApiKey}";
		private string Global =>
			"https://newsapi.org/v2/everything?q=global&" +
			$"apiKey={Settings.NewsApiKey}";

		public async Task<NewsResult> GetNews(NewsScope scope)
		{
			var url = GetUrl(scope);

			var webClient = new WebClient();
			var json = await webClient.DownloadStringTaskAsync(url);

			return JsonConvert.DeserializeObject<NewsResult>(json);
		}

		private string GetUrl(NewsScope scope)
		{
			return scope switch
			{
				NewsScope.Headlines => Headlines,
				NewsScope.Local => Local,
				NewsScope.Global => Global,
				_ => throw new InvalidOperationException($"Undefined scope: {scope}")
			};
		}
	}
}
