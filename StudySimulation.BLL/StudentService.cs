using StudySimulation.BLL.Abstract;
using StudySimulation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.BLL
{
    public class StudentService : GroupRating
    {
        protected override int CountLuck(int studentCount = 0, int successFactor = 0)
        {
            return successFactor - (studentCount/15);
        }
        protected override int GetGrade(int luck)
        {
            int evaluation = 0;
            luck += rand.Next(0, 30);
            if(luck <= 6)
            {
                evaluation = 0;
            }
            if (luck <= 12)
            {
                evaluation = 2;
            }
            else if (luck <= 18)
            {
                evaluation = 3;
            }
            else if (luck <= 26)
            {
                evaluation = 4;
            }
            else
            {
                evaluation = 5;
            }
            return evaluation;
        }
        protected override int GetAverageMark(Student student,string subject)
        {
            if (!CheckSub(student,subject))
            {
                return 0;
            }
            int averageMark = 0;
            int sum = 0;
            List<int> subjectEvaluations = student.Evaluations[subject];
            for (int i = 0; i < subjectEvaluations.Count; i++)
            {
                sum += subjectEvaluations[i];
            }
            averageMark = sum / subjectEvaluations.Count;
            return averageMark;
        }
        private bool CheckSub(Student student,string subject)
        {
            return student.Evaluations.ContainsKey(subject);
        }
    }
}
