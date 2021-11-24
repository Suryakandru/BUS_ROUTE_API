using System.Collections.Generic;

namespace ProjectWebAPI.Models
{
    public class BusViewModel
    {       
        public string BusID { get; set; }
                
        public string BusName { get; set; }
        
        public string Frequency { get; set; }
       
        public string BusNumber { get; set; }
       
        public string StartTime { get; set; }
       
        public string EndTime { get; set; }
       
        public string StartPlace { get; set; }
        
        public string EndPlace { get; set; }        

    }

    public class BusscheduleViewModel
    {
        public string BusID { get; set; }

        public string BusName { get; set; }        

        public string BusNumber { get; set; }

        public List<string> ScheduledTime { get; set; }        

        public string StartPlace { get; set; }

        public string EndPlace { get; set; }

    }
}
