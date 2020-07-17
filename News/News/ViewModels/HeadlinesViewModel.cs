using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using News.Models;
using News.Services;
using Xamarin.Forms;

namespace News.ViewModels
{
	public class HeadlinesViewModel : ViewModel
	{
		private readonly INewsService _newsService;

		public NewsResult CurrentNews { get; set; }

		public ICommand ItemSelected => new Command(async (selectedItem) =>
		{
			var selectedArticle = selectedItem as Article;
			var url = HttpUtility.UrlEncode(selectedArticle.Url);
			await Navigation.NavigateTo($"articleview?url={url}");
		});

		public HeadlinesViewModel(INewsService newsService)
		{
			_newsService = newsService;
		}

		public async Task Initialize(string scope)
		{
			var resolvedScope = scope.ToLower() switch
			{
				"headlines" => NewsScope.Headlines,
				"local" => NewsScope.Local,
				"global" => NewsScope.Global,
				_ => NewsScope.Headlines,
			};

			await Initialize(resolvedScope);
		}

		public async Task Initialize(NewsScope scope)
		{
			CurrentNews = await _newsService.GetNews(scope);
		}
	}
}
