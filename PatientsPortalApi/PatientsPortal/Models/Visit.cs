using System;

namespace PatientsPortal.Models
{
    public class Visit
    {
        public int ID { get; set; }
        public DateTime VisitDate { get; set; }
        public string Reason { get; set; }
    }
}
