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
        public List<T>? GetAll()
        {
            List<T> entities = new List<T>();
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            if (constructor == null)
                //log error
                return default;
                       
            try
            {
                using (var reader = new StreamReader(_filePath))
                {
                    string csvLine;
                    while(null != (csvLine = reader.ReadLine()))
                    {
                        Console.WriteLine($"CSV {csvLine}");
                        var entity = (T)constructor?.Invoke(new object[] { csvLine });
                        if(entity != null) 
                            entities.Add(entity);
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return entities == null ? default : entities;
        }
        public T? GetById(int id)
        {
            T entity = GetAll().Find(x => x.Id == id);
            return entity;
        }

        public int GetLastId()
        {
            List<T> entityList = GetAll()?.OrderByDescending(x => x.Id).ToList();
            if (entityList.Count > 0)
                return entityList[0].Id;

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
