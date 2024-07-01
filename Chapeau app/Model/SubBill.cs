

namespace Model
{
    public class SubBill
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public float Vat { get; set; }

        public int BillId { get; set; }
     
    }
}
