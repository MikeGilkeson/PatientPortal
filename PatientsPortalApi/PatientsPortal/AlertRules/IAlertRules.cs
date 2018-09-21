using System.Collections.Generic;
using PatientsPortal.Models;

namespace PatientsPortal.AlertRules
{
    public interface IAlertRules
    {
        ICollection<(string, string)> GetAlerts(Patient patient);
    }
}