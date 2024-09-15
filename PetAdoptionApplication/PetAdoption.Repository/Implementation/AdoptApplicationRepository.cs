using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.Domain;
using PetAdoption.Repository.Interface;

namespace PetAdoption.Repository.Implementation
{
    public class AdoptApplicationRepository : IAdoptApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<AdoptApplication> _adoptApplications;

        public AdoptApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
            _adoptApplications = context.Set<AdoptApplication>();
        }

        public List<AdoptApplication> GetAllApplications()
        {
            return _adoptApplications
                .Include(z => z.PetInAdoptApplications)
                .Include(z => z.Owner)
                .Include("PetInAdoptApplications.AdoptedPet")
                .Include("PetInAdoptApplications.AdoptedPet.Shelter")
                .ToList();
        }

        public AdoptApplication GetDetailsForApplication(BaseEntity model)
        {
            return _adoptApplications
                 .Include(z => z.PetInAdoptApplications)
                    .ThenInclude(z => z.AdoptedPet)
                      .ThenInclude(z => z.Shelter)
                 .Include(z => z.Owner)
                 .Include("PetInAdoptApplications.AdoptedPet")
                 .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
