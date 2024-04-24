using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplicationVMS.Data;
using WebApplicationVMS.model;

namespace WebApplicationVMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class LoginController : ControllerBase
    {
        private ApplicationDbContext _db;
        public LoginController(ApplicationDbContext context )
        {
            _db=context;
        }
        [HttpGet]

        public List<login> GetAllLogin()
        {
            return _db.Login.ToList();
        }
        [HttpPost]
        public ActionResult<login> AddLogin([FromBody] login loginDetails)
        {
            _db.Login.Add(loginDetails);
            _db.SaveChanges();

            return Ok(loginDetails);
        }
        [HttpPost("UpdateDetails")]
        public ActionResult<login> UpdateLogin( Int32 Id,[FromBody] login loginDetails)
        {
            var loginEmpDetails = _db.Login.FirstOrDefault(x => x.Id == Id);
            loginEmpDetails.f_name=loginDetails.f_name;
            loginEmpDetails.l_name=loginDetails.l_name;
            loginEmpDetails.email=loginDetails.email;
            loginEmpDetails.phone_no=loginDetails.phone_no;
            loginEmpDetails.department=loginDetails.department;

            _db.SaveChanges();

            return Ok(loginEmpDetails);
        }
        [HttpPut("DeleteEmployee")]
        public ActionResult<login> Delete(Int32 Id)
        {
            var loginEmpDetails = _db.Login.FirstOrDefault(x => x.Id == Id);
            _db.Remove(loginEmpDetails);
           

            _db.SaveChanges();

            return NoContent();
        }
        [HttpPost("validate")]
        public ActionResult<login> Login([FromBody] LoginRequest request)
        {
            var resp = _db.Login.FirstOrDefault(l => l.Emp_id == request.Emp_id && l.Password == request.Password);
            if (resp == null)
            {
                return NotFound("Invalid username or password"); // Return error message for invalid credentials
            }
            Dictionary<string, object> user = new Dictionary<string, object>();
            user.Add("id", resp.Id);
            user.Add("role", resp.role);
            LoginResponse response = new LoginResponse
            {
                accessToken = "abcd", 
                user = new Dictionary<string, object>
        {
            { "id", resp.Id },
                    {"role",resp.role },
        }
            };


            if (resp == null)
            {
                return NotFound("Invalid username or password");
            }
            return Ok(response);
        }
    }
    public class LoginRequest
    {
        public string Emp_id { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string accessToken { get; set; }
        public Dictionary<string, object> user { get; set; }
    }
         
}
