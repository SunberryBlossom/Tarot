using System;
using System.Collections.Generic;
using System.Text;
using TheSeer2.Models;
using TheSeer2.Models.Enums;

namespace TheSeer2.Interfaces
{
    internal interface IDataService
    {
        User? GetUser(string username);
        void SaveUser(User user);
        List<User> GetAllUsers();

        void SaveReading(Reading reading);
        List<Reading> GetUserReadings(Guid userId);

    }
}
