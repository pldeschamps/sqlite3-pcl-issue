using System;
using System.Collections.Generic;
using System.Text;

namespace AlmicantaratXF.Model
{
    public class Vessel
    {
        public string IMO { get; set; }
        public string Name { get; set; }
        public string CallSign { get; set; }
        public string Flag { get; set; }
        public Vessel()
        {
            IMO = "No IMO";
            Name = "Vessel Name";
            CallSign = "Call Sign";
            Flag = "ISO 3166 two-letter country code";
        }
    }
}
