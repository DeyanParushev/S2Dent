using Microsoft.AspNetCore.Mvc;
using S2Dent.Services.Interfaces;
using S2Dent.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace S2Dent.MVC.Controllers
{
    public class NewsController : Controller
    {
        private INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public async Task<IActionResult> All()
        {
            var news = await this.newsService.GetAllNews<NewsViewModel>();

            return this.View(news);
        }
    }
}
