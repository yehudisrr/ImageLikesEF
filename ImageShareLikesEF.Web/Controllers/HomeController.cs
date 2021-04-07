using ImageShareLikesEF.Data;
using ImageShareLikesEF.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageShareLikesEF.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _environment;
        public HomeController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImagesRepository(connectionString);
            var vm = new ImagesViewModel
            {
                Images = repo.GetAll()
            };
            return View(vm);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(Image image, IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                imageFile.CopyTo(stream);
            }
            image.FileName = fileName;
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImagesRepository(connectionString);
            repo.Add(image);
            return Redirect("/home/index");
        }
        public ActionResult ViewImage(int id)
        {
            var vm = new ViewImageViewModel();
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImagesRepository(connectionString);
            var image = repo.GetById(id);
            if (image == null)
            {
                return RedirectToAction("Index");
            }
            vm.Image = image;
            vm.Liked = false;

            List<string> ids = new List<string>();
            if (Request.Cookies["Ids"] != null)
            {
                ids = Request.Cookies["Ids"].Split(',').ToList();
            }

            if (ids.Contains(id.ToString()))
            {
                vm.Liked = true;
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImagesRepository(connectionString);
            Image currentImage = repo.GetById(id);
            repo.UpdateLikes(currentImage);

            string ids = "";
            var cookie = Request.Cookies["Ids"];
            if (cookie != null)
            {
                ids = $"{cookie},";
            }
            ids += id;
            Response.Cookies.Append("Ids", ids);
            return Json(currentImage.Likes);
        }

        public IActionResult GetCurrentLikes(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImagesRepository(connectionString);
            Image currentImage = repo.GetById(id);
            return Json(currentImage.Likes);
        }

    }
}
