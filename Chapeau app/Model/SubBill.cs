using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class SubBill: Bill
    {
        public Bill Bill { get; set; }
     
    }
}
