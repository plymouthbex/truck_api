using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{
    public class ListviewHDR
    {
        public int LVH_RECID { get; set; }
        public string? LVH_CODE { get; set; }
        public string? LVH_MENUTYPE { get; set; }
        public string? LVH_FROMCLAUSE { get; set; }
        public string? LVH_ORDERBY { get; set; }
        public string? LVH_DESC1 { get; set; }
        public string? LVH_DESC2 { get; set; }
        public string? LVH_ENUMCRITERIA { get; set; }
        public string? LVH_BASECRITERIA { get; set; }
        public string? LVH_COMPFIELDSALL { get; set; }
        public string? LVH_COMPFIELDS { get; set; }
        public string? LVH_HREFWEB { get; set; }
    }
}
