using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Blogs.Model
{   
    public class Archive 
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }      
        public DateTime PublishDate { get; set; }       
        public int CategoryId { get; set; }
        public int VisitTotal { get; set; }
    }
}