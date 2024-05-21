using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Table
    {
        public int Id { get; set; }
        public TableStatus Status { get; set; }
        public int Capacity { get; set; }
    }
}

public enum TableStatus
{
    Empty, Ocupied, Reserved
}
