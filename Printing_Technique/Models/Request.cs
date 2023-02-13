using System;
using System.Collections.Generic;

namespace Printing_Technique.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public int IdCross { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public decimal Price { get; set; } = 0;

        public virtual CrossConsTech IdCrossNavigation { get; set; } = null!;
    }
}
