using Blog.Data.Entites.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.ViewModels.Posts
{
    public class SliderViewModel
    {
        public string Title { get; set; }
        public Category Category { get; set; }  
        public string Image { get; set; }
        public string Slug { get; set;  }
    }
}
