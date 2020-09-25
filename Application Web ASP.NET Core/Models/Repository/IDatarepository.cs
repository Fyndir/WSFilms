using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.Repository
{
    public interface IDatarepository<TEntity>
    {
        public Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        public Task<ActionResult<TEntity>> GetById(int id);
        public Task<ActionResult<TEntity>> GetByString(string str);
        Task Add(TEntity entity);
        Task Update(TEntity entityToUpdate, TEntity entity);
        Task Delete(TEntity entity);
    }
}
