using CompareContacts.Models;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompareContacts.Service
{
    public class CompareTables : IEqualityComparer<Contact>
    {
        public static List<Contact> CompareExcelFiles()
        {
            List<Contact> excelSheet1 = new List<Contact>();
            List<Contact> excelSheet2 = new List<Contact>();

            var fileOne = @"C:\Users\Erins Laptop\source\repos\CompareContacts\CompareContacts\wwwroot\files\CompanyContacts.xlsx";
            var fileTwo = @"C:\Users\Erins Laptop\source\repos\CompareContacts\CompareContacts\wwwroot\files\Company2Contacts.xlsx";

            excelSheet1 = ConvertToList(fileOne);
            excelSheet2 = ConvertToList(fileTwo);

            //List<Contact> duplicates = excelSheet1.Intersect(excelSheet2).ToList();
            //List<Contact> duplicates = excelSheet1.Where(i => excelSheet2.Contains(i)).ToList();

            List<Contact> duplicates = excelSheet1.Intersect(excelSheet2, new CompareTables()).ToList();

            return duplicates;

        }

        public static List<Contact> ConvertToList(string filePath)
        {

            List<Contact> contacts = new List<Contact>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each row of the file
                    {
                        contacts.Add(new Contact
                        {
                            Name = reader.GetValue(0).ToString(),
                            Title = reader.GetValue(1).ToString(),
                            Email = reader.GetValue(2).ToString(),
                            Region = reader.GetValue(3).ToString(),
                            Office = reader.GetValue(4).ToString()
                        }) ;
                    }
                }
            }

            return contacts;
        }

        public bool Equals(Contact x, Contact y)
        {
            if (string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(x.Title, y.Title, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(x.Email, y.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public int GetHashCode(Contact obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
