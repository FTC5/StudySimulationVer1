using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL.Abstract
{
    public class Room
    {
        protected string name;

        public string Name { get => name; }
        public Room()
        {
            this.name = "Room";
        }
    }
}
