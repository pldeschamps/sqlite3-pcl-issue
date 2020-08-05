using System;
using System.Collections.Generic;
using System.Text;

namespace AlmicantaratXF.Model
{
    public class MakeCorrection
    {
        public static float MakeConstantCorrection(List<ConstantCorrection> ccs, float altitude)
        {
            float seconds = 0;
            if (ccs.Count > 1 && altitude >= (float)ccs[0].At)
            {
                ushort i = 0;
                while (altitude >= (float)ccs[i+1].At && i+2 < ccs.Count)
                    i++;
                ConstantCorrection lcc = ccs[i];
                ConstantCorrection hcc = ccs[i+1];
                if (altitude >= (float)hcc.At)
                    seconds = (float)hcc.Correction;
                else
                {
                    seconds = (float)lcc.Correction;
                    if ((hcc.At - lcc.At) != 0)
                        seconds += (altitude - (float)lcc.At) *
                                   (float)(hcc.Correction - lcc.Correction) /
                                   (float)(hcc.At - lcc.At);
                }
            }
            return seconds/60;
        }
        public static float MakeRefraction(float altitude, float pressure = 1010, float temperature = 10)
        {
            // refraction = 0.98 * 1 / tan(Altitude)
            if (Math.Cos(altitude * Math.PI / 180) > 0.98)
                altitude = 10;
            else if (Math.Cos(altitude * Math.PI / 180) < 0.02)
                altitude = 89;
            if (temperature < -50) temperature = -50;
            return (float)(0.98 / Math.Tan(altitude * Math.PI / 180))
                * pressure * 283 / (1010 * (273 + temperature));
        }
    }
}
