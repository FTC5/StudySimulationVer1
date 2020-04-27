using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.Abstract
{
    public abstract class Person
    {
        private string firstName;
        private string lastName;

        public string FirstName { get => firstName; }
        public string LastName { get => lastName;}

        protected Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public override string ToString()
        {
            return firstName +" "+lastName;
        }
    }
}
