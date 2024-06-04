using Microsoft.AspNetCore.Mvc;
using MVCCRUD.Models;
using MVCCRUD.Data;
using System.Collections.Generic;

namespace MVCCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly databaseContext _dbContext;

        public WorkersController(databaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Workers
        [HttpGet]
        public ActionResult<IEnumerable<Worker>> GetWorkers()
        {
            var workers = _dbContext.GetWorkers();
            return Ok(workers);
        }

        // GET: api/Workers/{id}
        [HttpGet("{id}")]
        public ActionResult<Worker> GetWorker(int id)
        {
            var worker = _dbContext.GetWorker(id);
            if (worker == null)
            {
                return NotFound();
            }
            return Ok(worker);
        }
        
        // POST: api/Workers
        [HttpPost]
        public ActionResult<Worker> CreateWorker([FromBody] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _dbContext.CreateWorker(worker);
                return CreatedAtAction(nameof(GetWorker), new { id = worker.id }, worker);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Workers/update
        [HttpPut]
        public IActionResult UpdateWorker([FromBody] Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.UpdateWorker(worker.id, worker);
            return NoContent();
        }

        // DELETE: api/Workers/delete/{id}
        [HttpDelete]
        public IActionResult DeleteWorker(int id)
        {
            _dbContext.DeleteWorker(id);
            return NoContent();
        }
    }
}
