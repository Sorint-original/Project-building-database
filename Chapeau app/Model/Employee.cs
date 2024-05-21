namespace Model
{
    public class Employee
    {
        public int Id { get; set; }
        public role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}

public enum role
{
    waiter,chef,barman
}