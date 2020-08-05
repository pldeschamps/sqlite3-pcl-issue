using System;
using System.Collections.Generic;
using System.Text;

namespace AlmicantaratXF.Model
{
    public class Sextant
    {
        public string Manufacturer { get; set; }
        public string Id { get; set; }
        public Sextant()
        {
            Manufacturer = "Manufacturer";
            Id = "Sextant Id #";
        }
    }
}
