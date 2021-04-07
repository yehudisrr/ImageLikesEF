using ImageShareLikesEF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageShareLikesEF.Web.Models
{
    public class ViewImageViewModel
    {
        public Image Image { get; set; }
        public bool Liked { get; set; }
    }
}
