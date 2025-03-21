/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System.Collections.Generic;

namespace Common.DTO
{
    public class ModulesDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? icon{ get; set; }
        public List<Children> Children { get; set; }

    }
}
