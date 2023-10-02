using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.MVVM.Models
{
    public class DeviceItem
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Vendor { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

    }
}
