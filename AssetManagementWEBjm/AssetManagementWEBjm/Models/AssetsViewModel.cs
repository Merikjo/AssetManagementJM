using AssetManagementWEBjm.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementWEBjm.Models
{
    public class AssetsViewModel
    {
        public AssetsViewModel()
        {
            this.AssetLocations = new HashSet<AssetLocations>();
            //this.AssetLocation = new HashSet<AssetLocation>();
        }

        public int AssetsId { get; set; }
        public string AssetCode { get; set; }
        public string AssetType { get; set; }
        public string AssetModel { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }
        public string LocationAdress { get; set; }

        public string AssetLocation { get; set; }

        public int? LocationId { get; set; }

        public virtual ICollection<AssetLocations> AssetLocations { get; set; }
        //public virtual ICollection<AssetLocation> AssetLocation { get; set; }
    }
}
