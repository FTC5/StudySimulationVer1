using StudySimulation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.SubActivities
{
    public class PerformExperiment: ISubActivities
    {
        public string Action()
        {
            return "Students Perform an experiment; ";
        }

        public override string ToString()
        {
            return "Perform experiment";
        }
    }
}
