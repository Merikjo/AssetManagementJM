using AssetManagementWEBjm.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementWEBjm.Models
{
    public class AssetLocationViewModel
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssetLocationViewModel()
        {
            this.AssetLocations = new HashSet<AssetLocations>();
        }

        public int AssetLocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAdress { get; set; }
        public int? AssetId { get; set; }
        public int? AssetsId { get; set; }


        public virtual ICollection<AssetLocations> AssetLocations { get; set; }
    }
}
