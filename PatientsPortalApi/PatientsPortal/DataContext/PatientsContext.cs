using Microsoft.EntityFrameworkCore;
using PatientsPortal.AlertRules;
using PatientsPortal.Models;

namespace PatientsPortal.DataContext
{
    public class PatientsContext : DbContext, IPatientsContext
    {
        public PatientsContext(DbContextOptions options) : base(options)
        {
            
        }

        public void AddAlertRules(IAlertRules alertRules)
        {
            foreach (var patient in Patients)
            {
                patient.AddAlertRules(alertRules);
            }
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
