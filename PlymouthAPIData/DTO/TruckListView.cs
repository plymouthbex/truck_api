using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{

    public class TruckListView
    {
        public int RecordID { get; set; }
        public int? RouteID { get; set; }
        public string? RouteCode { get; set; }
        public string? RouteName { get; set; }
        public int? TruckID { get; set; }
        public string? TruckCode { get; set; }
        public string? TruckName { get; set; }
        public int? DoorNO { get; set; }
        public string? DoorCode { get; set; }
        public string? DoorName { get; set; }
        public int? DriverID { get; set; }
        public string? DriverCode { get; set; }
        public string? DriverName { get; set; }
    }
    
}
