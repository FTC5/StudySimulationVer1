using StudySimulation.BLL.Abstract;
using static StudySimulation.BLL.Abstract.GroupRating;
using static StudySimulation.BLL.Abstract.Subject;

namespace StudySimulation.BLL
{
    public class Settings
    {
        GroupRating groupRating;
        University university;
        bool studentInfo = false;
        bool educationalMes = false;
        bool educationalFactor = false;

        public Settings(University university)
        {
            this.university = university;
            groupRating = university.GroupRating;
        }

        public bool StudentInfo { get => studentInfo;}
        public bool EducationalMes { get => educationalMes;}
        public bool EducationalFactor { get => educationalFactor;}

        

        public void StudentInfoSwitch(GetMarkHandler getMarkHandler)
        {
            if (studentInfo)
            {
                OffStudentInfo(getMarkHandler);
            }
            else
            {
                OnStudentInfo(getMarkHandler);
            }
            studentInfo = !studentInfo;
        }
        private void OnStudentInfo(GetMarkHandler getMarkHandler)
        {
            groupRating.GetMark += getMarkHandler;
        }
        private void OffStudentInfo(GetMarkHandler getMarkHandler)
        {
            groupRating.GetMark -= getMarkHandler;
        }
        public void EducationalMesSwitch(MessageHandler messageHandler)
        {
            if (educationalMes)
            {
                OffEducationalMes(messageHandler);
            }
            else
            {
                OnEducationalMes(messageHandler);
            }
            educationalMes = !educationalMes;
        }
        private void OnEducationalMes(MessageHandler messageHandler)
        {
            foreach (Subject item in university.Subjects)
            {
                item.Message += messageHandler;
            }
        }
        private void OffEducationalMes(MessageHandler messageHandler)
        {
            foreach (Subject item in university.Subjects)
            {
                item.Message -= messageHandler;
            }
        }
        public void EducationalFactorInfoSwitch(SuccessFactorHandler factorHandler)
        {
            if (educationalFactor)
            {
                OffEducationalFactorInfo(factorHandler);
               
            }
            else
            {
                OnEducationalFactorInfo(factorHandler);
            }
            educationalFactor = !educationalFactor;
        }
        
        
        private void OnEducationalFactorInfo(SuccessFactorHandler factorHandler)
        {
            foreach (Subject item in university.Subjects)
            {
                item.Factor += factorHandler;
            }
        }
        private void OffEducationalFactorInfo(SuccessFactorHandler factorHandler)
        {
            foreach (Subject item in university.Subjects)
            {
                item.Factor -= factorHandler;
            }
        }
    }
}
