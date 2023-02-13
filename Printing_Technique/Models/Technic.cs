using System;
using System.Collections.Generic;

namespace Printing_Technique.Models
{
    public partial class Technic
    {
        public Technic()
        {
            CrossConsTeches = new HashSet<CrossConsTech>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int IdCabinet { get; set; }

        public virtual Сabinet IdCabinetNavigation { get; set; } = null!;
        public virtual ICollection<CrossConsTech> CrossConsTeches { get; set; }
    }
}
