using System;
using System.Collections.Generic;

namespace Printing_Technique.Models
{
    public partial class Сabinet
    {
        public Сabinet()
        {
            Technics = new HashSet<Technic>();
        }

        public int Id { get; set; }
        public string Namber { get; set; } = null!;
        public int? IdDepartment { get; set; }

        public virtual Department? IdDepartmentNavigation { get; set; }
        public virtual ICollection<Technic> Technics { get; set; }
    }
}
