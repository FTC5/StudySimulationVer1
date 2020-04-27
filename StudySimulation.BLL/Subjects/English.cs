using StudySimulation.BLL.Abstract;
using StudySimulation.DAL;
using StudySimulation.DAL.Abstract;
using StudySimulation.DAL.Equipments;
using StudySimulation.DAL.Interface;
using StudySimulation.DAL.SubActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.BLL.Subjects
{
    public class English : Subject
    {
        public English(GroupRating groupRating) : base(groupRating)
        {
        }

        public override int Credit(Group group)
        {
            CallMessageEvent("Ooo Students have credit: ");
            CallMessageEvent("students receive grades");
            groupRating.SetAverageMark(group, name);
            return 0;
        }

        public override int Lab(Group group, Equipment equipment, Room room, ISubActivities subActivities)
        {
            CallMessageEvent(name + " dont have a lab!");
            return 0;
        }

        public override int Lectures(List<Group> groups, Equipment equipment)
        {
            CallMessageEvent(name + " dont have a lectures!");
            return 0;
        }

        public override int TermPaper(Group group, Equipment equipment)
        {
            CallMessageEvent(name + " dont have a term paper!");
            return 0;
        }
        public override int Practice(Group group, Equipment equipment, ISubActivities subActivities)
        {
            bool CheckAct()
            {
                if(subActivities is ReadText)
                {
                    return true;
                }else if(subActivities is TellText)
                {
                    return true;
                }else if(subActivities is WriteText)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (subActivities == null)
            {
                CallMessageEvent("Students do nothing, it is impossible to conduct classes");
                return 0;
            }
            else if (!CheckAct())
            {
                CallMessageEvent("Students perform the wrong actions, it is impossible to conduct classes");
                return 0;
            }
            else if (!(equipment is TapeRecorder))
            {
                CallMessageEvent("We dont have TapeRecorder, it is impossible to conduct classes");
                return 0;
            }

            successFactor = 0;
            CallMessageEvent("Practice start : ");
            successFactor += CheckEquipment(equipment);
            successFactor += 1;
            CallFactorEvent(subActivities.Action() + ".Student success factor:", successFactor);
            CallMessageEvent("Students study");
            groupRating.SetGroupGrades(group, name);
            return successFactor;
        }
    }
}
