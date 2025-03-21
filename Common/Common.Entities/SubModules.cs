/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class SubModules : BaseEntity
    {
        public string Title { get; set; }
        public string icon { get; set; }
        public string link{ get; set; }
        [ForeignKey(nameof(Module))]
        public virtual int ModuleId { get; set; }
        public virtual Modules Module{ get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
        public virtual ICollection<AccessSubModule> AccessSubModules{ get; set; }

    }
}
