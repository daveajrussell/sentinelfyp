using DomainModel.Models.SecurityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sentinel.Models
{
    public class CreateUserViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<Role> UserRoles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserContactNumber { get; set; }
        public string Email { get; set; }
    }
}