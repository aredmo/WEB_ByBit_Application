using System;
using System.Collections.Generic;
using WebApplication2.Entities;

namespace WebApplication2
{
    public interface IRepository<BaseEntity>
    {
        public int Create(BaseEntity entity);
        public int Update(BaseEntity item);
        public List<BalanceEntity> Read(BaseEntity item); 
        public int Delete(BaseEntity item);
    }
}