using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoModels;

namespace CoreDemo.Services.Impl
{
    public class CinemaMemoryService : ICinemaService
    {
        private readonly List<Cinema> _cinemas = new List<Cinema>();

        public CinemaMemoryService()
        {
            _cinemas.Add(new Cinema()
            {
                Id=GetMaxId(),
                Name = "City Cinema",
                Location = "Road abc , No.123",
                Capacity = 1000
            });
            _cinemas.Add(new Cinema()
            {
                Id = GetMaxId(),
                Name = "Fly Cinema",
                Location = "Road hello , No.1023",
                Capacity = 500
            });
        }

        public Task AddAsync(Cinema model)
        {
            var maxId = _cinemas.Max(x => x.Id);
            model.Id = maxId + 1;
            _cinemas.Add(model);
            return Task.CompletedTask;
        }

        public Task DelAsync(int id)
        {
            var find = _cinemas.FirstOrDefault(c => c.Id == id);
            if (find !=null)
            {
                _cinemas.Remove(find);
            }
            return Task.CompletedTask;
        }
      
        public Task<IEnumerable<Cinema>> GetAllAsync()
        {
            return Task.Run(() => _cinemas.OrderBy(c=>c.Id).AsEnumerable());
        }

        public Task<Cinema> GetByIdAsync(int id)
        {
            return Task.Run(() => _cinemas.FirstOrDefault(x => x.Id == id));
        }

        private int GetMaxId()
        {
            var maxId = 0;
            if (_cinemas.Count > 0)
            {
                maxId = _cinemas.Max(c => c.Id);
            }
            return maxId+1;
        }
    }
}
