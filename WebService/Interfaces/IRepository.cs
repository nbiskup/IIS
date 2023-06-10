namespace WebService.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<IList<T>> GetAllXml();
        T GetById(int id);
    }
}
