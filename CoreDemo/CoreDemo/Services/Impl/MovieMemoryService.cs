using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoModels;

namespace CoreDemo.Services.Impl
{
    public class MovieMemoryService : IMovieService
    {
        private readonly List<Movie> _movies = new List<Movie>();

        public MovieMemoryService()
        {
            _movies.Add(new Movie()
            {
                CinemaId = 1,
                Id = 1,
                Name = "Superman",
                ReleaseDate = new DateTime(2019, 1, 1),
                Starring = "Nick"
            });
            _movies.Add(new Movie()
            {
                CinemaId = 1,
                Id = 2,
                Name = "Ghost",
                ReleaseDate = new DateTime(1995, 5, 21),
                Starring = "Mochael Jackson"
            });
            _movies.Add(new Movie()
            {
                CinemaId = 2,
                Id = 1,
                Name = "Fight",
                ReleaseDate = new DateTime(2018, 11, 11),
                Starring = "Tommy"
            });
        }

        public Task AddAsync(Movie model)
        {
            var maxId = 0;

            var curCinemaMovies = _movies.Where(m => m.CinemaId == model.CinemaId);
            if (curCinemaMovies.Count<Movie>()>0)
            {
                maxId = curCinemaMovies.Max(m => m.Id);
            }
            model.Id = maxId + 1;
            _movies.Add(model);
            return Task.CompletedTask;
        }

        public Task DelMovies(IEnumerable<Movie> movies)
        {
            Movie find;
            foreach (var movie in movies)
            {
                find = _movies.FirstOrDefault(m => m.CinemaId == movie.CinemaId && m.Id == movie.Id);
                if (find !=null)
                {
                    _movies.Remove(find);
                }
            }
            return Task.CompletedTask;
        }
        
        public Task<IEnumerable<Movie>> GetByCinemaAsync(int cinemaId)
        {
            return Task.Run(() => _movies.OrderBy(m=>m.Id).Where(m => m.CinemaId == cinemaId));
        }

        public Task<Movie> GetById(int cinemaId, int movieId)
        {
            return Task.Run(()=>_movies.FirstOrDefault(m => m.Id == movieId && m.CinemaId==cinemaId));
        }
    }
}
