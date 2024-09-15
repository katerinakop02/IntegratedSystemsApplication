using PetAdoptionAdminApplication.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PetAdoptionAdminApplication.Controllers
{
    public class ShelterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //reads an Excel file and extracts shelters information from it, returning a list of shelters.
        private List<Shelter> getAllSheltersFromFile(string fileName)
        {
            List<Shelter> shelters = new List<Shelter>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        shelters.Add(new Models.Shelter
                        {
                            ShelterName = reader.GetValue(0).ToString(),
                            ShelterAdress = reader.GetValue(1).ToString(),
                            ShelterImage = reader.GetValue(2).ToString(),
                            Rating = (double)reader.GetValue(3),
                        });
                    }
                }
            }
            return shelters;
        }

        //handles the upload of a file containing movie data, processes the file to extract movie information,
        //and then sends this information to an API endpoint to import the movies into the system.
        public IActionResult ImportShelters(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Shelter> shelters = getAllSheltersFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5189/api/Admin/ImportAllShelters";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(shelters), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "Adopt");
        }
}
}
