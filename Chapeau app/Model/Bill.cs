namespace Model
{
    public class Bill
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public float Vat { get; set; }
        public int GuestNumber { get; set; }
        public Table Table { get; set; } // Add 'Model.' to match the accessibility
        public string Feedback { get; set; }
        public float Tip { get; set; }

        public Bill(Table table, string feedback) // Add 'Model.' to match the accessibility
        {
            Table = table;
            Feedback = feedback;
        }
    }

  
}
