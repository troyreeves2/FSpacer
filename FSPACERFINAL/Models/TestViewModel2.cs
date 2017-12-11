using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSPACERFINAL.Models
{
    public class TestViewModel2
    {

        public int ID { get; set; }
        public bool Active { get; set; }

       
        public int HorizontalGearCaseDeviation { get; set; }
        public int HorizontalCarrierDeviation { get; set; }
        public int Bearing { get; set; }
        public float HMDGear { get; set; }
        

        public int VerticalGearCaseDeviation { get; set; }
        public int VerticalCarrierDeviation { get; set; }
        public float GearMount { get; set; }
        public float VMDGear { get; set; }
     
        public string DriveNumber { get; set; }
       
        public int OperatorID { get; set; }
       
        public DateTime Date { get; set; }
    
    }
}
