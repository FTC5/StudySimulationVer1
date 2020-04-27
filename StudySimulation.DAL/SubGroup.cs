using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL
{
    public class SubGroup : Group
    {
        public SubGroup(string name, int course, List<Student> students,int subGroupNum) : base(name, course, students)
        {
            Name = name + subGroupNum.ToString();
        }
    }
}
