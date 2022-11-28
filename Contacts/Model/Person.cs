namespace Contacts.Model
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
        }

        public Person(string firstName, string email) : this() 
        {
            FirstName = firstName;
            Email = email;
        }
    }
}
