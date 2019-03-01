using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Services;
using CoreDemoModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;
        public HomeController(ICinemaService cinemaService,IMovieService movieService)
        {
            _cinemaService = cinemaService;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "电影院";
            return View(await _cinemaService.GetAllAsync());
        }

        public IActionResult Add()
        {
            ViewBag.Title = "添加电影院";
            return View(new Cinema());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _cinemaService.AddAsync(cinema);
                return RedirectToAction("Index");
            }
            return View(cinema);
        }

        public async Task<IActionResult> Edit(int cinemaId)
        {
            ViewBag.Title = "修改电影院";
            return View(await _cinemaService.GetByIdAsync(cinemaId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cinema  cinema)
        {
            if (ModelState.IsValid)
            {
                var exist = await _cinemaService.GetByIdAsync(cinema.Id);
                if (exist==null)
                {
                    return NotFound();
                }
                exist.Name = cinema.Name;
                exist.Location = cinema.Location;
                exist.Capacity = cinema.Capacity;
                return RedirectToAction("Index", "Home");
            }
            return View(cinema);
        }

        public async Task<IActionResult> Del(int cinemaId)
        {
            IEnumerable<Movie> movies = await _movieService.GetByCinemaAsync(cinemaId);
            await _movieService.DelMovies(movies);
            await _cinemaService.DelAsync(cinemaId);
            return RedirectToAction("Index", "Home");
        }


    }
}