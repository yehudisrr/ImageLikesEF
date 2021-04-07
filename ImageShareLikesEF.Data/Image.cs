using System;

namespace ImageShareLikesEF.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime TimeUploaded { get; set; }
        public int Likes { get; set; }
    }
}
