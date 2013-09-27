using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogs.Model
{
    public class SysRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatePerson { get; set; }

        public List<SysPerson> SysPerson { get; set; }
    }
}
