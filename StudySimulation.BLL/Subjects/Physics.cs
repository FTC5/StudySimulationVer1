
using StudySimulation.BLL.Abstract;
using StudySimulation.DAL;
using StudySimulation.DAL.Abstract;
using StudySimulation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.BLL.Subjects
{
    public class Physics : Subject
    {
        public Physics(GroupRating groupRating) : base(groupRating)
        {
        }

        public override int Credit(Group group)
        {
            CallMessageEvent(name + " dont have a credit!");
            return 0;
        }

        public override int Lab(Group group, Equipment equipment, Room room, ISubActivities subActivities)
        {
            successFactor = 0;
            CallMessageEvent("Lab start : ");
            successFactor = 0;

            if (room.Name != "Laboratory")
            {
                CallMessageEvent("Students cannot study. They dont have laboratory");
                return 0;
            }
            else if (subActivities == null)
            {
                CallMessageEvent("Students nothing to do");
                return 0;
            }
            if (subActivities.ToString() != "Perform experiment")
            {
                CallMessageEvent("Students do not conduct experiments. It is not possible to conduct physics");
                return 0;
            }
            CallMessageEvent(subActivities.Action());
            successFactor += CheckEquipment(equipment);
            CallMessageEvent("Students study");
            return successFactor;
        }
    }
}
