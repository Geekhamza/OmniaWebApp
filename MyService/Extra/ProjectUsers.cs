using MyDomain.Entities;
using MyService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.Extra
{
    public class ProjectUsers
    {
        public static bool isAuthorized(long projectId, string userID)
        {
            IProjectService projectS = new ProjectService();

            Project p = projectS.Get(projectId);

            List<string> authorizedUsers = new List<string>();
            char[] delimiterChars = { ',' };
            if (p.Users == null)
                p.Users = "";
            string[] AuthorizedUsers = p.Users.Split(delimiterChars);
            foreach (string id in AuthorizedUsers)
            {
                if (userID == id)
                {
                    return true;
                }
            }
            return false;
        }
        public static List<string> AuthorizedUsers(long projectId)
        {
            IProjectService projectS = new ProjectService();

            List<string> users = new List<string>();
            Project p = projectS.Get(projectId);

            List<string> authorizedUsers = new List<string>();
            char[] delimiterChars = { ',' };
            string[] AuthorizedUsers = p.Users.Split(delimiterChars);
            return AuthorizedUsers.ToList();
        }
        public static void AddUsers(List<string> users, long projectId)
        {
            IProjectService projectS = new ProjectService();
           

            Project p = projectS.Get(projectId);
            foreach (var  u in users)
            {
                if (!isAuthorized(projectId, u))
                {
                    p.Users += "," + u;
                    projectS.Update(p);
                    projectS.Save();
                }
            }
        }
        public static void DeleteUsers(List<string> users, long projectId)
        {
            IProjectService projectS = new ProjectService();
            

            Project p = projectS.Get(projectId);
            foreach (var u in users)
            {
                if (isAuthorized(projectId, u))
                {
                    p.Users.Replace("," + u, "");
                    projectS.Update(p);
                    projectS.Save();
                }
            }
        }
    }
}