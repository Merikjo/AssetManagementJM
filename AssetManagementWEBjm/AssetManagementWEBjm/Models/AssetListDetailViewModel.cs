﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementWEBjm.Models
{
    public class AssetListDetailViewModel
    {
        public int Id { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAdress { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public DateTime LastSeen { get; set; }

        public int? LocationId { get; set; }

        public int? AssetId { get; set; }

        public int? AssetsId { get; set; }
        public string Adress { get; set; }

        public string AssetType { get; set; }
        public string AssetModel { get; set; }

        public string AssetLocation { get; set; }
    }
}