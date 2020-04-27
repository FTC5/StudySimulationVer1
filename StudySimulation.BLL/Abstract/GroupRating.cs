using StudySimulation.DAL;
using System;
using System.Collections.Generic;

namespace StudySimulation.BLL.Abstract
{
    public abstract class GroupRating
    {
        public delegate void GetMarkHandler(string message);
        public event GetMarkHandler GetMark;
        protected Random rand;
        public GroupRating()
        {
            rand = new Random();
        }
        protected void CallGetMarkHandler(string message)
        {
            GetMark?.Invoke(message);
        }
        protected abstract int CountLuck(int studentCount = 0, int successFactor = 0);
        protected abstract int GetGrade(int luck);
        protected abstract int GetAverageMark(Student student, string subject);
        public virtual void SetGroupGrades(Group group, string subject,int boost = 0)
        {
            int luckNow = 0;
            int evaluation = 0;
            luckNow = CountLuck(group.Students.Count, boost);
            GetMark?.Invoke(group.ToString());
            foreach (Student student in group.Students)
            {
                luckNow += student.Luck;
                if (!CheckSub(student, subject))
                {
                    student.Evaluations.Add(subject, new List<int>());
                }
                evaluation = GetGrade(luckNow);
                GetMark?.Invoke(student.ToString() + " : " + evaluation);
                student.Evaluations[subject].Add(evaluation);
            }
        }
        public virtual void SetGrades(Student student, string subject, int studentCount = 0, int boost = 0)
        {
            int luckNow = 0;
            int evaluation = 0;
            luckNow = CountLuck(studentCount, boost);
            luckNow += student.Luck;
            if (!CheckSub(student, subject))
            {
                student.Evaluations.Add(subject, new List<int>());
            }
            evaluation = GetGrade(luckNow);
            GetMark?.Invoke(student.ToString() + " : " + evaluation);
            student.Evaluations[subject].Add(evaluation);
        }
        public void Neglect(Student student, string subject)
        {
            GetMark?.Invoke(student.ToString() + " : " + 0);
            student.Evaluations[subject].Add(0);
        }
        public void SetAverageMark(Group group, string subject)
        {
            int evaluation = 0;
            foreach (Student student in group.Students)
            {
                evaluation = GetAverageMark(student, subject);
                GetMark?.Invoke(student.ToString() + " : " + evaluation);
                student.Evaluations[subject].Add(evaluation);
            }
        }
        public virtual void SetRandGroupGrade(Group group, string subject, int successFactor = 0)
        {
            int luckNow = 0;
            int randNum = 0;
            int evaluation = 0;
            luckNow = CountLuck(group.Students.Count, successFactor);
            foreach (Student student in group.Students)
            {
                randNum = rand.Next(0, 30);
                if (randNum < 8)
                {
                    luckNow += student.Luck;
                    if (!CheckSub(student, subject))
                    {
                        student.Evaluations.Add(subject, new List<int>());
                    }
                    evaluation = GetGrade(luckNow);
                    if (evaluation == 0)
                    {
                        evaluation = 2;
                    }
                    GetMark?.Invoke(student.ToString() + " : " + evaluation);
                    student.Evaluations[subject].Add(evaluation);
                }

            }
        }
        private bool CheckSub(Student student, string subject)
        {
            return student.Evaluations.ContainsKey(subject);
        }
    }
}
