namespace Model
{
    public class Bill
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public float Vat { get; set; }
        public int GuestNumber { get; set; }
        public int Table { get; set; } // Add 'Model.' to match the accessibility
        public string? Feedback { get; set; }
        public float Tip { get; set; }

        public Bill(int billId, decimal totalPrice, float vat, int guestNumber, int table, string feedback, float tip) // Add 'Model.' to match the accessibility
        {
            Id = billId;
            TotalPrice = totalPrice;
            Vat = vat;
            GuestNumber = guestNumber;
            Table = table;
            Feedback = feedback;
            Tip = tip;
        }
    }
}
