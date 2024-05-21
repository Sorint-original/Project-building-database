using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Bill
    {
        public int Id { get; set; }
        public decimal TotalPrice {  get; set; }
        public float Vat {  get; set; }
        public int GuestNumber {  get; set; }
        public Table Table { get; set; }
    }
}
