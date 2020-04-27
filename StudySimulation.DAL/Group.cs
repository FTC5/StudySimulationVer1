using StudySimulation.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL
{
    public class Group : IComparable<Group>
    {
        List<Student> students = new List<Student>();
        string name;
        int course;

        public string Name { get => name; set => name = value; }
        public int Course { get => course; set => course = value; }
        public List<Student> Students { get => students; set => students = value; }

        public Group(string name, int course, List<Student> students)
        {
            Name = name;
            Course = course;
            Students = students;
        }
        public int CompareTo(Group group)
        {
            return ToString().CompareTo(group.ToString());
        }
        public override string ToString()
        {
            return Name+"-"+course;
        }
    }
}
