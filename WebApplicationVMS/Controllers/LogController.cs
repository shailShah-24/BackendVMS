using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationVMS.Data;
using WebApplicationVMS.model;
using WebApplicationVMS.Repository.Abstract;

namespace WebApplicationVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private ApplicationDbContext _db;
        private readonly IVisitorRepository _visitorRepo;
        public LogController(ApplicationDbContext context, IVisitorRepository visitorRepo)
        {
            _db = context;
            _visitorRepo = visitorRepo;
        }
        [HttpGet]

        public List<LogEntity> GetAllLogin()
        {
            return _db.Log.ToList();
        }
        [HttpPost]
        public ActionResult<LogEntity> AddLog([FromBody] LogEntity logDetails)
        {
            _db.Log.Add(logDetails);
            _db.SaveChanges();

            return Ok(logDetails);
        }
        [HttpPost("addlogsin")]
        public ActionResult<LogEntity> AddLogsin([FromBody] LogInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingLogEntry = _db.Log.FirstOrDefault(log => log.VisitorId == inputModel.VisitorId && log.LoginId == inputModel.LoginId);

            if (existingLogEntry == null)
            {
                // If no existing log entry found, create a new one for time in
                var logEntryIn = new LogEntity
                {
                    LoginId = inputModel.LoginId,
                    VisitorId = inputModel.VisitorId,
                    TimeIn = TimeOnly.FromDateTime(DateTime.Now) // Convert current DateTime to TimeOnly
                };

                // Save the log entry to the database
                _db.Log.Add(logEntryIn);
            }
            else
            {
                // If an existing log entry found, update it with the time out
                existingLogEntry.TimeOut = TimeOnly.FromDateTime(DateTime.Now); // Convert current DateTime to TimeOnly
            }

            // Save changes to the database
            _db.SaveChanges();

            return Ok(new { message = "Log entry updated successfully." });
        }

        // You can implement similar logic for recording time out.
        public class LogInputModel
        {
            public int LoginId { get; set; }
            public int VisitorId { get; set; }
        }
        [HttpGet("jointables")]
        public ActionResult<IEnumerable<LogEntity>> JointTables()
        {
            // Get current date
            var currentDate = DateOnly.FromDateTime(DateTime.Today);

            // Filter logs based on the current date falling within the range of from_D and to_D of the visitor
            var logs = _db.Log
                .Include(log => log.Visitor) // Include VisitorEntity navigation property
                .Where(log =>
                    log.Visitor.from_D <= currentDate &&
                    log.Visitor.to_D >= currentDate
                )
                .ToList();

            if (logs == null || !logs.Any())
            {
                return NotFound();
            }

            return logs;
        }

        

    }
}
