using System;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using S2Dent.Models;
using S2Dent.MVC.Areas.Identity;
using S2Dent.Services.Interfaces;
using S2Dent.ViewModels.InputModels;
using S2Dent.ViewModels.ViewModels;

namespace S2Dent.MVC.Controllers
{
    public class NewsController : Controller
    {
        private INewsService newsService;
        private readonly IMapper mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            this.newsService = newsService;
            this.mapper = mapper;
        }

        [Route("/News")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var news = await this.newsService.GetAllNews<NewsViewModel>();

                return this.View(news);
            }
            catch (Exception ex)
            {
                return this.View("Error");
            }
        }

        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> All()
        {
            var news = await this.newsService.GetAllNews<NewsViewModel>();

            return this.View(news);
        }

        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Edit(NewsInputModel newsInput)
        {
            try
            {
                var newsItem = this.mapper.Map<News>(newsInput);
                await this.newsService.EditNews(newsItem);

                return this.Redirect(nameof(this.GetAll));
            }
            catch (Exception ex)
            {
                return this.View("Error");
            }
        }

        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Create()
        {
            var newsModel = new NewsInputModel();
            return this.View(newsModel);
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Create(NewsInputModel newsInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(newsInput);
            }

            try
            {
                var newsItem = this.mapper.Map<News>(newsInput);
                await this.newsService.Create(newsItem);

                return this.Redirect(nameof(this.GetAll));
            }
            catch (Exception ex)
            {
                return this.View("Error");
            }
        }
    }
}
