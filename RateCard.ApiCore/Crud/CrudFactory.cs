using RateCard.ApiCore.Mappers;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.ApiCore.Crud
{
    public abstract class CrudFactory
    {

        protected EntityMapper EntityMapper { get; set; }

        public abstract bool Create(BaseEntity entity);
        public abstract T RCreate<T>(BaseEntity entity) where T : class;
        public abstract T Retrieve<T>(BaseEntity entity) where T : class;
        public abstract List<T> RetrieveAll<T>() where T : class;
        public abstract bool Update(BaseEntity entity);
        public abstract T RUpdate<T>(BaseEntity entity) where T : class;
        public abstract bool Delete(BaseEntity entity);
    }
}
