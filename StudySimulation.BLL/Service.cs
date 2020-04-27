using StudySimulation.BLL.Abstract;
using StudySimulation.BLL.Subjects;
using StudySimulation.DAL;
using StudySimulation.DAL.Abstract;
using StudySimulation.DAL.Equipments;
using StudySimulation.DAL.Interface;
using StudySimulation.DAL.Rooms;
using StudySimulation.DAL.SubActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudySimulation.BLL
{
    public enum ROOM
    {
        LABORATORY,
        CLASSROOM
    }
    public enum EQUIPMENT
    {
        COMPUTER,
        TAPERECORDER,
        NOTHING
    }
    public enum SUBACTIVITIES
    {
        PERFORM_EXPERIMENT,
        READTEXT,
        TELLTEXT,
        WRITETEXT,
        NOTHING
    }
    public class Service
    {
        Random random = new Random();
        List<Subject> subjects = new List<Subject>();//
        Dictionary<Subject, List<Teacher>> staff;
        List<Group> groups = new List<Group>();
        public Service(University university)
        {
            subjects = university.Subjects;
            staff = university.Staff;
            groups = university.Groups;
        }

        public void AddGroup(List<PersoneDTO> persones, int course, string name)
        {
            int GetLuck()
            {
                int minLuck = 1;
                int maxLuck = 5;
                return random.Next(minLuck, maxLuck);
            }
            List<Student> students = new List<Student>();
            for (int i = 0; i < persones.Count; i++)
            {
                students.Add(new Student(persones[i].FirstName,persones[i].LastName, GetLuck()));
            }
            groups.Add(new Group(name, course, students));
        }
        public bool AddTeacher(PersoneDTO person,int sub)
        {
            int count=0;
            Subject subject= subjects[sub];
            if (subject == null)
            {
                return false;
            }else if (subject is Physics)
            {
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].Course == 1)
                    {
                        count += 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].Course == 2 || groups[i].Course == 1)
                    {
                        count += 1;
                    }
                }
            }
            if (count+1 <= staff[subject].Count)
            {
                return false;
            }
            staff[subject].Add(new Teacher(person.FirstName,person.LastName));       
            return true;
        }
        public string AddGroupToTecher(int teacherInd, int groupInd, int sub)
        {
            Subject subject = subjects[sub];
            if (subject == null)
            {
                return "Choice subject";
            }
            else if (subject is Physics)
            {
                SubGroup[] subGroups;
                if (groups[groupInd].Course != 1)
                {
                    return "Group must be in 1 course";
                }
                subGroups = SpliteGroup(groups[groupInd]);
                for (int i = 0; i < subGroups.Length; i++)
                {
                    staff[subject][teacherInd].AddGroup(subGroups[i]);
                }
                return "";
            }
            else if (staff[subject].Count <= teacherInd)
            {
                return "Input error";
            }
            else if (groups.Count <= groupInd)
            {
                return "Input error";
            }
            if (groups[groupInd].Course > 2)
            {
                return "Group must be in 1 or 2 course";
            }
            staff[subject][teacherInd].AddGroup(groups[groupInd]);
            return "";
        }
        public bool StartActivity(int sub,EQUIPMENT Equip,SUBACTIVITIES SubAct,ROOM Room, int tIndex, int gIndex,int activity)
        {
            Subject subject = subjects[sub];
            Teacher teacher ;
            Group group;
            Room room = GetRoom(Room);
            Equipment equipment = GetEquipment(Equip);
            ISubActivities subActivities = GetSubactivities(SubAct);
            int successFactor = 0;
            if (activity != 4)
            {
                teacher = staff[subject][tIndex];
                group = teacher.Groups[gIndex];
            }
            else
            {
                List<Group> teachersGroups = new List<Group>();
                if (staff[subject].Count == 0)
                {
                    return false;
                }
                teacher = staff[subject][0];
                foreach (Teacher persone in staff[subject])
                {
                    teachersGroups.AddRange(persone.Groups);
                }
                subject.Lectures(teachersGroups, equipment);
                return true;
            }
            
           
            switch (activity)
            {
                case 1:
                    successFactor = subject.Lab(group, equipment, room, subActivities);
                    break;
                case 2:
                    successFactor = subject.Credit(group);
                    break;
                case 3:
                    successFactor = subject.Examination(group, equipment);
                    break;
                case 5:
                    successFactor = subject.ModulControlWork(group, equipment);
                    break;
                case 6:
                    successFactor = subject.Practice(group, equipment, subActivities);
                    break;
                case 7:
                    successFactor = subject.TermPaper(group, equipment);
                    break;
                default:
                    return false;
            }
            return true;
        }
        private Equipment GetEquipment(EQUIPMENT equipment)
        {
            switch (equipment)
            {
                case EQUIPMENT.COMPUTER:
                    return new Computer();
                case EQUIPMENT.TAPERECORDER:
                    return new TapeRecorder();
                case EQUIPMENT.NOTHING:
                default:
                    return null;
            }
        }
        private ISubActivities GetSubactivities(SUBACTIVITIES subActivities)
        {
            switch (subActivities)
            {
                case SUBACTIVITIES.PERFORM_EXPERIMENT:
                    return new PerformExperiment();
                case SUBACTIVITIES.READTEXT:
                    return new ReadText();
                case SUBACTIVITIES.TELLTEXT:
                    return new TellText();
                case SUBACTIVITIES.WRITETEXT:
                    return new WriteText();
                case SUBACTIVITIES.NOTHING:
                default:
                    return null;
            }
        }
        private Room GetRoom(ROOM room)
        {
            switch (room)
            {
                case ROOM.LABORATORY:
                    return new Laboratory();
                case ROOM.CLASSROOM:
                    return new Classroom();
                default:
                    return new Room();
                    
            }
        }
        private SubGroup[] SpliteGroup(Group group)
        {
            SubGroup[] subGroups;
            List<Student> students;
            int n = 1;
            int subGStudentCount = 10;
            int count = group.Students.Count;
            if (count < subGStudentCount*2)
            {
                subGroups = new SubGroup[n];
                subGroups[0] = new SubGroup(group.Name, group.Course, group.Students, 0);
                return subGroups;
            }
            n = count / 13;
            subGroups = new SubGroup[n];
            for (int i = 0, j = 0; i < n; i++)
            {
                students = new List<Student>();
                for (; j <= subGStudentCount*(i+1) || j<count; j++)
                {
                    students.Add(group.Students[j]);
                }
                subGroups[i] = new SubGroup(group.Name, group.Course, students, i);
            }
            return subGroups;
        }
    }
}
