using System;
using System.Collections.Generic;
using PatientsPortal.Models;

namespace PatientsPortal.AlertRules
{
    public class AlertRules : IAlertRules
    {
        public ICollection<(string, string)> GetAlerts(Patient patient)
        {
            // hardcoding rules, they should be stored in a db or config
            var alerts = new List<(string, string)>();
            var today = DateTime.Today;
            var age = today.Year - patient.DateOfBirth.Year;
            if (today.DayOfYear < patient.DateOfBirth.DayOfYear)
                age = age - 1;
            if (age >= 65 && (!patient.LastVisit.HasValue || today > patient.LastVisit.Value.AddMonths(6)))
            {
                alerts.Add(("warn", "Patient is over 65 recommend flu shot"));
            }
            if (!patient.NextVisit.HasValue && (!patient.LastVisit.HasValue || today > patient.LastVisit.Value.AddMonths(9)))
            {
                alerts.Add(("primary", "Remind patient to schedule a checkup"));
            }
            if (patient.Gender == "M" && (patient.EthnicityCode == "2080-0" || patient.EthnicityCode == "2079-2"))
            {
                alerts.Add(("accent", "Ask patient if they’d be willing to participate in an upcoming medical study"));
            }
            return alerts;
        }
    }
}
