using StudySimulation.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudySimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            University university = new University();
            Service service = new Service(university);
            InfoService infoService = new InfoService(university);
            Settings settings = new Settings(university);
            Menu menu = new Menu(service,settings,infoService);
            menu.MainMenu();
        }
        
    }
}
