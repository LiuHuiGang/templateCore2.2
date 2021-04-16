using Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.IService
{
    public interface IBaseService
    {
        IRepository<T> CreateService<T>() where T : class, new();
    }
}
