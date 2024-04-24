using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationVMS.model
{
    public class LogEntity
    {  
        public int ID { get; set; }
        public int LoginId { get; set; }
        public virtual login? Login { get; set; } // Navigation property

        // Foreign Key to Visitor (if applicable)
        public int? VisitorId { get; set; }
        public virtual VisitorEntity? Visitor { get; set; } // Navigation property

        public TimeOnly TimeIn { get; set; }
        public  TimeOnly TimeOut { get; set; }

       
    }
}
