namespace WebService.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        T GetById(int id);
    }
}
