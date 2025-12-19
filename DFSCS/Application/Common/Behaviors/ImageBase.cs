using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class ImageBase
    {
        public static string ImageToBase64(string imagePath)
        {
            string  filepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath); 
            byte[] imageBytes = File.ReadAllBytes(filepath);
            return Convert.ToBase64String(imageBytes);
        }
    }
}
