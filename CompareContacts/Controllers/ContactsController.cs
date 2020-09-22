using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompareContacts.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CompareContacts.Controllers
{
    public class CompareController : Controller
    {
        [HttpGet]
        public IActionResult Index(List<Contact> contacts = null)
        {
            contacts = contacts == null ? new List<Contact>() : contacts;
            return View(contacts);
        }
        [Route("save")]
        [HttpPost]
        public IActionResult Save(IFormFile [] files)
        {
            foreach (IFormFile file in files)
            {
                string fileName = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/files", file.FileName);
                var stream = new FileStream(fileName, FileMode.Create);
                file.CopyToAsync(stream);
            }
               
            return View("Success");
        }

        private List<Contact> GetContactList(string fName)
        {
            List<Contact> contacts = new List<Contact>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Contact()
                        {
                            Name = reader.GetValue(0).ToString(),
                            Title = reader.GetValue(1).ToString(),
                            Email = reader.GetValue(2).ToString(),
                            Region = reader.GetValue(3).ToString(),
                            Office = reader.GetValue(4).ToString(),


                        });
                    }
                }
            
            }
            return contacts;
        }
        
    }
}
