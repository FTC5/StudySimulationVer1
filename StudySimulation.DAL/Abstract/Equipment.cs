using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.Abstract
{
    public abstract class Equipment
    {
        protected string name;

        public string Name { get => name; }
        protected Equipment()
        {
            name = this.GetType().Name;
        }
    }
}
