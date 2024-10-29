using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{
    public class Brandcls
    {
        public string? Brand { get; set; }
        public string? RecordID { get; set; }
        public string? PriceListID { get; set; }
        public string? ChildID { get; set; }
        public string?  ChildName { get; set; }
    }
}
