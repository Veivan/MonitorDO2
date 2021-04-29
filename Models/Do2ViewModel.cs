using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MonitorDO2.Models
{
    public class Do2ViewModel
    {
        //public IEnumerable<Do2Model> Do2s { get; set; }

        public IEnumerable<RDnDO2Model> RdWoDo2s { get; set; }

        // date of dispatch
        [Display(Name = "Дата выдачи")]
        public DateTime RdDate { get; set; }

    }
}
