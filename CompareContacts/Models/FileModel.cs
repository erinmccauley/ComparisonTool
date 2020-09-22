using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareContacts.Models
{
    public class FileModel
    {
        public List<IFormFile> Files { get; set; }
    }
}
