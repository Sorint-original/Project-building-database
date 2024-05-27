using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public type Type { get; set; }
        public int Stock {  get; set; }
        public float Vat {  get; set; }
        public decimal Price { get; set; }
        
    }
}

public enum type
{
    food,drink
}
