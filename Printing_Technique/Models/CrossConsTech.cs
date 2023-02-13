using System;
using System.Collections.Generic;

namespace Printing_Technique.Models
{
    public partial class CrossConsTech
    {
        public CrossConsTech()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int? IdCons { get; set; }
        public int? IdTech { get; set; }

        public virtual Consumable? IdConsNavigation { get; set; }
        public virtual Technic? IdTechNavigation { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
