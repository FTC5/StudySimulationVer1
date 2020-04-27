using StudySimulation.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation
{
    class Menu
    {
        Service service;
        InfoService infoService;
        Settings settings;
        string choise;

        public Menu(Service service, Settings settings,InfoService infoService)
        {
            this.service = service;
            this.settings = settings;
            this.infoService = infoService;
            settings.EducationalFactorInfoSwitch(DisplayMessage);
            settings.EducationalMesSwitch(DisplayMessage);
            settings.StudentInfoSwitch(DisplayMessage);
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("---|Menu|----");
                Console.WriteLine("Add teacher : 1");
                Console.WriteLine("Add group : 2");
                Console.WriteLine("Add group to teacher: 3");
                Console.WriteLine("Start activity : 4");
                Console.WriteLine("Get Info : 5");
                Console.WriteLine("Get groups grades : 6");
                Console.WriteLine("Settings : 7");
                Console.WriteLine("Exit : 8");
                choise = Console.ReadLine();
                Console.WriteLine(" ");
                switch (choise)
                {
                    case "1":
                        AddTeacher(ChoiceSubject());
                        break;
                    case "2":
                        AddGroup();
                        break;
                    case "3":
                        AddGroupToteacher();
                        break;
                    case "4":
                        StartActivity(ChoiceSubject());
                        break;
                    case "5":
                        Console.WriteLine(infoService.GetInfo());
                        break;
                    case "6":
                        int n=NumberCheck("\n -|Choise Group| -\n" + infoService.GetGroup(),
                            infoService.GetGroupCount());
                        Console.WriteLine(infoService.GetGroupsGrades(n));
                        break;
                    case "7":
                        Settings();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Interapt input");
                        break;
                }
                Console.WriteLine(" ");
            }
            
        }
        
        private void StartActivity(int subject)
        {
            int teacher=0;
            int group=0;
            int activity = choiceActivity();
            EQUIPMENT equipment=EQUIPMENT.NOTHING;
            ROOM room=ROOM.CLASSROOM;
            SUBACTIVITIES subActivities=SUBACTIVITIES.NOTHING;
            if (activity != 4)
            {
                teacher = ChoiceTeacher(subject);
                if (teacher == -2)
                {
                    Console.WriteLine("//|Process stop|\\\\");
                    return;
                }
                if(teacher!=-1)
                {
                    group = ChoiseTeacherGroup(subject, teacher);
                    if (group == -2)
                    {
                        Console.WriteLine("//|Process stop|\\\\");
                        return;
                    }
                }
            }
            switch (activity)
            {
                case 1:
                    equipment = ChoiceEQUIPMENT();
                    room = ChoiceROOM();
                    subActivities = ChoiceSUBACTIVITIES();
                    break;
                case 6:
                    equipment = ChoiceEQUIPMENT();
                    subActivities = ChoiceSUBACTIVITIES();
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 7:
                    equipment = ChoiceEQUIPMENT();
                    break;
            }
            if(teacher==-1)
            {
                int tCount= infoService.GetSubjTeacherCount(subject);
                int gCount=0;
                for (int i = 0; i < tCount; i++)
                {
                    gCount = infoService.GetTeacherGroupCount(subject, i);
                    for (int j = 0; j < gCount; j++)
                    {
                        service.StartActivity(subject, equipment, subActivities, room, i, j, activity);
                    }
                }
            }
            else if(group==-1)
            {
                int gCount = 0;
                gCount = infoService.GetTeacherGroupCount(subject, teacher);
                for (int j = 0; j < gCount; j++)
                {
                    service.StartActivity(subject, equipment, subActivities, room, teacher, j, activity);
                }
            }
            else
            {
                service.StartActivity(subject, equipment, subActivities, room, teacher, group, activity);
            }
            
        }
        private int choiceActivity()
        {
            int count = 7;
            return NumberCheck("\n -|Choise Activity| -\n" + " Lab 1; \n Credit 2; \n Examination 3; \n" +
                " Lectures 4; \n ModulControlWork 5; \n Practice 6; \n TermPaper 7; \n",
                count + 1, 1);
        }
        private int ChoiceTeacher(int subject)
        {
            if (infoService.GetSubjTeacherCount(subject) == 0)
            {
                Console.WriteLine("\\\\|We dont have teacher|//");
                return -2;
            }
            return NumberCheck(infoService.GetSubjTeacher(subject) + "All (-1) \nNo one(-2)",
                infoService.GetSubjTeacherCount(subject),-2);
        }
        private int ChoiseTeacherGroup(int subject,int teacher)
        {
            if (infoService.GetTeacherGroupCount(subject, teacher) == 0)
            {
                Console.WriteLine("\\\\|Teacher dont have group|//");
                return -2;
            }
            return NumberCheck("\n -|Choise Group| -\n" + infoService.GetTeacherGroup(subject,teacher)+ "All (-1) \nNo one(-2)",
                infoService.GetTeacherGroupCount(subject,teacher),-2);
        }
        private int ChoiceSubject()
        {
            return NumberCheck("-|Choise Subject|-\n" + infoService.GetSubject(), infoService.GetSubjectCount());
        }

        private void AddGroup()
        {
            int course = 0;
            int count = 0;
            string groupName="";
            List<PersoneDTO> persones = new List<PersoneDTO>();
            Console.WriteLine("\n-|Create Group|-\n");
            course = NumberCheck("Course :",7,1);
            count = NumberCheck("Student count(<=60) :",61,13);
            Console.Write("Group name : ");
            groupName = Console.ReadLine();
            Console.WriteLine("\n+|Student|+");
            persones.Add(AddPerson());
            Console.WriteLine("Clone : No(0)");
            choise = Console.ReadLine();
            if (choise == "0")
            {
                for (int i = 1; i < count; i++)
                {
                    persones.Add(AddPerson());
                }
            }
            else
            {
                for (int i = 1; i < count; i++)
                {
                    persones.Add(Clone(persones[0], i));
                }
            }
            service.AddGroup(persones,course, groupName);
        }
        private PersoneDTO Clone(PersoneDTO original,int cloneIndex)
        {
            PersoneDTO personeDTO = new PersoneDTO();
            personeDTO.FirstName = original.FirstName + cloneIndex.ToString();
            personeDTO.LastName = original.LastName + cloneIndex.ToString();
            return personeDTO;
        }
        private int NumberCheck(string message, int max, int min=0)
        {
            int n = 0;
            while (true)
            {
                Console.Write(message+" ");
                choise = Console.ReadLine();
                if (Int32.TryParse(choise, out n))
                {
                    if (n < max && n >= min)
                    {
                        return n;
                    }
                    Console.WriteLine("//|Wrong input!|\\\\");
                }
                else
                {
                    Console.WriteLine("//|Wrong input!|\\\\");
                }
            }
        }
        public void AddTeacher(int subject)
        {
            if(!service.AddTeacher(AddPerson(), subject))
            {
                Console.WriteLine("\\\\|To many teacher in subject|//");
            }
        }
        private PersoneDTO AddPerson()
        {
            Console.WriteLine("\n-|Create Person|-\n");
            PersoneDTO personeDTO = new PersoneDTO();
            Console.Write("Fist name : ");
            personeDTO.FirstName = Console.ReadLine();
            Console.Write("Last name : ");
            personeDTO.LastName = Console.ReadLine();
            return personeDTO;
        }
        public void AddGroupToteacher()
        {
            string techerList;
            string grouplist;
            string text;
            int tIndex = 0;
            int gIndex = 0;
            int subject = ChoiceSubject();
            techerList= infoService.GetSubjTeacher(subject);
            grouplist = infoService.GetGroup();
            if (techerList == "")
            {
                Console.WriteLine("\\\\|We dont have teacher|//");
                return;
            }else if (grouplist == "")
            {
                Console.WriteLine("\\\\|We dont have group|//");
                return;
            }
            while (true)
            {
                tIndex=NumberCheck("\n-|Choise Teacher|-\n" + techerList, infoService.GetSubjTeacherCount(subject));
                gIndex = NumberCheck("\n-|Choise Group|-\n" + grouplist , infoService.GetGroupCount());
                text = service.AddGroupToTecher(tIndex, gIndex, subject);
                if (""!=text)
                {
                    Console.WriteLine(text + "(exit 0)" );
                    choise = Console.ReadLine();
                    if (choise == "0")
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private EQUIPMENT ChoiceEQUIPMENT()
        {
            int max = 0;
            string text = "";
            string buff = "";
            foreach (EQUIPMENT item in Enum.GetValues(typeof(EQUIPMENT)))
            {
                text += item.ToString().ToLower() + " : " + (int)item + "\n";
                max = (int)item;
            }
            return (EQUIPMENT)NumberCheck("\n -|Choise EQUIPMENT| -\n"+text,max+1);

        }
        private SUBACTIVITIES ChoiceSUBACTIVITIES()
        {
            int max = 0;
            string text = "";
            foreach (SUBACTIVITIES item in Enum.GetValues(typeof(SUBACTIVITIES)))
            {
                text += item.ToString().ToLower() + " : " + (int)item + "\n";
                max = (int)item;
            }
            return (SUBACTIVITIES)NumberCheck("\n -|Choise SUBACTIVITIES| -\n" + text, max + 1);

        }
        private ROOM ChoiceROOM()
        {
            int max = 0;
            string text = "";
            foreach (ROOM item in Enum.GetValues(typeof(ROOM)))
            {
                text += item.ToString().ToLower() + " : " + (int)item + "\n";
                max = (int)item;
            }
            return (ROOM)NumberCheck("\n -|Choise ROOM| -\n" + text, max + 1);

        }
        private void Settings()
        {
            void Message(string message,bool result)
            {
                if (result == false)
                {
                    Console.WriteLine("On "+ message);
                }
                else
                {
                    Console.WriteLine("Off " + message);
                }
            }
            while(true)
            {
                Message("Educational factor: 1", settings.EducationalFactor);
                Message("Educational message: 2", settings.EducationalMes);
                Message("Student information: 3", settings.StudentInfo);
                Console.WriteLine("All change : 4");
                Console.WriteLine("Exit : other symbols");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        settings.EducationalFactorInfoSwitch(DisplayMessage);
                        break;
                    case "2":
                        settings.EducationalMesSwitch(DisplayMessage);
                        break;
                    case "3":
                        settings.StudentInfoSwitch(DisplayMessage);
                        break;
                    case "4":
                        settings.EducationalFactorInfoSwitch(DisplayMessage);
                        settings.EducationalMesSwitch(DisplayMessage);
                        settings.StudentInfoSwitch(DisplayMessage);
                        break;
                    default:
                        return;
                }
            }
            
        }
        private void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
