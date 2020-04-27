using StudySimulation.DAL.Abstract;
using StudySimulation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation.DAL
{
    public class Student :Person
    {
        int luck;
        
        Dictionary<string, List<int>> evaluations;

        public Dictionary<string, List<int>> Evaluations { get => evaluations; set => evaluations = value; }
        public int Luck { get => luck; set => luck = value; }

        public Student(string firstName, string lastName,int luck) : base(firstName, lastName)
        {
            Evaluations = new Dictionary<string, List<int>>();
            this.Luck = luck;
        }
        public string GetGrades(string subject)
        {
            string text = subject + " : ";
            if (!evaluations.ContainsKey(subject))
            {
                return text;
            }
            foreach (int item in evaluations[subject])
            {
                text += item.ToString() + ", ";
            }
            return text;
        }
    }
}
