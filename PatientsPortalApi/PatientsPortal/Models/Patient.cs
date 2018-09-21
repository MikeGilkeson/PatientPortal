using System;
using System.Collections.Generic;
using PatientsPortal.AlertRules;

namespace PatientsPortal.Models
{
    public class Patient
    {
        private IAlertRules _alertRules;

        public void AddAlertRules(IAlertRules alertRules)
        {
            _alertRules = alertRules;
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string EthnicityCode { get; set; }
        public ICollection<Visit> Visits { get; set; }
        public DateTime? LastVisit { get; set; }
        public DateTime? NextVisit { get; set; }

        public ICollection<(string, string)> Alerts => _alertRules?.GetAlerts(this);
    }
}
