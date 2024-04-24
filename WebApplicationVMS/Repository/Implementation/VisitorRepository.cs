using WebApplicationVMS.Data;
using WebApplicationVMS.model;
using WebApplicationVMS.Repository.Abstract;

namespace WebApplicationVMS.Repository.Implementation
{
    public class VisitorRepository:IVisitorRepository
    {
        private readonly ApplicationDbContext _context;
        public VisitorRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public bool Add(VisitorEntity model)
        {
            try
            {
                _context.Visitor.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }

        public List<VisitorEntity> GetAll()
        {
            try
            {
                return _context.Visitor.ToList();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                return null;
            }
        }
        public bool Delete(VisitorEntity model)
        {
            try
            {
                _context.Visitor.Remove(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                return false;
            }
        }
        public VisitorEntity Update(VisitorEntity visitor)
        {
            var existingVisitor = _context.Visitor.Find(visitor.Id);
            if (existingVisitor == null)
            {
                return null; // Or throw exception, handle it based on your requirements
            }

            // Update the properties of existingVisitor with visitor's properties
            existingVisitor.f_name = visitor.f_name;
            existingVisitor.l_name = visitor.l_name;
            existingVisitor.email_id = visitor.email_id;
            existingVisitor.address = visitor.address;
            existingVisitor.phone_no = visitor.phone_no;
            existingVisitor.purpose = visitor.purpose;
            existingVisitor.company = visitor.company;
            existingVisitor.department = visitor.department;
            existingVisitor.from_D = visitor.from_D;
            existingVisitor.to_D = visitor.to_D;
            existingVisitor.time = visitor.time;

            _context.SaveChanges();
            return existingVisitor;
        }
        public VisitorEntity Get(int id)
        {
            return _context.Visitor.FirstOrDefault(v => v.Id == id);
        }
    }
}
