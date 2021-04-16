using Com.IService;
using Com.Models;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Service
{
    public class BaseService : IBaseService
    {
        private IRepositoryFactory _repositoryFactory;
        private DataContext _mydbcontext;
        public BaseService(IRepositoryFactory repositoryFactory, DataContext mydbcontext)
        {
            _repositoryFactory = repositoryFactory;
            _mydbcontext = mydbcontext;
        }

        public IRepository<T> CreateService<T>() where T : class, new()
        {
            return _repositoryFactory.CreateRepository<T>(_mydbcontext);
        }
    }
}
