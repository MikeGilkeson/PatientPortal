using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientsPortal.DataContext;
using PatientsPortal.Models;

namespace PatientsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IPatientsContext _context;

        public VisitsController(IPatientsContext context)
        {
            _context = context;
        }

        // GET: api/Visits
        [HttpGet]
        public IEnumerable<Visit> GetVisits()
        {
            return _context.Visits;
        }

        // GET: api/Visits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visit = await _context.Visits.FindAsync(id);

            if (visit == null)
            {
                return NotFound();
            }

            return Ok(visit);
        }
    }
}