using StudySimulation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.SubActivities
{
    public class TellText : ISubActivities
    {
        public string Action()
        {
            return "Students tell texts; ";
        }
        public override string ToString()
        {
            return "Tell text";
        }
    }
}
