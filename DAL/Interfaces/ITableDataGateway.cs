using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface ITableDataGateway<T> where T:EntityBase
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
