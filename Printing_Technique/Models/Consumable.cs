using System;
using System.Collections.Generic;

namespace Printing_Technique.Models
{
    public partial class Consumable
    {
        public Consumable()
        {
            CrossConsTeches = new HashSet<CrossConsTech>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CrossConsTech> CrossConsTeches { get; set; }
    }
}
