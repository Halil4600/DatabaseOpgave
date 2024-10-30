using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDatabaseOpgave6
{
    public class Facility
    {
        public int Facility_no { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"FacilityNo: {Facility_no}, Name: {Name}";
        }
    }

}
