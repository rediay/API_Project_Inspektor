﻿/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace Common.Entities
{
    public class Permissions
    {
        public int UserId { get; set; }
        public int SubModuleId { get; set; }
        public bool Status { get; set; }
        public virtual User User { get; set; }
        public virtual SubModules SubModule { get; set; }
    }
}
