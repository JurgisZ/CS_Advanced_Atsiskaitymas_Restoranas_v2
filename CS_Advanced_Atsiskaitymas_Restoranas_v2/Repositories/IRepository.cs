
namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories
{
    internal interface IRepository<T>
    {
        void Create(T entity);
        bool Delete(T entity);
        public List<T> GetAll();
        public T GetById(int id);
        public int GetLastId(int id);
        bool Update(T entity);
    }
}