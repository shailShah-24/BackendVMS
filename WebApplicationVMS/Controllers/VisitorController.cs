using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationVMS.Data;
using WebApplicationVMS.model;
using WebApplicationVMS.Repository.Abstract;

namespace WebApplicationVMS.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IFileServices _fileServices;
        private readonly IVisitorRepository _visitorRepo;
        private ApplicationDbContext _db;

        public VisitorController(IFileServices fs, IVisitorRepository visitorRepo, ApplicationDbContext db)
        {
            this._fileServices = fs;
            this._visitorRepo = visitorRepo;
            this._db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var visitors = _visitorRepo.GetAll();   // Adjust this based on your repository implementation.
            if (visitors == null)
            {
                return NotFound();
            }
            // Adjust image paths for all visitors
            foreach (var visitor in visitors)
            {
                visitor.image = Url.Content("~/Uploads/" + visitor.image);
            }
            return Ok(visitors);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var visitors = _visitorRepo.GetAll().Where(v => v.LoginId == id).ToList();
            if (visitors == null || visitors.Count == 0)
            {
                return NotFound();
            }

            // Adjust image paths if necessary for all visitors
            foreach (var visitor in visitors)
            {
                visitor.image = Url.Content("~/Uploads/" + visitor.image);
            }

            return Ok(visitors);
        }


        [HttpPost]
        public IActionResult Add([FromForm]VisitorEntity model)
        {
            if(model.ImageFile != null)
            {
                var result= _fileServices.SaveImage(model.ImageFile);
                if(result.Item1==1)
                {
                    model.image= result.Item2;
                }
                var visitorResult=_visitorRepo.Add(model);
            }
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var visitor = _visitorRepo.GetAll().FirstOrDefault(v => v.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            var deleteResult = _visitorRepo.Delete(visitor);
            if (!deleteResult)
            {
                // Handle deletion failure, return appropriate status code or message
                return StatusCode(500, "Failed to delete visitor.");
            }

            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] VisitorUpdateDto updateDto)
        {
            var existingVisitor = _visitorRepo.Get(id);
            if (existingVisitor == null)
            {
                return NotFound();
            }

            // Update the properties of existingVisitor with the properties in the update DTO
            existingVisitor.f_name = updateDto.f_name ?? existingVisitor.f_name;
            existingVisitor.l_name = updateDto.l_name ?? existingVisitor.l_name;
            existingVisitor.email_id = updateDto.email_id ?? existingVisitor.email_id;
            existingVisitor.address = updateDto.address ?? existingVisitor.address;
            existingVisitor.phone_no = updateDto.phone_no ?? existingVisitor.phone_no;
            existingVisitor.purpose = updateDto.purpose ?? existingVisitor.purpose;
            existingVisitor.company = updateDto.company ?? existingVisitor.company;
            existingVisitor.department = updateDto.department ?? existingVisitor.department;
            if (updateDto.from_D.HasValue)
            {
                existingVisitor.from_D = updateDto.from_D.Value;
            }

            if (updateDto.to_D.HasValue)
            {
                existingVisitor.to_D = updateDto.to_D.Value;
            }

            if (updateDto.time.HasValue)
            {
                existingVisitor.time = updateDto.time.Value;
            }
            var updatedVisitor = _visitorRepo.Update(existingVisitor);
            return Ok(updatedVisitor);
        }

        public class VisitorUpdateDto
        {
            public string? f_name { get; set; }
            public string? l_name { get; set; }
            public string? email_id { get; set; }
            public string? address { get; set; }
            public string? phone_no { get; set; }
            public string? purpose { get; set; }
            public string? company { get; set; }
            public string? department { get; set; }
            public DateOnly? from_D { get; set; }
            public DateOnly? to_D { get; set; }
            public TimeOnly? time { get; set; }
        }
        [HttpGet("GetVisitorsByCurrentDate")]
        public IActionResult GetVisitorsByCurrentDate()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Today);

            // Filter visitors based on the current date falling within the range of from_D and to_D
            var visitors = _visitorRepo.GetAll()
                .Where(v => v.from_D <= currentDate && v.to_D >= currentDate)
                .ToList();

            if (visitors == null || !visitors.Any())
            {
                return NotFound();
            }

            // Adjust image paths if necessary for all visitors
            foreach (var visitor in visitors)
            {
                visitor.image = Url.Content("~/Uploads/" + visitor.image);
            }

            return Ok(visitors);
        }
        [HttpGet("jointables")]
        public ActionResult<IEnumerable<VisitorLogDto>> JointTables()
        {
            // Perform outer join between VisitorEntity and LogEntity
            var visitorLogs = _visitorRepo.GetAll()
        .GroupJoin(
            _db.Log,
            visitor => visitor.Id,
            log => log.VisitorId,
            (visitor, logs) => new { Visitor = visitor, Logs = logs }
        )
        .SelectMany(
            result => result.Logs.DefaultIfEmpty(),
            (visitor, log) => new VisitorLogDto
            {
                VisitorId = visitor.Visitor.Id,
                VisitorFirstName = visitor.Visitor.f_name,
                VisitorLastName = visitor.Visitor.l_name,
                VisitorEmail = visitor.Visitor.email_id,
                VisitorAddress = visitor.Visitor.address,
                VisitorPhone = visitor.Visitor.phone_no,
                VisitorPurpose = visitor.Visitor.purpose,
                VisitorCompany = visitor.Visitor.company,
                VisitorDepartment = visitor.Visitor.department,
                VisitorFrom_D=visitor.Visitor.from_D ,
                VisitorTo_D=visitor.Visitor.to_D ,
                VisitorTime=visitor.Visitor.time,
                TimeIn = log != null ? log.TimeIn : null,
                TimeOut = log != null ? log.TimeOut : null,
                LoginId=log!=null ? log.LoginId : null,
            })
        .ToList();

            if (visitorLogs == null || !visitorLogs.Any())
            {
                return NotFound();
            }

            return visitorLogs;
        }

        // Define VisitorLogDto class to hold the combined data
        public class VisitorLogDto
        {
            public int VisitorId { get; set; }
            public string VisitorFirstName { get; set; }
            public string VisitorLastName { get; set; }
            public string VisitorEmail { get; set; }
            public string VisitorAddress { get; set; }
            public string VisitorPhone { get; set; }
            public string VisitorPurpose { get; set; }
            public string VisitorCompany { get; set; }
            public string VisitorDepartment { get; set; }
            public DateOnly FromDate { get; set; }
            public DateOnly ToDate { get; set; }
            public TimeOnly VisitorTime { get; set; }
            public string ImagePath { get; set; }
            public DateOnly VisitorFrom_D { get; set; }
            public DateOnly VisitorTo_D { get; set; }

            public int? LoginId { get; set; }
            public TimeOnly? TimeIn { get; set; } // Nullable TimeOnly
            public TimeOnly? TimeOut { get; set; } // Nullable TimeOnly
        }

    }
}
