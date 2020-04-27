using StudySimulation.BLL.Abstract;
using StudySimulation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.BLL
{
    public class InfoService
    {
        University university;

        public InfoService(University university)
        {
            this.university = university;
        }
        public int GetSubjectCount()
        {
            return university.Subjects.Count;
        }
        public int GetSubjTeacherCount(int sub)
        {
            Subject subject = university.Subjects[sub];
            return university.Staff[subject].Count;
        }
        public int GetGroupCount()
        {
            return university.Groups.Count;
        }
        public string GetGroup()
        {
            return GetTextList(university.Groups);
        }
        public string GetSubject()
        {
            return GetTextList(university.Subjects);
        }
        public string GetSubjTeacher(int sub)
        {
            Subject subject = university.Subjects[sub];
            return GetTextList(university.Staff[subject]);
        }
        public string GetTeacherGroup(int sub, int teacherIndex)
        {
            Subject subject = university.Subjects[sub];
            Teacher teacher = university.Staff[subject][teacherIndex];
            return GetTextList(teacher.Groups);
        }
        private string GetTextList<T>(List<T> list)
        {
            string text = "";
            for (int i = 0; i < list.Count; i++)
            {
                text += list[i].ToString() + " : " + i + "\n";
            }
            return text;
        }
       
        public int GetTeacherGroupCount(int sub, int teacherIndex)
        {
            Subject subject = university.Subjects[sub];
            Teacher teacher = university.Staff[subject][teacherIndex];
            return teacher.Groups.Count;
        }
        public string GetInfo()
        {
            string text = "";
            foreach (Subject subject in university.Subjects)
            {
                text += "\n-|" + subject.ToString() + " teacher|-\n";
                foreach (Teacher teacher in university.Staff[subject])
                {
                    text += teacher.ToString() + " group : ";
                    foreach (Group group in teacher.Groups)
                    {
                        text += group.ToString() + " ";
                    }
                }
            }
            return text;
        }
        public string GetGroupsGrades(int groupId)
        {
            string text = "";
            Group group = university.Groups[groupId];
            text += "\n-|" + group.ToString() + " student|-";
            foreach (Student student in group.Students)
            {
                text += "\n" + student.ToString() + "\n";
                foreach (Subject subject in university.Subjects)
                {
                    text += student.GetGrades(subject.ToString()) + "\n";
                }
            }
            return text;
        }
    }
}
