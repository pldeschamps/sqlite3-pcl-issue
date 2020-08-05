using System;
using System.Collections.Generic;
using System.Text;

namespace AlmicantaratXF.Model
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public Sextant Sextant { get; set; }
        public Profile()
        {
            FirstName = "Observer First Name";
            SurName = "Observer Surname";
            Sextant = new Sextant();
        }
    }
}
