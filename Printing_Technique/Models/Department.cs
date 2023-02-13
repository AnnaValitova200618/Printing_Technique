using System;
using System.Collections.Generic;

namespace Printing_Technique.Models
{
    public partial class Department
    {
        public Department()
        {
            Сabinets = new HashSet<Сabinet>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Сabinet> Сabinets { get; set; }
    }
}
