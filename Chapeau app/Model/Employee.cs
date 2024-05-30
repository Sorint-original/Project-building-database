using System.Data;

namespace Model
{
    public class Employee
    {
        public int Id { get; set; }

        public Role Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                int age = today.Year - DateOfBirth.Year;

                if (today < DateOfBirth.AddYears(age))
                {
                    age--;
                }

                return age;
            }
        }

        public string Password { get; set; }


        public Employee(int id, Role role, string firstName, string lastName, DateOnly dateOfBirth, string password)
        {
            Id = id;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Password = password;
        }
    }

}

public enum Role
{
    Waiter, Chef, Barman
}