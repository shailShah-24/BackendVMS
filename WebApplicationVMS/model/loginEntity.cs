using System.ComponentModel.DataAnnotations;

namespace WebApplicationVMS.model
{
    public class login
    {
        [Key]
        public int Id { get; set; }
       public string Emp_id { get; set; }
        public string Password { get; set;}
     public string role { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set;}
        public string email { get; set;}
        public string phone_no { get; set;}
        public string department { get; set; }

    }
}
