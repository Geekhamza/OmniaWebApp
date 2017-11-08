using MyData.Infrastructure;
using MyDomain.Entities;
using MyService.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var pservice = new ProjectService();
            var eservice = new EquipementService();

            var manhole1 = (Manhole)eservice.GetEquipementById(1);
            var manhole2 = (Manhole)eservice.GetEquipementById(2);
            var manhole3 = (Manhole)eservice.GetEquipementById(3);
            var manhole4 = (Manhole)eservice.GetEquipementById(4);
            manhole2.ConnectedManholesOUT.Add(manhole3);
            manhole3.ConnectedManholesOUT.Add(manhole4);
            manhole2.ConnectedManholesOUT.Add(manhole4);
            eservice.Save();
            Console.WriteLine(manhole2.ConnectedManholesOUT.Count());
            Console.WriteLine(manhole3.ConnectedManholesOUT.Count());

            Console.WriteLine("finished");
            Console.ReadLine();

              
         
          
        }
    }
}


