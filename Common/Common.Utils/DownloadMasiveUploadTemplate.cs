using System.IO;
using System.Net;
using Common.Entities;

namespace Common.Utils
{
    public static class DownloadMasiveUploadTemplate
    {
        public static string Download<T>()
        {
            return null;
            /*var className = typeof(T).Name;
            var countryClassName = nameof(Country);
            var listClassName = nameof(List);

            if (className == countryClassName)
            {
                var fileName = "uploadCountryTemplate.xlsx";
                var path = getFilePath(fileName);
                var fileByte = WebRequestMethods.File.ReadAllBytes(path);
                File(fileByte, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                File.Create(fileName,  fileName);
                
                
                return path;
            }
            else if (className == listClassName)
            {
                var fileName = "uploadListTemplate.xlsx";
                var path = getFilePath("uploadListTemplate");
                return path;
            }

            return null;*/
        }

        private static string getFilePath(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Templates/{fileName}");
            return path;
        }
    }
}