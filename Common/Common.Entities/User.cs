/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;

namespace Common.Entities
{
    public class User : DeletableEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
 
        public int CompanyId { get; set; }
        
        public bool IsActive { get; set; }
        
        /*public int? Age { get; set; }

        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressZipCode { get; set; }
        public double? AddressLat { get; set; }
        public double? AddressLng { get; set; }*/

        /*public virtual UserPhoto Photo { get; set; }*/
        public virtual Settings Settings { get; set; }
        public string Identification { get; set; }
        public bool TermsCondition { get; set; }
        public bool HasResetPassword { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
