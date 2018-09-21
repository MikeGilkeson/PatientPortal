using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PatientsPortal.AlertRules;
using PatientsPortal.DataContext;
using PatientsPortal.ErrorHandling;
using PatientsPortal.Models;

namespace PatientsPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add Patients Context as in memory db, for real app this would connect to physical db
            services.AddDbContext<IPatientsContext, PatientsContext>(opt => opt.UseInMemoryDatabase("patients"));
            // add alert rules object
            services.AddTransient<IAlertRules, AlertRules.AlertRules>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // load patient info from json
                AddDataFromJson(serviceScope.ServiceProvider.GetService<IPatientsContext>() as PatientsContext);
            }

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }

        private void AddDataFromJson(PatientsContext context)
        {
            var patients = JsonConvert.DeserializeObject<Patient[]>(
                File.ReadAllText(Path.Combine("Data", "SampleData.json")),
                new IsoDateTimeConverter {DateTimeFormat = "M/d/yyyy"} // Dates in Json are in M/d/yyyy format
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
                    .Select(v => (DateTime?) v.VisitDate)
                    .FirstOrDefault();
                patient.NextVisit = patient.Visits?
                    .Where(v => v.VisitDate > now)
                    .OrderBy(v => v.VisitDate)
                    .Select(v => (DateTime?)v.VisitDate)
                    .FirstOrDefault();
            }
        }
    }
}
