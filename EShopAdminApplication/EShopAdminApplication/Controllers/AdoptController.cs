using ClosedXML.Excel;
using PetAdoptionAdminApplication.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System.Text;

namespace PetAdoptionAdminApplication.Controllers
{
    public class AdoptController : Controller
    {
        public AdoptController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5189/api/Admin/GetAllApplications";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<AdoptApplication>>().Result;
            return View(data);
        }

        public IActionResult Details(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5189/api/Admin/GetDetailsForApplication";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<AdoptApplication>().Result;
            return View(data);
        }
        public FileContentResult CreateInvoice(Guid Id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5189/api/Admin/GetDetailsForApplication";
            var model = new
            {
                Id = Id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<AdoptApplication>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{ApplicationNumber}}", data.Id.ToString());
            document.Content.Replace("{{UserName}}", data.Owner.FirstName + " " + data.Owner.LastName);
            StringBuilder sb = new StringBuilder();
            var total = 0;
            foreach (var item in data.PetInAdoptApplications)
            {
                sb.Append(item.AdoptedPet.Shelter.ShelterName + " has quantity " + item.Quantity + " with price " + item.AdoptedPet.Price + "$");
                total += (int)(item.Quantity * item.AdoptedPet.Price);
            }
            document.Content.Replace("{{PetsList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "$");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportedInvoice.pdf");

        }
        [HttpGet]
        public FileContentResult ExportAllShelters()
        {
            string fileName = "Shelters.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Applications");
                worksheet.Cell(1, 1).Value = "OrderID";
                worksheet.Cell(1, 2).Value = "Customer UserName";
                worksheet.Cell(1, 3).Value = "Total Price";
                HttpClient client = new HttpClient();
                string URL = "http://localhost:5189/api/Admin/GetAllApplications";

                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<AdoptApplication>>().Result;

                for (int i = 0; i < data.Count(); i++)
                {
                    var item = data[i];
                    worksheet.Cell(i + 2, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.Owner.FirstName;
                    var total = 0.0;
                    for (int j = 0; j < item.PetInAdoptApplications.Count(); j++)
                    {
                        worksheet.Cell(1, 4 + j).Value = "Product - " + (j + 1);
                        worksheet.Cell(i + 2, 4 + j).Value = item.PetInAdoptApplications.ElementAt(j).AdoptedPet.Shelter.ShelterName;
                        total += (item.PetInAdoptApplications.ElementAt(j).Quantity * item.PetInAdoptApplications.ElementAt(j).AdoptedPet.Price);
                    }
                    worksheet.Cell(i + 2, 3).Value = total;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
    }
}
