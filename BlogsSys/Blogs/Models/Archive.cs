using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Blogs
{   
    public class Archive 
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ImgUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int FromPageSeq { get; set; }
        public string CollectDate { get; set; }
        public int CategoryId { get; set; }
    }
}