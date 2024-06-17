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
        public string Type { get; set; }
        public int Stock {  get; set; }
        public decimal Vat {  get; set; }
        public decimal Price { get; set; }
        public int PreparationTime { get; set; }

     /*    public MenuItem(int id, string name, string type, int stock, decimal vat, decimal price, int preparationTime)
        {
            Id = id;
            Name = name;
            Type = type;
            Stock = stock;
            Vat = vat;
            Price = price;
            PreparationTime = preparationTime;
        } */
    }

    

}
