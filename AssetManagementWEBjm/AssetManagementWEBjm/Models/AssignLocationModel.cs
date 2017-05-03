﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementWEBjm.Models
{
    public class AssignLocationModel
    {
        public string AssetCode { get; set; }
        public string LocationCode { get; set; }
        public int Id { get; set; }
    
        public string LocationName { get; set; }
       
        public string AssetName { get; set; }
        public string LastSeen { get; set; }

    }
}