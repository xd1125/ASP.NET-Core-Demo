using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Services;
using CoreDemoModels;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;

        public MovieController(IMovieService movieService,
            ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index(int cinemaId)
        {
            var cinema = await _cinemaService.GetByIdAsync(cinemaId);
            ViewBag.Title = $"{cinema.Name}电影院上映的电影有：";
            ViewBag.CinemaId = cinemaId;
            return View( await _movieService.GetByCinemaAsync(cinemaId));
        }

        public IActionResult Add(int cinemaId)
        {
            ViewBag.Title = "添加电影";
            ViewBag.CinemaId = cinemaId;
            return View(new Movie() { CinemaId = cinemaId });
        }

        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddAsync(movie);
                return RedirectToAction("Index", new { cinemaId = movie.CinemaId });
            }
            return View(movie);
        }

        public async Task<IActionResult> Watch(int cinemaId, int movieId)
        {
            ViewBag.Title = "电影详情";
            return View(await _movieService.GetById(cinemaId,movieId));
        }

        public async Task<IActionResult> Edit(int cinemaId,int movieId)
        {
            var movie = await _movieService.GetById(cinemaId, movieId);
            return View(movie);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var exist = await _movieService.GetById(movie.CinemaId, movie.Id);
                if (exist==null)
                {
                    return NotFound();
                }
                exist.Name = movie.Name;
                exist.Starring = movie.Starring;
                exist.ReleaseDate = movie.ReleaseDate;
                return RedirectToAction("Index", "Movie", new { cinemaId = movie.CinemaId });
            }
            return View(movie);
        }

        public async Task<IActionResult> Del(int cinemaId,int movieId)
        {
            var movie = await _movieService.GetById(cinemaId, movieId);
            await _movieService.DelMovies(new List<Movie>() { movie });
            return RedirectToAction("Index", "Movie", new { cinemaId });
        }
    }
}