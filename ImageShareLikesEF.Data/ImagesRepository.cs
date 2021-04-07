using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageShareLikesEF.Data
{
    public class ImagesRepository
    {
        private readonly string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetAll()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.OrderByDescending(i => i.TimeUploaded).ToList();
        }

        public void Add(Image image)
        {
            image.TimeUploaded = DateTime.Now;
            using var context = new ImageDbContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }
        public Image GetById(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void UpdateLikes(Image image)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Images.Attach(image);
            image.Likes++;
            context.Entry(image).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}