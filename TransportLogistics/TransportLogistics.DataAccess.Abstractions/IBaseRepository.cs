﻿using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IBaseRepository<T> where T : DataEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        T Add(T entity);
        T Update(T entity);
        bool Remove(Guid id);
    }
}
