using StudySimulation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.SubActivities
{
    public class ReadText : ISubActivities
    {
        public string Action()
        {
            return "Students reading text; ";
        }
        public override string ToString()
        {
            return "Read text";
        }
    }
}
