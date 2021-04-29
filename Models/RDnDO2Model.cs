using System;
using System.ComponentModel;

namespace MonitorDO2.Models
{
    public class RDnDO2Model
    {
        [Description(@"DOC_AWB_RECEIPT_DISPATCH link")]
        public string RdId { get; set; }

        [Description(@"DOC_DO_RECEIPT_DISPATCH link")]
        public string DrdId { get; set; }

        [Description(@"DOC_DO_DO2 link")]
        public string Do2Id { get; set; }

        [Description(@"AWB number")]
        public string FullAwbNumber { get; set; }

        [Description(@"AWB technology")]
        public string AwbTech { get; set; }       

        [Description(@"dispatch's date")]
        public DateTime RdDate { get; set; }
        public string Pieces { get; set; }
        public string Weight { get; set; }
    }
}
