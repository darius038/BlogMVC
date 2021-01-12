using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Threading.Tasks;

namespace BlogMVC.Repository.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Paths:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public bool RemoveImage(string image)
        {
            try
            {
                var file = Path.Combine(_imagePath, image);
                if (File.Exists(file))
                    File.Delete(file);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Saving uploaded image for local storage - returns saved image name
        public async Task<string> SaveImage(IFormFile image) //handle image null
        {
            try
            {
                var imagePath = Path.Combine(_imagePath);

                //Check if image path exist , if not - creat, to avoid errors
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                //Creating new image file name
                var fileType = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{fileType}";

                //Area for temporrary fileStream opbject
                using (var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Image save Error";
            }            
        }
    }
}
