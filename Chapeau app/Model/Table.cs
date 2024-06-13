using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Table
    {
        public int Number { get; set; }
        public TableStatus Status { get; set; }
        public int Capacity { get; set; }
    }
}

public enum TableStatus
{
    Empty, Occupied, Reserved
}
