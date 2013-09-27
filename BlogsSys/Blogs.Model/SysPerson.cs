using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogs.Model
{
    public class SysPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MyName { get; set; }
        public string Password { get; set; }
        public string SurePassword { get; set; }
        public string Sex { get; set; }
        public string SysDepartmentId { get; set; }
        public string Position { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Remark { get; set; }
        public string State { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatePerson { get; set; }
        public string Version { get; set; }
    }
}
