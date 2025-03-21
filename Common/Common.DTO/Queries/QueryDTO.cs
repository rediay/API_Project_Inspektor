﻿/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.DTO.Queries
{
    public class QueryDTO
    {
        public int Id { get; set; }
        public int IdQueryCompany { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string ThirdTypeName { get; set; }
        public int QueryTypeId { get; set; }
        public QueryType QueryType { get; set; }
        // public User User { get; set; }
        public Company Company { get; set; }
        public List<ListDTO> Lists { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public CompanyDto Company { get; set; }
        public  UserDTO User { get; set; }
        //public List<QueryDetailDTO> QueryDetails { get; set; }


    }
}
