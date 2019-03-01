using CoreDemoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Services
{
    public interface IMovieService
    {
        Task AddAsync(Movie model);
        Task<IEnumerable<Movie>> GetByCinemaAsync(int cinemaId);
        Task<Movie> GetById(int cinemaId,int movieId);
        Task DelMovies(IEnumerable<Movie> movies);
    }
}
