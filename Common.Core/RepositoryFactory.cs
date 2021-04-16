using Com.Models;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> CreateRepository<T>(DataContext mydbcontext) where T : class
        {
            return new Repository<T>(mydbcontext);
        }
    }
}
