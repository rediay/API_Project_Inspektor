/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool HasResetPassword { get; set; }

        //public int? Age { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }

        public AddressDTO Address { get; set; }

        public SettingsDTO Settings { get; set; }
        public CompanyDTO company { get; set; }        

        public List<ModulesDTO> UserPermissions { get; set; }
        




    }
}
