using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndecommPOC.Models
{
    interface IGenericRepository<TEntity>
    {
        IEnumerable<TEntity> GetAllRecords();
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity GetFirstorDefault(int? recordId);

        void Delete(TEntity entity);
    }
}
