using StudySimulation.DAL;
using StudySimulation.DAL.Abstract;
using StudySimulation.DAL.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StudySimulation.BLL.Abstract
{
    public abstract class Subject
    {
        public delegate void SuccessFactorHandler(string message);
        public event SuccessFactorHandler Factor;
        public delegate void MessageHandler(string message);
        public event MessageHandler Message;
        public GroupRating groupRating;
        protected string name;
        protected int successFactor=0;
        public Subject(GroupRating groupRating)
        {
            this.groupRating = groupRating;
            name = this.GetType().Name;
        }

        public abstract int Lab(Group group, Equipment equipment, Room room, ISubActivities subActivities);
        public abstract int Credit(Group group);
        protected void CallFactorEvent(string message,int successFactor)
        {
            Factor?.Invoke(message+successFactor);
        }
        protected void CallMessageEvent(string message)
        {
            Message?.Invoke(message);
        }
        protected virtual int CheckEquipment(Equipment equipment)
        {
            
            if (equipment != null)
            {
                if (equipment.Name == "Computer")
                {
                    successFactor += 3;
                    Factor?.Invoke($"Students use computer. Student success factor: {successFactor}");
                }
                if (equipment.Name == "Tape recorder")
                {
                    successFactor += -1;
                    Factor?.Invoke($"Teacher use tape recorder. Student success factor: {successFactor}");
                }
            }
            return successFactor;
        }
        protected bool neglectCheack(Student student)
        {
            List<int> evaluations;
            evaluations = student.Evaluations[name];
            foreach (int item in evaluations)
            {
                if (item == 0)
                {
                    return false;
                }
            }
            return true;

        }
        public virtual int Examination(Group group,Equipment equipment)
        {
            
            successFactor = 0;
            Message?.Invoke("Examination start (-_-): ");
            successFactor += CheckEquipment(equipment);
            Message?.Invoke("Students write the exam");
            foreach (Student student in group.Students)
            {
                if(neglectCheack(student)==true)
                {
                    groupRating.Neglect(student, name);
                }
                else
                {
                    groupRating.SetGrades(student, name, group.Students.Count, successFactor);
                }
            }
            return successFactor;
        }
        public virtual int Lectures(List<Group> groups, Equipment equipment)
        {
            successFactor = 0;
            Message?.Invoke("Lectures start : ");
            successFactor += CheckEquipment(equipment);
            Message?.Invoke("Students study");
            foreach (Group group in groups)
            {
                groupRating.SetRandGroupGrade(group, name);
            }  
            return successFactor;
        }
        public virtual int ModulControlWork(Group group, Equipment equipment)
        {
            successFactor = 0;
            Message?.Invoke("Modul control work start (-_-): ");
            successFactor += CheckEquipment(equipment);
            Message?.Invoke("Students write the modul control work");
            foreach (Student student in group.Students)
            {
                if (neglectCheack(student) == true)
                {
                    groupRating.Neglect(student, name);
                }
                else
                {
                    groupRating.SetGrades(student, name, group.Students.Count, successFactor);
                }
            }
            return successFactor;
        }
        public virtual int Practice(Group group, Equipment equipment, ISubActivities subActivities)
        {
            successFactor = 0;
            Message?.Invoke("Practice start : ");
            successFactor += CheckEquipment(equipment);
            if (subActivities != null)
            {
                successFactor += 1;
                Factor?.Invoke(subActivities.Action()+ $".Student success factor: {successFactor}");
            }
            Message?.Invoke("Students study");
            groupRating.SetGroupGrades(group, name);
            return successFactor;
        }
        public virtual int TermPaper(Group group, Equipment equipment)
        {
            successFactor = 0;
            Message?.Invoke("TermPaper start : ");
            Message?.Invoke("Students defend coursework");
            successFactor += CheckEquipment(equipment);
            groupRating.SetGroupGrades(group,name);
            return successFactor;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
