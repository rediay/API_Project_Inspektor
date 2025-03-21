/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Access : BaseEntity
    {
        public string Name { get; set; }        
        [ForeignKey("Company")]
        public virtual int CompanyId { get; set; }        
        public virtual Company Company { get; set; }
        public virtual ICollection<AccessSubModule> AccessSubModules { get; set; }
    }
}
