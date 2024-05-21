namespace Model
{
    public class Employee
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}

public enum Role
{
    waiter,chef,barman
}