using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PatientsPortal.Controllers;
using PatientsPortal.DataContext;
using PatientsPortal.Models;
using Xunit;

namespace PatientsPortal.Tests
{
    public class PatientsControllerTests
    {
        private IPatientsContext GetPatientsContext()
        {
            var context = new PatientsContext(
                new DbContextOptionsBuilder<PatientsContext>()
                    .UseInMemoryDatabase("patients")
                    .Options);
            AddDataFromJson(context);
            return context;
        }
        private void AddDataFromJson(PatientsContext context)
        {
            var patients = JsonConvert.DeserializeObject<Patient[]>(
                File.ReadAllText(Path.Combine("Data", "SampleData.json")),
                new IsoDateTimeConverter { DateTimeFormat = "M/d/yyyy" } // Dates in Json are in M/d/yyyy format
            );
            PopulateLastAndNextVisit(patients);
            context.Patients.AddRange(patients);
            context.SaveChanges();
        }

        // calculate the last and next visit at load time for demo purposes. Depending on how often data is load this could become stale since we're using DateTime.Now
        private void PopulateLastAndNextVisit(Patient[] patients)
        {
            var now = DateTime.Now;
            foreach (var patient in patients)
            {
                patient.LastVisit = patient.Visits?
                    .Where(v => v.VisitDate < now)
                    .OrderByDescending(v => v.VisitDate)
                    .Select(v => (DateTime?)v.VisitDate)
                    .FirstOrDefault();
                patient.NextVisit = patient.Visits?
                    .Where(v => v.VisitDate > now)
                    .OrderBy(v => v.VisitDate)
                    .Select(v => (DateTime?)v.VisitDate)
                    .FirstOrDefault();
            }
        }
        [Fact]
        public void NullContextTest()
        {
            var patientController = new PatientsController(null, null);
            Assert.Throws<NullReferenceException>(() =>
            {
                patientController.GetPatients();
            });
        }

        [Fact]
        public void GetPatientsTest()
        {
            var patientController = new PatientsController(GetPatientsContext(), new AlertRules.AlertRules());
            var patients = patientController.GetPatients();
            Assert.NotNull(patients);
            Assert.Equal(5, patients.Count());
            Assert.Equal(1, patients.First().ID);
        }

        [Fact]
        public async void GetPatientTest()
        {
            var patientController = new PatientsController(GetPatientsContext(), new AlertRules.AlertRules());
            var result = await patientController.GetPatient(1);
            Assert.NotNull(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var patient = okResult.Value as Patient;
            Assert.NotNull(patient);
            Assert.Equal(1, patient.ID);
            Assert.Single(patient.Alerts);
            Assert.Equal(3, patient.Visits.Count());
        }
    }
}
