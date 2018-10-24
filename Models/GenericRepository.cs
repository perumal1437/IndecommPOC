using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data;
using System.Web;

namespace IndecommPOC.Models
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity:class 
    {

        internal PropertiesEntities _propertiesentities;
        DbSet<TEntity> _dbset;
        public GenericRepository(PropertiesEntities propertiesentities)
        {
            this._propertiesentities = propertiesentities;
            _dbset = _propertiesentities.Set<TEntity>();


        }        
        public IEnumerable<TEntity> GetAllRecords()
        {
            return _dbset.ToList();
        }
        public void Add(TEntity entity)
        {

             _dbset.Add(entity);
        }

        public void Update(TEntity entity)
        {

            _dbset.Attach(entity);
            _propertiesentities.Entry(entity).State = EntityState.Modified;
        }

        public TEntity GetFirstorDefault(int? recordId)
        {

            return _dbset.Find(recordId);

        }

        public void Delete(TEntity entity)
        {
            if(_propertiesentities.Entry(entity).State==EntityState.Detached)
            { _dbset.Attach(entity); }
            _dbset.Remove(entity);
        }

    }
}