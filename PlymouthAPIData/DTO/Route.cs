using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{
    public class Routes
    {
        public int RECID { get; set; }
        public string? CODE { get; set; }
        public string? NAME { get; set; }
        public string? CREATEDDATE { get; set; }
        public string? MODIFIEDDATE { get; set; }
        public int SORTORDER { get; set; }
        public string? DISABLE { get; set; }
        public int CompRecId { get; set; }

    }
}
