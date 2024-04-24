using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationVMS.model
{
    public class VisitorEntity
    {
        [Key]
        public int Id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string email_id { get; set;}
        public string address { get; set;}
        public string phone_no { get; set;}
        public string purpose { get; set;}
        public string company { get; set;}
        public string department { get; set;}
        public DateOnly from_D { get; set;}
        public DateOnly to_D { get;set;}
        public TimeOnly time { get; set;}
        public string? image { get; set;}
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
      
        public int? LoginId { get; set;}

        [ForeignKey("LoginId")]
        public virtual login? Login { get; set; }

    }
}
