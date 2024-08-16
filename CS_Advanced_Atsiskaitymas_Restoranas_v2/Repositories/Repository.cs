using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories
{
    internal class Repository<T> : IRepository<T> where T : EntityBase//, new()
    {
        protected readonly string _filePath;

        public Repository(string filePath)
        {
            _filePath = filePath;
        }
        public void Create(T entity)
        {

        }
        public List<T> GetAll()
        {
            List<T> entities = default;
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            if(constructor == null)
                //log error
                return entities;
                       
            try
            {
                using (var reader = new StreamReader(_filePath))
                {
                    string csvLine;
                    while(null != (csvLine = reader.ReadLine()))
                    {
                        entities.Add((T)constructor.Invoke(new object[] { csvLine }));
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return entities;
        }
        public T GetById(int id)
        {
            return default;
        }

        public int GetLastId(int id)
        {
            return default;
        }
        public bool Update(T entity)
        {
            return default;
        }
        public bool Delete(T entity)
        {
            return default;
        }
    }
}
