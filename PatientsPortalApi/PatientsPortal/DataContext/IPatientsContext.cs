using System;
using Microsoft.EntityFrameworkCore;
using PatientsPortal.AlertRules;
using PatientsPortal.Models;

namespace PatientsPortal.DataContext
{
    public interface IPatientsContext : IDisposable
    {
        void AddAlertRules(IAlertRules alertRules);
        DbSet<Patient> Patients { get; }
        DbSet<Visit> Visits { get; set; }
    }
}
