
namespace StudySimulation.BLL
{
    public class PersoneDTO 
    {
        string firstName;
        string lastName;
        public PersoneDTO()
        {
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
    }
}
