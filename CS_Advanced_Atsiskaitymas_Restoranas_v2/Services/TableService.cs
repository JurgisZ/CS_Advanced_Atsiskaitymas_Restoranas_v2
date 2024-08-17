using CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Services
{
    internal class TableService
    {
        private readonly IRepository<Table> _repository;
        public TableService(IRepository<Table> repository)
        {
            _repository = repository;
        }
        public List<Table>? GetAvailableTables()
        {
            var tables = _repository.GetAll().Where(x => !x.Disabled).ToList();
            tables = tables.OrderBy(x => x.TableNumber).ToList();
            return tables;
        }
        public Table? GetById(int id) 
        { 
            return _repository.GetById(id);
        }
        public void Update(Table table) 
        { 
            _repository.Update(table);
        }
    }
}
