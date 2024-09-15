using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Domain.Domain;
using PetAdoption.Domain.DTO;
using PetAdoption.Repository;
using PetAdoption.Service.Interface;

namespace PetAdoption.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdoptApplicationService _adoptApplicationService;
        private readonly ApplicationDbContext _context;
        public AdminController(IAdoptApplicationService adoptService, ApplicationDbContext context)
        {
            _adoptApplicationService = adoptService;
            _context = context;
        }

        [HttpGet]
        public List<AdoptApplication> Index()
        {
            return _adoptApplicationService.GetAllApplications();
        }

        [HttpGet("[action]")]
        public List<AdoptApplication> GetAllApplications()
        {
            return _adoptApplicationService.GetAllApplications();
        }

        [HttpPost("[action]")]
        public AdoptApplication GetDetailsForApplication(BaseEntity model)
        {
            return _adoptApplicationService.GetDetailsForApplication(model);
        }

        //This API endpoint receives a list of shelters and attempts to import them into the system. If a shelter already exists,
        //it skips that shelter. Otherwise, it creates a new shelter.
        [HttpPost("[action]")]
        public bool ImportAllShelters(List<ShelterDTO> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                try
                {
                    var shelterCheck = _context.Set<Shelter>().FirstOrDefault(m => m.ShelterName == item.ShelterName);
                    if (shelterCheck == null)
                    {
                        var shelter = new Shelter
                        {
                            ShelterName = item.ShelterName,
                            ShelterAdress = item.ShelterAdress,
                            ShelterImage = item.ShelterImage,
                            Rating = item.Rating,
                        };

                        _context.Add(shelter);
                        _context.SaveChanges();

                    }
                    else
                    {
                        continue;
                    }
                }
                catch { status = false; }
            }
            return status;
        }
    }
}
