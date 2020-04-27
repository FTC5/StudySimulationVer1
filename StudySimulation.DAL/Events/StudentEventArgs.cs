using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.Events
{
    class StudentEventArgs
    {
        private string firstName;
        private string lastName;
        private string subject;
        private int evaluation;

        public StudentEventArgs(string firstName, string lastName, string subject, int evaluation)
        {
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            Evaluation = evaluation;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Subject { get => subject; set => subject = value; }
        public int Evaluation { get => evaluation; set => evaluation = value; }
    }
}
