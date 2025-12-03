using System;
using System.Collections.Generic;
using System.Text;
using TheSeer.Models;
using TheSeer.Models.Enums;

namespace TheSeer.Interfaces
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
