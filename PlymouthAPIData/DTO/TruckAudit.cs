using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{
    public class TruckAudit
    {
        public int RECID { get; set; }
        public int TRUCKID { get; set; }
        public string? TRUCKNAME { get; set; }
        public int ROUTERID { get; set; }
        public string? ROUTENAME { get; set; }
        public int DRIVERID { get; set; }
        public string? DRIVERNAME { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public DateTime SUBMITTEDDATE { get; set; }

    }
}
