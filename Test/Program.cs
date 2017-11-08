using MyData.Infrastructure;
using MyDomain.Entities;
using MyService.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var m1 = new Manhole()
            {
                latManhole = 10.0,
                lngManhole = 20.0,
                ProjectRefId = 12,
                reference = "ZZ",
                TypeCh = "ee"
            };
            var equipmentS=new EquipementService();
            equipmentS.Save();
            Console.WriteLine("finished");
            Console.ReadLine();




        }
    }
}


