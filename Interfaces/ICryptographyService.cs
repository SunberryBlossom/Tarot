using System;
using System.Collections.Generic;
using System.Text;

namespace TheSeer2.Interfaces
{
    internal interface ICryptographyService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
