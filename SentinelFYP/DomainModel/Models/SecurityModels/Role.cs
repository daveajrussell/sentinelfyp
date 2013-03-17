using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.SecurityModels
{
    public class Role
    {
        public Guid RoleKey { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
