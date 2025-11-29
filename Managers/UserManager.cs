using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSeer.Models;

namespace TheSeer.Managers
{
    internal static class UserManager
    {
        private static List<User> RegisteredUsers = new List<User>();

        public static void CreateUser(string username, string password, string email, string nickname)
        {
            throw new NotImplementedException();
        }

        public static void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public static void AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public static void UpdateUserDetails(string detailToUpdate)
        {
            throw new NotImplementedException();
        }

        public static void GetUserDetails(string username, string detailWanted)
        {
            throw new NotImplementedException();
        }

        public static void GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public static void GetUser(string username)
        {

        }
    }
}
