using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{
    public class LRoute
    {
        public int RecordID { get; set; }
        public int CompanyID { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
