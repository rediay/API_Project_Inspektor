using Common.DTO.Management;
using Common.Entities;
using System;

namespace Common.DTO.Users
{
    public class UserManagementDto : BaseDTO
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int? CompanyId { get; set; }
        public bool IsActive { get; set; }

        public Permissions[] Permissions { get; set; }
        public RoleUserDTO roleUser { get; set; }
    }
}