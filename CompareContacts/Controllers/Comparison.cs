using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CompareContacts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompareContacts.Controllers
{
    public class Comparison : Controller
    {
        public IActionResult Index()
        {
            List<Contact> duplicates = CompareContacts.Service.CompareTables.CompareExcelFiles();
            return View(duplicates);
        }
    }
}
