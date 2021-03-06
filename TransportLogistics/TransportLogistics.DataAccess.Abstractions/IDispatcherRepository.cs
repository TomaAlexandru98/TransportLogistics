﻿using System;
using System.Collections.Generic;
using System.Text;
using TransportLogistics.Model;

namespace TransportLogistics.DataAccess.Abstractions
{
    public interface IDispatcherRepository:IBaseRepository<Dispatcher>
    {
        Dispatcher GetByUserId(string userId);
    }
}
