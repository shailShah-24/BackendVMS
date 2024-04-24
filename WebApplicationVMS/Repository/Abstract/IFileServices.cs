namespace WebApplicationVMS.Repository.Abstract
{
    public interface IFileServices
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
