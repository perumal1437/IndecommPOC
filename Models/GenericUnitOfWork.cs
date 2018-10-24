using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IndecommPOC.Models
{
    public class GenericUnitOfWork:IDisposable
    {
        private PropertiesEntities _propertiesentities = new PropertiesEntities();       

        private GenericRepository<Property> propertyRepository;      

        public GenericRepository<Property> PropertyRepository
        {

            get
            {
                if (this.propertyRepository == null)
                {
                    this.propertyRepository = new GenericRepository<Property>(_propertiesentities);
                }
                return propertyRepository;
            }

        }
       

        public void Save()
        {
            _propertiesentities.SaveChanges();

        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _propertiesentities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}