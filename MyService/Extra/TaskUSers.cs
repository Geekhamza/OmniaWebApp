using MyDomain.Entities;
using MyService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService.Extra
{
    public  class TaskUSers
    {
        public static bool isAuthorized(long taskID, string userID)
        {
            var  taskS = new TacheService();

            Tache t = taskS.Get(taskID);

            List<string> authorizedUsers = new List<string>();
            char[] delimiterChars = { ',' };
            if (t.AffectedUSers == null)
                t.AffectedUSers = "";
            string[] AuthorizedUsers = t.AffectedUSers.Split(delimiterChars);
            foreach (string id in AuthorizedUsers)
            {
                if (userID == id)
                {
                    return true;
                }
            }
            return false;
        }
        public static List<string> AuthorizedUsers(long taskID)
        {

            var taskS = new TacheService();

            Tache t = taskS.Get(taskID);
            List<string> users = new List<string>();
            

            List<string> authorizedUsers = new List<string>();
            char[] delimiterChars = { ',' };
            string[] AuthorizedUsers = t.AffectedUSers.Split(delimiterChars);
            return AuthorizedUsers.ToList();
        }
        public static void AddUsers(List<string> users, long TaskID)
        {
            var taskS = new TacheService();

            Tache t = taskS.Get(TaskID);
            foreach (var u in users)
            {
                if (!isAuthorized(TaskID, u))
                {
                    t.AffectedUSers  += "," + u;
                    taskS.Update(t);
                    taskS.Save();
                }
            }
        }
        public static void DeleteUsers(List<string> users, long TaskID)
        {
            var taskS = new TacheService();

            Tache t = taskS.Get(TaskID);
            foreach (var u in users)
            {
                if (isAuthorized(TaskID, u))
                {
                    t.AffectedUSers .Replace("," + u, "");
                    taskS.Update(t);
                    taskS.Save();
                }
            }
        }

    }
}
