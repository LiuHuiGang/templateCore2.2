using Com.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interface
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(DataContext mydbcontext) where T : class;
    }
}
