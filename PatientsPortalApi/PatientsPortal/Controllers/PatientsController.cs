using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientsPortal.AlertRules;
using PatientsPortal.DataContext;
using PatientsPortal.Models;

namespace PatientsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsContext _context;

        public PatientsController(IPatientsContext context, IAlertRules alertRules)
        {
            _context = context;
            _context?.AddAlertRules(alertRules);
        }

        // GET: api/Patients
        [HttpGet]
        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = _context.Patients.Include(p => p.Visits);
            var patient = await query.SingleOrDefaultAsync(p => p.ID == id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}