using WebApplicationVMS.model;

namespace WebApplicationVMS.Repository.Abstract
{
    public interface IVisitorRepository
    {
        bool Add(VisitorEntity model);
        List<VisitorEntity> GetAll();
        bool Delete(VisitorEntity model);
        VisitorEntity Update(VisitorEntity visitor);
        VisitorEntity Get(int id);
    }
}
