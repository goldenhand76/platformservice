using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePlatform(Platform plat)
        {
            if(plat== null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _context.Platforms.Add(plat);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}