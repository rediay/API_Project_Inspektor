/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;

namespace Common.DTO
{
    public class IndividualQueryExternalParamsDTO
    {
        public string name { get; set; }
        public string document { get; set; }
      
        public int typeDocument { get; set; }
        public int quantityResults { get; set; } = 0;
        public int numberWords { get; set; }
        public bool hasPriority4 { get; set; }
      
    }
}
