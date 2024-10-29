using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlymouthAPIData.DTO
{
    public class PreparePriceBook
    {
        public int RECID { get; set; }
        public int RGRECID { get; set; }
        public int CRECID { get; set; }
        public int PPLRECID { get; set; }
        public int MAINGROUPRECID { get; set; }
        public int SUBGROUPRECID { get; set; }
        public int BRANDRECID { get; set; }
        public string? PROPERTYITEM { get; set; }
        public int ITEMRECID { get; set; }
        public string? ITEMDESCRIPTION { get; set; }
        public decimal ITEMPRICE { get; set; }
        public DateTime EFFECTIVEFROM { get; set; }
        public DateTime VALIDITYDAYS { get; set; }
    }
}
