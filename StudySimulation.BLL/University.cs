using StudySimulation.BLL.Abstract;
using StudySimulation.BLL.Subjects;
using StudySimulation.DAL;
using System.Collections.Generic;

namespace StudySimulation.BLL
{
    public class University
    {
        List<Subject> subjects = new List<Subject>();//
        Dictionary<Subject, List<Teacher>> staff;
        List<Group> groups = new List<Group>();
        GroupRating groupRating;
        internal List<Subject> Subjects { get => subjects;}
        internal List<Group> Groups { get => groups; set => groups = value; }
        internal Dictionary<Subject, List<Teacher>> Staff { get => staff; set => staff = value; }
        public GroupRating GroupRating { get => groupRating;}

        public University()
        {
            AddSubject();
            CreateStaff(subjects);
        }
        private void AddSubject()
        {
            groupRating = new StudentService();
            subjects.Add(new English(groupRating));
            subjects.Add(new Physics(groupRating));
            subjects.Add(new HigherMathematics(groupRating));
        }
        private void CreateStaff(List<Subject> subjectsList)
        {
            Staff = new Dictionary<Subject, List<Teacher>>();
            for (int i = 0; i < subjectsList.Count; i++)
            {
                Staff.Add(subjectsList[i], new List<Teacher>());
            }
        }
    }
}
