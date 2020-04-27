using StudySimulation.DAL.Abstract;
using StudySimulation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL
{
    public class Teacher: Person
    {
        private List<Group> groups = new List<Group>();
        public List<Group> Groups { get => groups;}
        public Teacher(string firstName, string lastName) : base(firstName, lastName)
        {

        }
        public bool AddGroup(Group group)
        {
            if (Groups.Contains<Group>(group))
            {
                return false;
            }
            Groups.Add(group);
            return true;
        }
    }
}
