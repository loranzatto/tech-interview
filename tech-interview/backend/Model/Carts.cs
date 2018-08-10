using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tech_interview.backend.Model
{
    public class Carts
    {
        public Carts()
        {
            Items = new List<Items>();
        }
        public int Id { get; set; }
        public List<Items> Items { get; set; }
        public int Total { get; set; }
    }
}
